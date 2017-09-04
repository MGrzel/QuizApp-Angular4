import { pageInOut, elementInOut } from './../_animations/animations.component';
import { Category } from './../models/category';
import { Answer } from './../models/answer';
import { Question } from './../models/question';
import { QuizDataManagementService } from './../services/quiz-data-management.service';
import { Router } from '@angular/router';
import { Component, OnInit, HostBinding } from '@angular/core';
import { NgModel } from '@angular/forms';
import { QuizDataGetService } from '../services/quiz-data-get.service';

@Component({
    selector: 'category-management',
    templateUrl: './category-management.component.html',
    styleUrls: ['./category-management.component.scss'],
    animations: [pageInOut, elementInOut]
})

export class CategoryManagementComponent implements OnInit {
    @HostBinding('@pageInOut') pageInOut;

    categoryList: Category[];
    selectedCategory: Category;
    newCategory = new Category();
    categoryToRestore: Category;
    deletedCategoryList: Category[];
    tabAnimationState: string;
    alert = {
        success: null,
        error: null
    };


    constructor(
        private quizDataGetService: QuizDataGetService,
        private quizDataManagementService: QuizDataManagementService,
        private router: Router
    ) { }

    clearSucces(): void {
        this.alert.success = null;
    }

    clearError(): void {
        this.alert.error = null;
    }

    updateCategory(): void {
        this.quizDataManagementService.updateCategory(this.selectedCategory)
            .then(category => {
                this.alert.error = null;
                this.alert.success = 'Category updated successfuly!';
                this.getCategoryList();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Category not updated!';
            });
    }

    addCategory() {
        return this.quizDataManagementService.addCategory(this.newCategory)
            .then(category => {
                this.newCategory.title = '';
                this.alert.error = null;
                this.alert.success = 'Category added successfuly!';
                this.getCategoryList();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Category not added!';
            });
    }

    deleteCategory() {
        return this.quizDataManagementService.deleteCategory(this.selectedCategory)
            .then(category => {
                this.alert.error = null;
                this.alert.success = 'Category deleted successfuly!';
                this.getCategoryList();
                this.getDeletedCategoryList();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Category not deleted!';
            });
    }

    restoreCategory() {
        return this.quizDataManagementService.restoreCategory(this.categoryToRestore)
            .then(challenge => {
                this.alert.error = null;
                this.alert.success = 'Category restored successfuly!';
                this.getCategoryList();
                this.getDeletedCategoryList();
            })
            .catch(() => {
                this.alert.success = null;
                this.alert.error = 'Category not restored!';
            });
    }

    getCategoryList() {
        return this.quizDataGetService.getCategoryList()
        .then(categories => {
            this.categoryList = categories;
            this.selectedCategory = categories[0];
        })
        .catch(() => this.router.navigate([`/http-error`]));
    }

    getDeletedCategoryList() {
        return this.quizDataGetService.getDeletedCategoryList()
        .then(categories => {
            this.deletedCategoryList = categories;
            this.categoryToRestore = categories[0];
        });
    }

    ngOnInit() {
        this.getCategoryList();
        this.getDeletedCategoryList();
    }
}
