import { UserService } from './../services/user.service';
import { LoginUser } from './../models/loginUser';
import { pageInOut } from './../_animations/animations.component';
import { QuizDataGetService } from './../services/quiz-data-get.service';
import { Challenge } from './../models/challenge';
import { Router } from '@angular/router';
import { Component, OnInit, HostBinding } from '@angular/core';


import { NgStyle } from '@angular/common';

import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
    // tslint:disable-next-line:component-selector
    selector: 'login',
    templateUrl: 'login.component.html',
    styleUrls: [
        'login.component.scss'
    ],
    animations: [pageInOut]
})

export class LoginComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    constructor(
        private userService: UserService,
        private router: Router
    ) { }

    user: LoginUser;
    alert = {
        success: null,
        error: null
    };


    clearSucces(): void {
        this.alert.success = null;
    }

    clearError(): void {
        this.alert.error = null;
    }

    login(): void {
        this.alert.success = null;
        this.alert.error = null;
        this.userService.login(this.user)
            .then(user => {
                this.alert.error = null;
                this.alert.success = 'Successfuly logged in!';
                setTimeout(() => { this.router.navigate(['/account']); }, 1000);
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Login error!';
            });
    }



    ngOnInit(): void {
        this.user = new LoginUser();
    }
}
