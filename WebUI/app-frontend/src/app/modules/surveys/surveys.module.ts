import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SurveysRoutingModule } from './surveys-routing.module';
import { CreateSurveyComponent } from './components/create-survey/create-survey.component';
import { SharedModule } from 'src/app/shared/shared.module';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatCardModule} from '@angular/material/card';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatChipsModule} from '@angular/material/chips';
import {MatRadioModule} from '@angular/material/radio';
import { SurveysAllComponent } from './components/surveys-all/surveys-all.component';
import { SurveysCreatedByMeComponent } from './components/surveys-created-by-me/surveys-created-by-me.component';
import { SurveysAssignedToMeComponent } from './components/surveys-assigned-to-me/surveys-assigned-to-me.component';
import { SurveyFillComponent } from './components/survey-fill/survey-fill.component';
import { SurveyUserResultComponent } from './components/survey-user-result/survey-user-result.component';
import { QuestionAnswerComponent } from './components/question-answer/question-answer.component';
import { SurveyResultsComponent } from './components/survey-results/survey-results.component';
import { QuestionAnswersComponent } from './components/question-answers/question-answers.component';
import { RespondentComponent } from './components/question-answers/respondent/respondent.component';

@NgModule({
  declarations: [
    CreateSurveyComponent,
    SurveysAllComponent,
    SurveysCreatedByMeComponent,
    SurveysAssignedToMeComponent,
    SurveyFillComponent,
    SurveyUserResultComponent,
    QuestionAnswerComponent,
    SurveyResultsComponent,
    QuestionAnswersComponent,
    RespondentComponent
  ],
  imports: [
    CommonModule,
    SurveysRoutingModule,
    SharedModule,
    MatInputModule,
    MatFormFieldModule,
    MatSlideToggleModule,
    MatCardModule,
    MatSelectModule,
    MatButtonModule,
    MatChipsModule,
    MatRadioModule
  ]
})
export class SurveysModule { }
