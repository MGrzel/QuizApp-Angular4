import { CategoryManagementComponent } from './../admin-tools/category-management.component';
import { StandardTypeQuizComponent } from './../quiz/standard-type-quiz.component';
import { MillionairesTypeQuizComponent } from './../quiz/millionaires-type-quiz.component';
import { HttpErrorComponent } from './../http-error/http-error.component';
import { ChallengeManagementComponent } from './../admin-tools/challenge-management.component';
import { QuestionManagementComponent } from './../admin-tools/question-management.component';
import { AdminToolsComponent } from './../admin-tools/admin-tools.component';
import { SessionHistoryDetailComponent } from './../session-history/session-history-detail.component';
import { SessionHistoryListComponent } from './../session-history/session-history-list.component';
import { QuizListComponent } from './../quiz/quiz-list.component';
import { NotFoundComponent } from '../http-error/not-found.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/quiz-list',
        pathMatch: 'full'
    },
    {
        path: 'quiz-list',
        component: QuizListComponent
    },
    {
        path: 'standard-quiz/:id',
        component: StandardTypeQuizComponent
    },
    {
        path: 'millionaires-quiz/:id',
        component: MillionairesTypeQuizComponent
    },
    {
        path: 'session-history',
        component: SessionHistoryListComponent
    },
    {
        path: 'session-history/:id',
        component: SessionHistoryDetailComponent
    },
    {
        path: 'admin-tools',
        component: AdminToolsComponent,
        children: [
            {
                path: '',
                redirectTo: '/admin-tools/question-management',
                pathMatch: 'full'
            },
            {
                path: 'question-management',
                component: QuestionManagementComponent
            },
            {
                path: 'category-management',
                component: CategoryManagementComponent
            },
            {
                path: 'challenge-management',
                component: ChallengeManagementComponent
            },
        ]
    },
    {
        path: 'http-error',
        component: HttpErrorComponent
    },
    {
        path: '**',
        component: NotFoundComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule {}
