import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TrainingScoreType } from 'src/app/enums/training-score-type';
import { TrainingScore } from 'src/app/models/training-score.model';

@Component({
  selector: 'training-scores-table',
  templateUrl: './training-scores-table.component.html'
})
export class TrainingScoresTableComponent {

  @Input() trainingScores: TrainingScore[];
  @Output() deleteScore = new EventEmitter<string>();
  @Output() updateScore = new EventEmitter<string>();
  @Output() activateScore = new EventEmitter<string>();
  @Output() deactivateScore = new EventEmitter<string>();

  confirmDeleteScore(id: string){
    this.deleteScore.emit(id);
  }

  confirmEditScore(id: string){
    this.updateScore.emit(id);
  }

  confirmActivateScore(id: string){
    this.activateScore.emit(id);
  }

  confirmDeactivateScore(id: string){
    this.deactivateScore.emit(id);
  }
}
