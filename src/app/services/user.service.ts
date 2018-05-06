import { LoginUser } from './../models/loginUser';
import { environment } from './../../environments/environment';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { RegistrationUser } from '../models/registrationUser';

@Injectable()
export class UserService {
    private headers = new Headers({ 'Content-Type': 'application/json' });
    token: string;

    constructor(
        private http: Http,
        router: Router
    ) { }

    port = environment.apiPort;
    loggedIn: boolean;

    login(user: LoginUser): Promise<any> {
        return this.http.post(`http://localhost:${this.port}/quizapp/account/users/token`,
            JSON.stringify(user),
            { headers: this.headers })
            .toPromise()
            .then(response => {
                const token = response.json() && response.json().token;

                if (token) {
                    sessionStorage.setItem('token', token);
                    return true;
                } else {
                    return false;
                }
            });
    }

    checkLoginStatus(): boolean {
        const token = sessionStorage.getItem('token');

        if (token) {
            this.loggedIn = true;
        } else {
            this.loggedIn = false;
        }
        return this.loggedIn;
    }

    register(user: RegistrationUser): Promise<any> {
        return this.http.post(`http://localhost:${this.port}/quizapp/account/users/register`,
            JSON.stringify(user),
            { headers: this.headers })
            .toPromise();
    }

    logout(): void {
        sessionStorage.removeItem('token');
    }


    private handleError(error: any): Promise<any> {
        console.error(error);
        if (sessionStorage.getItem('token')) {
            sessionStorage.removeItem('token');
        }
        return Promise.reject(error.message || error);
    }
}
