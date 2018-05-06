import { CategoryQuestion } from './../models/categoryquestion';
import { Category } from './../models/category';
import { Answer } from './../models/answer';
import { Question } from './../models/question';
import { QuizDataGetService } from './../services/quiz-data-get.service';
import { QuizDataManagementService } from './../services/quiz-data-management.service';
import { elementInOut, pageInOut } from './../_animations/animations.component';
import { Router } from '@angular/router';
import { Component, OnInit, HostBinding } from '@angular/core';
import { NgModel } from '@angular/forms';



@Component({
    selector: 'question-management',
    templateUrl: './question-management.component.html',
    styleUrls: ['./question-management.component.scss'],
    animations: [pageInOut, elementInOut]
})

export class QuestionManagementComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    questionList: Question[];
    selectedQuestion: Question;
    newQuestion = new Question();
    categoryList: Category[];
    selectedCategory: Category;
    questionToRestore: Question;
    deletedQuestionList: Question[];
    alert = {
        success: null,
        error: null
    };

    constructor(
        private quizDataGetService: QuizDataGetService,
        private quizDataManagementService: QuizDataManagementService,
        private router: Router
    ) { }

    initializeNewQuestion(): void {

        this.newQuestion.answers = new Array<Answer>();
        this.newQuestion.categoryList = new Array<CategoryQuestion>();
        this.newQuestion.title = '';

        for (let i = 0; i < 4; i++) {
            const answer = new Answer;
            this.newQuestion.answers.push(answer);
        }
    }

    clearSucces(): void {
        this.alert.success = null;
    }

    clearError(): void {
        this.alert.error = null;
    }

    changeCorrectAnswer(question: Question, answer: Answer) {
        if(answer.title == null) {
            return;
        }
        for(let i = 0; i < question.answers.length; i++) {
            question.answers[i].isCorrect = false;
            if(question.answers[i].title === answer.title) {
                question.answers[i].isCorrect = true;
            }
        }
    }

    checkCategoryExistance(question: Question, category: Category): boolean {
        if (question.categoryList.length >= 0) {
            for (let i = 0; i < question.categoryList.length; i++) {
                if (question.categoryList[i].category.title === category.title) {
                    return false;
                }
            }
        }

        return true;
    }

    addCategory(question: Question, category: Category): void {
        const cat = new CategoryQuestion();
        cat.category = category;

        if (this.checkCategoryExistance(question, category)) {
            question.categoryList.push(cat);
        }
        // TODO: error
    }

    deleteCategory(question: Question, category: Category): void {
        const cat = new CategoryQuestion();
        cat.question = question;
        cat.questionId = question.id;
        cat.category = category;
        cat.categoryId = category.id;

        const index = this.findIndexOfCategory(question.categoryList, category);
        if (index > -1) {
            question.categoryList.splice(index, 1);
        }
        // TODO: error
    }

    findIndexOfCategory(categories: CategoryQuestion[], category: Category) {
        for(let i = 0; i < categories.length; i++) {
            if(categories[i].category.id === category.id) {
                return i;
            }
        }

        return -1;
    }

    updateQuestion(): void {
        this.quizDataManagementService.updateQuestion(this.selectedQuestion)
            .then(question => {
                this.alert.error = null;
                this.alert.success = 'Question updated successfuly!';
                this.getQuestionListAsAdmin();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Question not updated!';
            });
    }

    addQuestion(): void {
        this.quizDataManagementService.addQuestion(this.newQuestion)
            .then(question => {
                this.alert.error = null;
                this.alert.success = 'Question added successfuly!';
                this.getQuestionListAsAdmin();
                this.initializeNewQuestion();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Question not added!';
            });
    }

    deleteQuestion(): void {
        this.quizDataManagementService.deleteQuestion(this.selectedQuestion)
            .then(question => {
                this.alert.error = null;
                this.alert.success = 'Question deleted successfuly!';
                this.getQuestionListAsAdmin();
                this.getDeletedQuestionList();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Question not deleted!';
            });
    }

    restoreQuestion(): void {
        this.quizDataManagementService.restoreQuestion(this.questionToRestore)
            .then(question => {
                this.alert.error = null;
                this.alert.success = 'Question restored successfuly!';
                this.getQuestionListAsAdmin();
                this.getDeletedQuestionList();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Question not restored!';
            });
    }

    getQuestionListAsAdmin() {
        return this.quizDataGetService.getQuestionListAsAdmin()
            .then(questions => {
                this.questionList = questions;
                this.selectedQuestion = this.questionList[0];
            })
            .catch(() => this.router.navigate([`/http-error`]));
    }

    getCategoryList() {
        return this.quizDataGetService.getCategoryList()
            .then(categories => {
                this.categoryList = categories;
                this.selectedCategory = categories[0];
            })
            .catch(() => this.router.navigate([`/http-error`]));
    }

    getDeletedQuestionList() {
        return this.quizDataGetService.getDeletedQuestionList()
            .then(questions => {
                this.deletedQuestionList = questions;
                this.questionToRestore = questions[0];
            });
    }

    ngOnInit(): void {
        this.getQuestionListAsAdmin();
        this.getCategoryList();
        this.getDeletedQuestionList();
        // TODO: error

        this.initializeNewQuestion();
    }
}
