import { Question } from './../models/question';
import { Session } from './../models/session';
import { pageInOut, fadeInOut } from './../_animations/animations.component';
import { Answer } from './../models/answer';
import { QuizDataGetService } from './../services/quiz-data-get.service';
import { ClientQuiz } from './../models/clientquiz';
import { QuizDataManagementService } from './../services/quiz-data-management.service';
import { Component, HostBinding, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { NgClass, Location } from '@angular/common';

@Component({
    selector: 'millionaires-type-quiz',
    templateUrl: './millionaires-type-quiz.component.html',
    styleUrls: ['./millionaires-type-quiz.component.scss'],
    animations: [pageInOut, fadeInOut]
})

export class MillionairesTypeQuizComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    quizSession: Session;
    clientQuiz: ClientQuiz[];
    currentAnswer = 0;
    animatedAnswer: string;
    unselectedAnswer: string;
    color: string;
    questionAnimationState = 'in';
    pageAnimationState = 'in';
    id: string;
    wrongAnswer: boolean;
    noAnswer: boolean;
    highlightedAnswer: number;
    randomCount = new Array<number>();
    lastRandom: number;
    win: boolean;

    constructor(
        private quizDataGetService: QuizDataGetService,
        private quizDataManagementService: QuizDataManagementService,
        private route: ActivatedRoute,
        private location: Location,
        private router: Router
    ) { }

    checkIfNumberExists(checkedNumber: number): boolean {
        return this.randomCount.includes(checkedNumber);
    }

    generateRandomNumber(min: number, max: number, count: number): number {
        if (count === this.randomCount.length) {
            this.randomCount.splice(0);
        }

        let random: number;

        do {
            random = Math.floor((Math.random() * (max - min + 1)) + min);
        } while (this.checkIfNumberExists(random) || this.lastRandom === random);
        this.lastRandom = random;
        this.randomCount.push(random);

        return random;
    }


    changeQuestionState(questionAnimationState: string, delay: number): void {
        setTimeout(() => {
            this.questionAnimationState = questionAnimationState;
        }, delay);
    }

    changeQuestion(move: number): void {
        this.changeQuestionState('out', 0);

        if (this.currentAnswer < this.clientQuiz.length - 1) {
            this.currentAnswer++;
        } else {
            this.win = true;
            this.submitQuiz();
            setTimeout(() => {
                this.questionAnimationState = 'leave';
                this.router.navigate([`/`]);
            }, 2000);
        }

        this.changeQuestionState('in', 300);
    }

    selectAnswer(answer: Answer): void {
        this.animatedAnswer = answer.id;
        if (this.clientQuiz[this.currentAnswer].selectedAnswer === answer) {
            this.animatedAnswer = '';
            this.unselectedAnswer = answer.id;
            this.clientQuiz[this.currentAnswer].selectedAnswer = null;
        } else {
            this.unselectedAnswer = '';
            this.clientQuiz[this.currentAnswer].selectedAnswer = answer;
        }
    }

    highlightAnswer(count: number, interval: number): void {
        const randomCountMax = 4;
        if (count > 0) {
            setTimeout(() => {
                this.highlightedAnswer = this.generateRandomNumber(0, 3, 4);
                this.highlightAnswer(count - 1, interval);
            }, interval);
        }
        if (count === 0) {
            this.highlightedAnswer = null;
        }
    }

    checkSingleAnswer(clientQuiz: ClientQuiz): void {
        const count = 24;
        const interval = 100;
        const endWaitTime = 2000;
        this.highlightAnswer(count, interval);

        if (clientQuiz.selectedAnswer) {
            this.quizDataGetService.checkSingleAnswer(clientQuiz.question.id, clientQuiz.selectedAnswer.id)
                .then(result => {
                    setTimeout(() => {
                        if (result) {
                            this.wrongAnswer = false;
                            this.noAnswer = false;
                            setTimeout(() => {
                                this.changeQuestion(1);
                            }, endWaitTime);
                        } else {
                            this.wrongAnswer = true;
                            this.noAnswer = false;
                            this.submitQuiz();
                            setTimeout(() => {
                                this.questionAnimationState = 'leave';
                                this.router.navigate([`/`]);
                            }, endWaitTime);
                        }
                    }, count * interval + interval);
                });
        } else {
            setTimeout(() => {
                this.noAnswer = true;
                this.submitQuiz();
                setTimeout(() => {
                    this.questionAnimationState = 'leave';
                    this.router.navigate([`/`]);
                }, endWaitTime);
            }, count * interval);
        }
    }

    submitQuiz(): void {
        this.quizDataManagementService.submitQuiz(this.quizSession)
            .then(quiz => {
                this.quizSession = quiz;
            });
    }

    getQuiz() {
        return this.quizDataGetService.getQuizById(this.id)
            .then(quiz => {
                if (quiz.challenge.quizType.title !== 'millionaires') {
                    this.router.navigate([`/404`]);
                }
                this.quizSession = quiz;
                this.color = this.quizSession.challenge.color.title;
                this.clientQuiz = this.quizSession.clientQuiz;
            });
    }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.id = params['id'];
        });
        this.getQuiz()
            .catch(() => this.router.navigate([`/404`]));
    }
}
