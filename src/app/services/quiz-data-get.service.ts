import { QuizType } from './../models/quiztype';
import { Color } from './../models/color';
import { Session } from './../models/session';
import { ClientQuiz } from './../models/clientquiz';
import { Challenge } from './../models/challenge';
import { Question } from './../models/question';
import { Category } from './../models/category';
import { Answer } from './../models/answer';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

@Injectable()
export class QuizDataGetService {
    private headers = new Headers({'Content-Type': 'application/json'});

    constructor(
        private http: Http,
        router: Router
    ) { }

    port: number = 61337;

    getChallengeList(): Promise<Challenge[]> {
        return this.http.get(`http://localhost:${this.port}/quizapp/get/challenges/`)
            .toPromise()
            .then(response => response.json() as Challenge[])
            .catch(this.handleError);
    }

    getDeletedChallengeList(): Promise<Challenge[]> {
        return this.http.get(`http://localhost:${this.port}/quizapp/get/deleted/challenges/`)
            .toPromise()
            .then(response => response.json() as Challenge[])
            .catch(this.handleError);
    }

    getQuizById(id: number): Promise<Session> {
        return this.http.get(`http://localhost:${this.port}/quizapp/get/quiz/${ id }`)
            .toPromise()
            .then(response => response.json() as Session)
            .catch(this.handleError);
    }

    getCategoryList(): Promise<Category[]> {
        return this.http.get(`http://localhost:${this.port}/quizapp/get/categories`)
            .toPromise()
            .then(response => response.json() as Category[])
            .catch(this.handleError);
    }

    getDeletedCategoryList(): Promise<Category[]> {
        return this.http.get(`http://localhost:${this.port}/quizapp/get/deleted/categories`)
            .toPromise()
            .then(response => response.json() as Category[])
            .catch(this.handleError);
    }

    checkSingleAnswer(questionId: number, answerId: number): Promise<boolean> {
        return this.http.get(`http://localhost:${this.port}/quizapp/get/correctanswer/${ questionId }/${ answerId }`)
        .toPromise()
        .then(response => response.json() as boolean)
        .catch(this.handleError);
    }

    getSessionById(id: number): Promise<Session> {
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/sessions/${ id }`)
            .toPromise()
            .then(response => response.json() as Session)
            .catch(this.handleError);
    }

    getSessionList(): Promise<Session[]> {
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/sessions`)
            .toPromise()
            .then(response => response.json() as Session[])
            .catch(this.handleError);
    }

    getQuestionList(): Promise<Question[]> {
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/questions`)
            .toPromise()
            .then(response => response.json() as Question[])
            .catch(this.handleError);
    }

    getDeletedQuestionList(): Promise<Question[]> {
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/deleted/questions`)
            .toPromise()
            .then(response => response.json() as Question[])
            .catch(this.handleError);
    }

    getQuizTypeList(): Promise<QuizType[]> {
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/quiztypes`)
            .toPromise()
            .then(response => response.json() as QuizType[])
            .catch(this.handleError);
    }

    getColorList(): Promise<Color[]> {
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/colors`)
            .toPromise()
            .then(response => response.json() as Color[])
            .catch(this.handleError);
    }

    getQuestionListAsAdmin(): Promise<Question[]> {
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/admin/questions`)
            .toPromise()
            .then(response => response.json() as Question[])
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error(error);
        return Promise.reject(error.message || error);
    }
}
