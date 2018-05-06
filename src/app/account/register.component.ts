import { RegistrationUser } from './../models/registrationUser';
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
    selector: 'register',
    templateUrl: 'register.component.html',
    styleUrls: [
        'register.component.scss'
    ],
    animations: [pageInOut]
})

export class RegisterComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    constructor(
        private userService: UserService,
        private router: Router
    ) { }

    user: RegistrationUser;
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

    register(): void {
        this.alert.success = null;
        this.alert.error = null;
        this.userService.register(this.user)
            .then(user => {
                this.alert.error = null;
                this.alert.success = 'Successfuly registered!';
                setTimeout(() => { this.router.navigate(['/account/login']); }, 1000);
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Registration error!';
            });
    }



    ngOnInit(): void {
        this.user = new RegistrationUser();
    }
}
