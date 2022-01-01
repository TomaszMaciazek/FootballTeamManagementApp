import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddMatchComponent } from './components/matches/add-match/add-match.component';
import { MatchesComponent } from './components/matches/matches.component';
import { AddTrainingComponent } from './components/trainings/add-training/add-training.component';
import { EditTrainingComponent } from './components/trainings/edit-training/edit-training.component';
import { TrainingComponent } from './components/trainings/training/training.component';
import { TrainingsComponent } from './components/trainings/trainings.component';
import { AddTrainingScoreComponent } from './components/trainingScores/add-training-scores/add-training-score.component';

const routes: Routes = [
  { path: 'trainings/add', component: AddTrainingComponent},
  { path: 'trainings/edit', component: EditTrainingComponent},
  { path: 'trainings', component: TrainingsComponent},
  { path: 'training/:id', component: TrainingComponent},
  { path: 'matches', component: MatchesComponent},
  { path: 'matches/add', component: AddMatchComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ResultsRoutingModule { }
