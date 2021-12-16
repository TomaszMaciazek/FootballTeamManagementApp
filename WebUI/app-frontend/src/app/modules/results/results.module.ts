import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {EditorModule} from 'primeng/editor';
import { ResultsRoutingModule } from './results-routing.module';
import { TrainingsComponent } from './components/trainings/trainings.component';
import { AddTrainingComponent } from './components/trainings/add-training/add-training.component';
import { EditTrainingComponent } from './components/trainings/edit-training/edit-training.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    TrainingsComponent,
    AddTrainingComponent,
    EditTrainingComponent
  ],
  imports: [
    CommonModule,
    ResultsRoutingModule,
    SharedModule,
    EditorModule
  ]
})
export class ResultsModule { }
