import { environment } from './../../environments/environment';
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


@Injectable()
export class QuizDataGetService {
    constructor(
        private http: Http,
        router: Router
    ) { }

    port = environment.apiPort;


    getChallengeList(): Promise<Challenge[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http.get(`http://localhost:${this.port}/quizapp/get/challenges/`, { headers: header })
            .toPromise()
            .then(response => response.json() as Challenge[])
            .catch(this.handleError);
    }

    getDeletedChallengeList(): Promise<Challenge[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http.get(`http://localhost:${this.port}/quizapp/get/deleted/challenges/`, { headers: header })
            .toPromise()
            .then(response => response.json() as Challenge[])
            .catch(this.handleError);
    }

    getQuizById(id: string): Promise<Session> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http.get(`http://localhost:${this.port}/quizapp/get/quiz/${id}`, { headers: header })
            .toPromise()
            .then(response => response.json() as Session)
            .catch(this.handleError);
    }

    getCategoryList(): Promise<Category[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http.get(`http://localhost:${this.port}/quizapp/get/categories`, { headers: header })
            .toPromise()
            .then(response => response.json() as Category[])
            .catch(this.handleError);
    }

    getDeletedCategoryList(): Promise<Category[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http.get(`http://localhost:${this.port}/quizapp/get/deleted/categories`, { headers: header })
            .toPromise()
            .then(response => response.json() as Category[])
            .catch(this.handleError);
    }

    checkSingleAnswer(questionId: string, answerId: string): Promise<boolean> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http.get(`http://localhost:${this.port}/quizapp/get/correctanswer/${questionId}/${answerId}`, { headers: header })
            .toPromise()
            .then(response => response.json() as boolean)
            .catch(this.handleError);
    }

    getSessionById(id: string): Promise<Session> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/sessions/${id}`, { headers: header })
            .toPromise()
            .then(response => response.json() as Session)
            .catch(this.handleError);
    }

    getSessionList(): Promise<Session[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/sessions`, { headers: header })
            .toPromise()
            .then(response => response.json() as Session[])
            .catch(this.handleError);
    }

    getQuestionList(): Promise<Question[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/questions`, { headers: header })
            .toPromise()
            .then(response => response.json() as Question[])
            .catch(this.handleError);
    }

    getDeletedQuestionList(): Promise<Question[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/deleted/questions`, { headers: header })
            .toPromise()
            .then(response => response.json() as Question[])
            .catch(this.handleError);
    }

    getQuizTypeList(): Promise<QuizType[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/quiztypes`, { headers: header })
            .toPromise()
            .then(response => response.json() as QuizType[])
            .catch(this.handleError);
    }

    getColorList(): Promise<Color[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/colors`, { headers: header })
            .toPromise()
            .then(response => response.json() as Color[])
            .catch(this.handleError);
    }

    getQuestionListAsAdmin(): Promise<Question[]> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        return this.http
            .get(`http://localhost:${this.port}/quizapp/get/admin/questions`, { headers: header })
            .toPromise()
            .then(response => response.json() as Question[])
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error(error);
        return Promise.reject(error.message || error);
    }
}
