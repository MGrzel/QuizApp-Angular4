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
    selector: 'standard-type-quiz',
    templateUrl: './standard-type-quiz.component.html',
    styleUrls: ['./standard-type-quiz.component.scss'],
    animations: [pageInOut, fadeInOut]
})

export class StandardTypeQuizComponent implements OnInit {
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

    constructor(
        private quizDataGetService: QuizDataGetService,
        private quizDataManagementService: QuizDataManagementService,
        private route: ActivatedRoute,
        private location: Location,
        private router: Router
    ) { }



    changeQuestionState(questionAnimationState: string, delay: number): void {
        setTimeout(() => {
            this.questionAnimationState = questionAnimationState;
        }, delay);
    }

    changeQuestion(move: number): void {
        this.changeQuestionState('out', 0);

        if (move < 0 && this.currentAnswer === 0) {
            this.currentAnswer = this.clientQuiz.length - 1;
        } else {
            move += this.currentAnswer;
            this.currentAnswer = Math.abs(move % this.clientQuiz.length);
        }

        this.changeQuestionState('in', 300);
    }

    moveToQuestion(number: number): void {
        this.changeQuestionState('out', 0);

        this.currentAnswer = number;

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

    submitQuiz(): void {
        this.quizDataManagementService.submitQuiz(this.quizSession)
            .then(quiz => {
                this.quizSession = quiz;
            });

        this.questionAnimationState = 'leave';
        setTimeout(() => {
            this.router.navigate([`/`]);
        }, 150);
    }

    getQuiz() {
        return this.quizDataGetService.getQuizById(this.id)
            .then(quiz => {
                this.quizSession = quiz;
                if (quiz.challenge.quizType.title !== 'standard') {
                    this.router.navigate([`/404`]);
                }
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
