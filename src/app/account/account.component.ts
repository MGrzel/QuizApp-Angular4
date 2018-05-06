import { UserService } from './../services/user.service';
import { pageInOut } from './../_animations/animations.component';
import { QuizDataGetService } from './../services/quiz-data-get.service';
import { Challenge } from './../models/challenge';
import { Router } from '@angular/router';
import { Component, OnInit, HostBinding } from '@angular/core';


import { NgStyle } from '@angular/common';

import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
    // tslint:disable-next-line:component-selector
    selector: 'account',
    templateUrl: 'account.component.html',
    styleUrls: [
        'account.component.scss'
    ],
    animations: [pageInOut]
})

export class AccountComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    constructor(
        private userService: UserService
    ) { }

    isLogged: boolean;

    logout(): void {
        this.userService.logout();
        this.checkLoginStatus();
    }

    checkLoginStatus(): void {
        this.isLogged = this.userService.checkLoginStatus();
    }

    ngOnInit(): void {
        this.checkLoginStatus();
    }
}
