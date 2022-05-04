import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { CreateSurveyComponent } from './components/create-survey/create-survey.component';
import { SurveyFillComponent } from './components/survey-fill/survey-fill.component';
import { SurveyResultsComponent } from './components/survey-results/survey-results.component';
import { SurveyUserResultComponent } from './components/survey-user-result/survey-user-result.component';
import { SurveysAllComponent } from './components/surveys-all/surveys-all.component';
import { SurveysAssignedToMeComponent } from './components/surveys-assigned-to-me/surveys-assigned-to-me.component';
import { SurveysCreatedByMeComponent } from './components/surveys-created-by-me/surveys-created-by-me.component';

const routes: Routes = [
  { path: 'create', component: CreateSurveyComponent, canActivate: [AuthGuard]},
  { path: 'all', component: SurveysAllComponent, canActivate: [AuthGuard]},
  { path: 'created', component: SurveysCreatedByMeComponent, canActivate: [AuthGuard]},
  { path: 'assigned', component: SurveysAssignedToMeComponent, canActivate: [AuthGuard]},
  { path: 'fill/:id', component: SurveyFillComponent, canActivate: [AuthGuard]},
  { path: 'result/:id', component: SurveyUserResultComponent, canActivate: [AuthGuard]},
  { path: 'results/all/:id', component: SurveyResultsComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SurveysRoutingModule { }
