import { AccountComponent } from './../account/account.component';
import { LoginComponent } from './../account/login.component';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { QuizDataGetService } from './../services/quiz-data-get.service';
import { QuizDataManagementService } from './../services/quiz-data-management.service';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { AppComponent } from './../app-root/app.component';
import { QuizListComponent } from '../quiz/quiz-list.component';
import { NotFoundComponent } from '../http-error/not-found.component';
import { StandardTypeQuizComponent } from '../quiz/standard-type-quiz.component';
import { SessionHistoryDetailComponent } from '../session-history/session-history-detail.component';
import { QuestionManagementComponent } from '../admin-tools/question-management.component';
import { MillionairesTypeQuizComponent } from '../quiz/millionaires-type-quiz.component';
import { HttpErrorComponent } from './../http-error/http-error.component';
import { ChallengeManagementComponent } from './../admin-tools/challenge-management.component';
import { CategoryManagementComponent } from './../admin-tools/category-management.component';
import { AdminToolsComponent } from './../admin-tools/admin-tools.component';
import { SessionHistoryListComponent } from './../session-history/session-history-list.component';
import { UserService } from '../services/user.service';
import { RegisterComponent } from '../account/register.component';


@NgModule({
  declarations: [
    AppComponent,
    QuizListComponent,
    NotFoundComponent,
    StandardTypeQuizComponent,
    SessionHistoryListComponent,
    SessionHistoryDetailComponent,
    AdminToolsComponent,
    QuestionManagementComponent,
    CategoryManagementComponent,
    ChallengeManagementComponent,
    HttpErrorComponent,
    MillionairesTypeQuizComponent,
    LoginComponent,
    RegisterComponent,
    AccountComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    HttpModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
  ],
  providers: [QuizDataGetService, QuizDataManagementService, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
