import { Question } from './../models/question';
import { Session } from './../models/session';
import { pageInOut, fadeInOut } from './../_animations/animations.component';
import { Answer } from './../models/answer';
import { QuizDataGetService } from './../services/quiz-data-get.service';
import { ClientQuiz } from './../models/clientquiz';
import { Component, HostBinding, OnInit, OnChanges } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { NgClass, Location } from '@angular/common';


@Component({
    selector: 'session-history-list',
    templateUrl: './session-history-list.component.html',
    styleUrls: ['./session-history-list.component.scss'],
    animations: [pageInOut, fadeInOut]
})

export class SessionHistoryListComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    sessions: Session[];
    color: string;
    pageAnimationState = 'in';

    constructor(
        private quizDataGetService: QuizDataGetService,
        private route: ActivatedRoute,
        private location: Location,
        private router: Router
    ) { }

    sessionDetail(id: number): void {
        this.pageAnimationState = 'out';
        setTimeout(() => {
            this.router.navigate([`/session-history`, id]);
        }, 150);
    }

    ngOnInit(): void {
        this.quizDataGetService.getSessionList()
            .then(session => this.sessions = session)
            .catch(() => this.router.navigate([`/http-error`]));
    }
}
