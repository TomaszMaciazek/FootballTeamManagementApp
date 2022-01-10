import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {EditorModule} from 'primeng/editor';
import { ResultsRoutingModule } from './results-routing.module';
import { TrainingsComponent } from './components/trainings/trainings.component';
import { AddTrainingComponent } from './components/trainings/add-training/add-training.component';
import { EditTrainingComponent } from './components/trainings/edit-training/edit-training.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { TrainingComponent } from './components/trainings/training/training.component';
import { AddTrainingScoreComponent } from './components/trainingScores/add-training-scores/add-training-score.component';
import { TrainingScoresComponent } from './components/trainingScores/training-scores.component';
import { TrainingScoresTableComponent } from './components/trainingScores/training-scores-table/training-scores-table.component';
import { EditTrainingScoreValueComponent } from './components/trainingScores/edit-training-score-value/edit-training-score-value.component';
import { MatchesComponent } from './components/matches/matches.component';
import { AddMatchComponent } from './components/matches/add-match/add-match.component';
import { MatchStepOneComponent } from './components/matches/match-step-one/match-step-one.component';
import { MatchStepTwoComponent } from './components/matches/match-step-two/match-step-two.component';
import { MatchStepThreeComponent } from './components/matches/match-step-three/match-step-three.component';
import { MatchStepFourComponent } from './components/matches/match-step-four/match-step-four.component';
import { MatchStepFiveComponent } from './components/matches/match-step-five/match-step-five.component';
import { MatchStepSixComponent } from './components/matches/match-step-six/match-step-six.component';
import { MatchComponent } from './components/matches/match/match.component';
import { MatchScoresComponent } from './components/matches/match-scores/match-scores.component';
import { AddMatchScoreComponent } from './components/matches/match-scores/add-match-score/add-match-score.component';
import { EditMatchScoreComponent } from './components/matches/match-scores/edit-match-score/edit-match-score.component';
import { MatchScoresTableComponent } from './components/matches/match-scores/match-scores-table/match-scores-table.component';


@NgModule({
  declarations: [
    TrainingsComponent,
    AddTrainingComponent,
    EditTrainingComponent,
    TrainingComponent,
    AddTrainingScoreComponent,
    TrainingScoresComponent,
    TrainingScoresTableComponent,
    EditTrainingScoreValueComponent,
    MatchesComponent,
    AddMatchComponent,
    MatchStepOneComponent,
    MatchStepTwoComponent,
    MatchStepThreeComponent,
    MatchStepFourComponent,
    MatchStepFiveComponent,
    MatchStepSixComponent,
    MatchComponent,
    MatchScoresComponent,
    AddMatchScoreComponent,
    EditMatchScoreComponent,
    MatchScoresTableComponent
  ],
  imports: [
    CommonModule,
    ResultsRoutingModule,
    SharedModule,
    EditorModule
  ]
})
export class ResultsModule { }
