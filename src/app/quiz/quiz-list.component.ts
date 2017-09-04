import { pageInOut } from './../_animations/animations.component';
import { QuizDataGetService } from './../services/quiz-data-get.service';
import { Challenge } from './../models/challenge';
import { Router } from '@angular/router';
import { Component, OnInit, HostBinding } from '@angular/core';


import { NgStyle } from '@angular/common';

import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
    selector: 'quiz-list',
    templateUrl: 'quiz-list.component.html',
    styleUrls: [
        'quiz-list.component.scss'
    ],
    animations: [pageInOut]
})

export class QuizListComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    challengeList: Challenge[];
    pageAnimationState = 'in';
    selectedChallenge: Challenge;

    constructor(
        private quizDataGetService: QuizDataGetService,
        private modalService: NgbModal,
        private router: Router
    ) { }

    open(challenge: Challenge) {
        this.selectedChallenge = challenge;
    }

    selectQuiz(id: number): void {
        this.pageAnimationState = 'out';
        setTimeout(() => {
            this.router.navigate([`/${this.selectedChallenge.quizType.title}-quiz`, id]);
        }, 150);
    }

    getChallengeList() {
        return this.quizDataGetService.getChallengeList()
            .then(challenges => this.challengeList = challenges);
    }


    ngOnInit(): void {
        this.getChallengeList()
            .catch(() => this.router.navigate([`/http-error`]));
    }
}
