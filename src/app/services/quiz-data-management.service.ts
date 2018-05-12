import { UserService } from './user.service';
import { environment } from './../../environments/environment';
import { Category } from './../models/category';
import { Session } from './../models/session';
import { ClientQuiz } from './../models/clientquiz';
import { Challenge } from './../models/challenge';
import { Question } from './../models/question';
import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';


@Injectable()
export class QuizDataManagementService {

    constructor(
        private http: Http,
        private userService: UserService
    ) { }

    port = environment.apiPort;

    updateQuestion(question: Question): Promise<Question> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .put
            (`http://localhost:${this.port}/quizapp/put/questions/${ question.id }`, JSON.stringify(question), { headers: header })
            .toPromise()
            .then(response => response.json() as Question)
            .catch(this.handleError);
    }

    updateCategory(category: Category): Promise<Category> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .put
            (`http://localhost:${this.port}/quizapp/put/categories/${ category.id }`, JSON.stringify(category), { headers: header })
            .toPromise()
            .then(response => response.json() as Category)
            .catch(this.handleError);
    }

    updateChallenge(challenge: Challenge): Promise<Challenge> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .put
            (`http://localhost:${this.port}/quizapp/put/challenges/${ challenge.id }`, JSON.stringify(challenge), { headers: header })
            .toPromise()
            .then(response => response.json() as Challenge)
            .catch(this.handleError);
    }

    addQuestion(question: Question): Promise<Question> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .post(`http://localhost:${this.port}/quizapp/post/questions`, JSON.stringify(question), { headers: header })
            .toPromise()
            .then(response => response.json() as Question)
            .catch(this.handleError);
    }

    addCategory(category: Category): Promise<Category> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .post(`http://localhost:${this.port}/quizapp/post/categories`, JSON.stringify(category), { headers: header })
            .toPromise()
            .then(response => response.json() as Category)
            .catch(this.handleError);
    }

    addChallenge(challenge: Challenge): Promise<Challenge> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .post(`http://localhost:${this.port}/quizapp/post/challenges`, JSON.stringify(challenge), { headers: header })
            .toPromise()
            .then(response => response.json() as Challenge)
            .catch(this.handleError);
    }

    submitQuiz(quiz: Session): Promise<Session> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
        .post(`http://localhost:${this.port}/quizapp/post/sessions`, JSON.stringify(quiz), { headers: header })
        .toPromise()
        .then(response => response.json() as Session)
        .catch(this.handleError);
    }

    deleteQuestion(question: Question): Promise<Question> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .delete(`http://localhost:${this.port}/quizapp/delete/questions/${ question.id }`, { headers: header })
            .toPromise()
            .catch(this.handleError);
    }

    deleteCategory(category: Category): Promise<Category> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .delete(`http://localhost:${this.port}/quizapp/delete/categories/${ category.id }`, { headers: header })
            .toPromise()
            .catch(this.handleError);
    }

    deleteChallenge(challenge: Challenge): Promise<Challenge> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .delete(`http://localhost:${this.port}/quizapp/delete/challenges/${ challenge.id }`, { headers: header })
            .toPromise()
            .catch(this.handleError);
    }


    restoreQuestion(question: Question): Promise<Question> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .patch
            (`http://localhost:${this.port}/quizapp/patch/questions/${ question.id }`, JSON.stringify(question), { headers: header })
            .toPromise()
            .catch(this.handleError);
    }

    restoreCategory(category: Category): Promise<Category> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .patch
            (`http://localhost:${this.port}/quizapp/patch/categories/${ category.id }`, JSON.stringify(category), { headers: header })
            .toPromise()
            .catch(this.handleError);
    }

    restoreChallenge(challenge: Challenge): Promise<Challenge> {
        const token = sessionStorage.getItem('token');
        const header = new Headers({'Authorization': `Bearer ${token}`});
        header.append('Content-Type', 'application/json');
        return this.http
            .patch
            (`http://localhost:${this.port}/quizapp/patch/challenges/${challenge.id}`, JSON.stringify(challenge), { headers: header })
            .toPromise()
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        this.userService.logout();
        console.error(error);
        return Promise.reject(error.message || error);
    }
}
