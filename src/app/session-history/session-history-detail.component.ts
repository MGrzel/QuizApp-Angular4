import { fadeInOut, pageInOut } from './../_animations/animations.component';
import { ClientQuiz } from './../models/clientquiz';
import { Session } from './../models/session';
import { Answer } from './../models/answer';
import { QuizDataGetService } from './../services/quiz-data-get.service';
import { Component, HostBinding, OnInit, OnChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { NgClass, Location } from '@angular/common';

@Component({
    selector: 'session-history-detail',
    templateUrl: './session-history-detail.component.html',
    styleUrls: ['./session-history-detail.component.scss'],
    animations: [pageInOut, fadeInOut]
})

export class SessionHistoryDetailComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    quizSession: Session;
    clientQuiz: ClientQuiz[];
    currentAnswer = 0;
    color: string;
    questionAnimationState = 'in';
    pageAnimationState = 'in';
    id: string;

    constructor(
        private quizDataGetService: QuizDataGetService,
        private route: ActivatedRoute,
        private location: Location,
        private router: Router
    ) { }

    moveToQuestion(number: number): void {
        this.changeQuestionState('out', 0);

        this.currentAnswer = number;

        this.changeQuestionState('in', 300);
    }


    changeQuestionState(state: string, delay: number): void {
        setTimeout(() => {
            this.questionAnimationState = state;
        }, delay);
    }

    changePageState(state: string, delay: number): void {
        setTimeout(() => {
            this.questionAnimationState = state;
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

    getSessionById() {
        return this.quizDataGetService.getSessionById(this.id)
            .then(quiz => {
                this.quizSession = quiz;
                this.color = this.quizSession.challenge.color.title;
                this.clientQuiz = this.quizSession.clientQuiz;
            })
    }

    ngOnInit(): void {

        this.route.params.subscribe(params => {
            this.id = params['id'];
        });

        this.getSessionById()
            .catch(() => this.router.navigate([`/http-error`]));
    }
}
