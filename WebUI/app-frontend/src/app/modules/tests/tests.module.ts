import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from 'src/app/shared/shared.module';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatCardModule} from '@angular/material/card';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatChipsModule} from '@angular/material/chips';
import {MatRadioModule} from '@angular/material/radio';

import { TestsRoutingModule } from './tests-routing.module';
import { CreateTestComponent } from './components/create-test/create-test.component';
import { TestUserResultComponent } from './components/test-user-result/test-user-result.component';
import { TestPassScoreSelectComponent } from './components/create-test/test-pass-score-select/test-pass-score-select.component';
import { TestsAllComponent } from './components/tests-all/tests-all.component';
import { TestFillComponent } from './components/test-fill/test-fill.component';
import { TestsCreatedByMeComponent } from './components/tests-created-by-me/tests-created-by-me.component';
import { TestsAssignedToMeComponent } from './components/tests-assigned-to-me/tests-assigned-to-me.component';
import { TestResultsComponent } from './components/test-results/test-results.component';
import { TestQuestionAnswerComponent } from './components/test-user-result/test-question-answer/test-question-answer.component';


@NgModule({
  declarations: [
    CreateTestComponent,
    TestUserResultComponent,
    TestPassScoreSelectComponent,
    TestsAllComponent,
    TestFillComponent,
    TestsCreatedByMeComponent,
    TestsAssignedToMeComponent,
    TestResultsComponent,
    TestQuestionAnswerComponent
  ],
  imports: [
    CommonModule,
    TestsRoutingModule,
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
export class TestsModule { }
