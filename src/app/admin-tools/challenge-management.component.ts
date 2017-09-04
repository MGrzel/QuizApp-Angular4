import { QuizType } from './../models/quiztype';
import { Category } from './../models/category';
import { Answer } from './../models/answer';
import { Challenge } from './../models/challenge';
import { QuizDataGetService } from './../services/quiz-data-get.service';
import { QuizDataManagementService } from './../services/quiz-data-management.service';
import { Color } from './../models/color';
import { elementInOut, pageInOut } from './../_animations/animations.component';
import { Router } from '@angular/router';
import { Component, OnInit, HostBinding } from '@angular/core';
import { NgModel } from '@angular/forms';

@Component({
    selector: 'challenge-management',
    templateUrl: './challenge-management.component.html',
    styleUrls: ['./challenge-management.component.scss'],
    animations: [pageInOut, elementInOut]
})

export class ChallengeManagementComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    challengeList: Challenge[];
    quizTypeList: QuizType[];
    selectedChallenge: Challenge;
    newChallenge = new Challenge();
    colorList: Color[];
    categoryList: Category[];
    selectedCategory: Category;
    challengeToRestore: Challenge;
    deletedChallengeList: Challenge[];

    alert = {
        success: null,
        error: null
    };

    constructor(
        private quizDataGetService: QuizDataGetService,
        private quizDataManagementService: QuizDataManagementService,
        private router: Router
    ) { }

    initializeNewChallenge() {
        this.newChallenge.categoryList = new Array<Category>();
        this.newChallenge.id = 0;
        this.newChallenge.title = '';
    }

    clearSucces(): void {
        this.alert.success = null;
    }

    clearError(): void {
        this.alert.error = null;
    }

    checkCategoryExistance(challenge: Challenge, category: Category): boolean {
        if (challenge.categoryList.length >= 0) {
            for (let i = 0; i < challenge.categoryList.length; i++) {
                if (challenge.categoryList[i].title === category.title) {
                    return false;
                }
            }
        }

        return true;
    }

    addCategory(challenge: Challenge, category: Category): void {
        if (this.checkCategoryExistance(challenge, category)) {
            challenge.categoryList.push(category);
        }
    }

    deleteCategory(challenge: Challenge, category: Category): void {
        const index = challenge.categoryList.indexOf(category);
        if (index > -1) {
            challenge.categoryList.splice(index, 1);
        }
    }

    updateChallenge(): void {
        this.quizDataManagementService.updateChallenge(this.selectedChallenge)
            .then(challenge => {
                this.alert.error = null;
                this.alert.success = 'Challenge updated successfuly!';
                this.getChallengeList()
                    .then(() => {
                        this.bindPropertiesToChallenge();
                    });
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Challenge not updated!';
            });
    }

    addChallenge(): void {
        this.quizDataManagementService.addChallenge(this.newChallenge)
            .then(challenge => {
                this.alert.error = null;
                this.alert.success = 'Challenge added successfuly!';
                this.initializeNewChallenge();
                this.getChallengeList()
                    .then(() => {
                        this.bindPropertiesToChallenge();
                    });
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Challenge not added!';
            });
    }

    deleteChallenge(): void {
        this.quizDataManagementService.deleteChallenge(this.selectedChallenge)
            .then(question => {
                this.alert.error = null;
                this.alert.success = 'Challenge deleted successfuly!';
                this.getChallengeList()
                    .then(() => {
                        this.bindPropertiesToChallenge();
                    });
                this.getDeletedChallengeList();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Challenge not deleted!';
            });
    }

    restoreChallenge(): void {
        this.quizDataManagementService.restoreChallenge(this.challengeToRestore)
            .then(question => {
                this.alert.error = null;
                this.alert.success = 'Challenge restored successfuly!';
                this.getChallengeList()
                    .then(() => {
                        this.bindPropertiesToChallenge();
                    });
                this.getDeletedChallengeList();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Challenge not restored!';
            });
    }

    getColorById(id: number): Color {
        for (let i = 0; i < this.colorList.length; i++) {
            if (id === this.colorList[i].id) {
                return this.colorList[i];
            }
        }
    }

    getQuizTypeById(id: number): QuizType {
        for (let i = 0; i < this.quizTypeList.length; i++) {
            if (id === this.quizTypeList[i].id) {
                return this.quizTypeList[i];
            }
        }
    }

    bindPropertiesToChallenge() {
        for (let i = 0; i < this.challengeList.length; i++) {
            this.challengeList[i].color = this.getColorById(this.challengeList[i].color.id);
        }

        for (let i = 0; i < this.challengeList.length; i++) {
            this.challengeList[i].quizType = this.getQuizTypeById(this.challengeList[i].quizType.id);
        }
    }

    getChallengeList() {
        return this.quizDataGetService.getChallengeList()
            .then(challenges => {
                this.challengeList = challenges;
                this.selectedChallenge = challenges[0];
            })
            .catch(() => this.router.navigate([`/http-error`]));
    }

    getDeletedChallengeList() {
        return this.quizDataGetService.getDeletedChallengeList()
            .then(challenges => {
                this.deletedChallengeList = challenges;
                this.challengeToRestore = challenges[0];
            });
    }

    getColorList() {
        return this.quizDataGetService.getColorList()
            .then(colors => {
                this.colorList = colors;
                this.newChallenge.color = colors[0];
            })
            .catch(() => this.router.navigate([`/http-error`]));
    }

    getCategoryList() {
        return this.quizDataGetService.getCategoryList()
            .then(categories => {
                this.categoryList = categories;
                this.selectedCategory = categories[0];
                this.newChallenge.quizType = this.quizTypeList[0];
            })
            .catch(() => this.router.navigate([`/http-error`]));
    }

    getQuizTypeList() {
        return this.quizDataGetService.getQuizTypeList()
            .then(quiztypes => {
                this.quizTypeList = quiztypes;
            })
            .catch(() => this.router.navigate([`/http-error`]));
    }

    ngOnInit() {
        this.getChallengeList()
            .then(() => {
                this.getColorList()
                    .then(() => {
                        this.getQuizTypeList()
                            .then(() => {
                                this.getCategoryList()
                                    .then(() => {
                                        this.getDeletedChallengeList();
                                        this.bindPropertiesToChallenge();
                                        this.initializeNewChallenge();
                                    });
                            });
                    });
            });
        // TODO: error
    }
}
