import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TrainingScoreType } from 'src/app/enums/training-score-type';
import { TrainingScore } from 'src/app/models/training-score.model';

@Component({
  selector: 'training-scores',
  templateUrl: './training-scores.component.html'
})
export class TrainingScoresComponent implements OnInit {

  @Input() trainingScores: TrainingScore[];

  @Output() confirmDeleteScore = new EventEmitter<string>();
  @Output() confirmUpdateScore = new EventEmitter<string>();
  @Output() confirmDeactivateScore = new EventEmitter<string>();
  @Output() confirmActivateScore = new EventEmitter<string>();

  ballControlScores: Array<TrainingScore> = [];
  leftFootPassAccuracyScores: Array<TrainingScore> = [];
  rightFootPassAccuracyScores: Array<TrainingScore> = [];
  leftFootDribblingAccuracyScores: Array<TrainingScore> = [];
  rightFootDribblingAccuracyScores: Array<TrainingScore> = [];
  leftFootBallReceivingAccuracyScores: Array<TrainingScore> = [];
  rightFootBallReceivingAccuracyScores: Array<TrainingScore> = [];
  leftFootShotsAccuracyScores: Array<TrainingScore> = [];
  rightFootShotsAccuracyScores: Array<TrainingScore> = [];
  headingAccuracyScores: Array<TrainingScore> = [];
  oneVsOneScoreScores: Array<TrainingScore> = [];
  mobilityScores: Array<TrainingScore> = [];
  strengthScores: Array<TrainingScore> = [];
  enduranceScores: Array<TrainingScore> = [];
  agilityScores: Array<TrainingScore> = [];
  coordinationScores: Array<TrainingScore> = [];
  concentrationScores: Array<TrainingScore> = [];
  emotionsControlScores: Array<TrainingScore> = [];
  selfconfidenceScores: Array<TrainingScore> = [];
  stressControlScores: Array<TrainingScore> = [];
  attitudeScores: Array<TrainingScore> = [];
  communicationScores: Array<TrainingScore> = [];
  cooperationScores: Array<TrainingScore> = [];
  determinationScores: Array<TrainingScore> = [];
  disciplineScores: Array<TrainingScore> = [];
  engagementScores: Array<TrainingScore> = [];
  creativityScores: Array<TrainingScore> = [];
  decisivenessScores: Array<TrainingScore> = [];
  awarenessScores: Array<TrainingScore> = [];

  ngOnInit(): void {
    this.ballControlScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.BallControl);
    this.leftFootPassAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.LeftFootPassAccuracy);
    this.rightFootPassAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.RightFootPassAccuracy);
    this.leftFootDribblingAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.LeftFootDribblingAccuracy);
    this.rightFootDribblingAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.RightFootDribblingAccuracy);
    this.leftFootBallReceivingAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.LeftFootBallReceivingAccuracy);
    this.rightFootBallReceivingAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.RightFootBallReceivingAccuracy);
    this.leftFootShotsAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.LeftFootShotsAccuracy);
    this.rightFootShotsAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.RightFootShotsAccuracy);
    this.headingAccuracyScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.HeadingAccuracy);
    this.oneVsOneScoreScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.OneVsOneScore);
    this.mobilityScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Mobility);
    this.strengthScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Strength);
    this.enduranceScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Endurance);
    this.agilityScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Agility);
    this.coordinationScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Coordination);
    this.concentrationScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Concentration);
    this.emotionsControlScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.EmotionsControl);
    this.selfconfidenceScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Selfconfidence);
    this.stressControlScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.StressControl);
    this.attitudeScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Attitude);
    this.communicationScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Communication);
    this.cooperationScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Cooperation);
    this.determinationScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Determination);
    this.disciplineScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Discipline);
    this.engagementScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Engagement);
    this.creativityScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Creativity);
    this.decisivenessScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Decisiveness);
    this.awarenessScores = this.trainingScores.filter(x => x.scoreType == TrainingScoreType.Awareness);
  }

  confirmDelete(id: string){
    this.confirmDeleteScore.emit(id);
  }

  confirmUpdate(id: string){
    this.confirmUpdateScore.emit(id);
  }

  confirmActivate(id: string){
    this.confirmActivateScore.emit(id);
  }

  confirmDeactivate(id: string){
    this.confirmDeactivateScore.emit(id);
  }
}
