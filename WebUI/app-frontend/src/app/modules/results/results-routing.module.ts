import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddTrainingComponent } from './components/trainings/add-training/add-training.component';
import { EditTrainingComponent } from './components/trainings/edit-training/edit-training.component';
import { TrainingsComponent } from './components/trainings/trainings.component';

const routes: Routes = [
  { path: 'trainings/add', component: AddTrainingComponent},
  { path: 'trainings/edit', component: EditTrainingComponent},
  { path: 'trainings', component: TrainingsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ResultsRoutingModule { }
