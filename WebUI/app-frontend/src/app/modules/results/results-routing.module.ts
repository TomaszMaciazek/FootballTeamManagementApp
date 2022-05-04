import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { AddMatchComponent } from './components/matches/add-match/add-match.component';
import { MatchComponent } from './components/matches/match/match.component';
import { MatchesComponent } from './components/matches/matches.component';
import { AddTrainingComponent } from './components/trainings/add-training/add-training.component';
import { EditTrainingComponent } from './components/trainings/edit-training/edit-training.component';
import { TrainingComponent } from './components/trainings/training/training.component';
import { TrainingsComponent } from './components/trainings/trainings.component';
import { AddTrainingScoreComponent } from './components/trainingScores/add-training-scores/add-training-score.component';

const routes: Routes = [
  { path: 'trainings/add', component: AddTrainingComponent, canActivate: [AuthGuard]},
  { path: 'trainings/edit', component: EditTrainingComponent, canActivate: [AuthGuard]},
  { path: 'trainings', component: TrainingsComponent, canActivate: [AuthGuard]},
  { path: 'training/:id', component: TrainingComponent, canActivate: [AuthGuard]},
  { path: 'matches', component: MatchesComponent, canActivate: [AuthGuard]},
  { path: 'matches/add', component: AddMatchComponent, canActivate: [AuthGuard]},
  { path: 'match/:id', component: MatchComponent, canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ResultsRoutingModule { }
