import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatchScoreType } from 'src/app/enums/match-score-type';
import { MatchScore } from 'src/app/models/match-score.model';
import { MatchPlayer } from 'src/app/models/player/match-player';

@Component({
  selector: 'match-scores',
  templateUrl: './match-scores.component.html',
  styleUrls: ['./match-scores.component.scss']
})
export class MatchScoresComponent implements OnInit {

  @Input() matchScores: Array<MatchScore>;
  @Input() players: MatchPlayer[];

  @Output() confirmDeleteScore = new EventEmitter<string>();
  @Output() confirmUpdateScore = new EventEmitter<string>();
  @Output() confirmDeactivateScore = new EventEmitter<string>();
  @Output() confirmActivateScore = new EventEmitter<string>();

  tacticalPerformanceScores: Array<MatchScore>;
  ballControlScores: Array<MatchScore> = [];
  leftFootPassAccuracyScores: Array<MatchScore> = [];
  rightFootPassAccuracyScores: Array<MatchScore> = [];
  leftFootDribblingAccuracyScores: Array<MatchScore> = [];
  rightFootDribblingAccuracyScores: Array<MatchScore> = [];
  leftFootBallReceivingAccuracyScores: Array<MatchScore> = [];
  rightFootBallReceivingAccuracyScores: Array<MatchScore> = [];
  leftFootShotsAccuracyScores: Array<MatchScore> = [];
  rightFootShotsAccuracyScores: Array<MatchScore> = [];
  headingAccuracyScores: Array<MatchScore> = [];
  oneVsOneScoreScores: Array<MatchScore> = [];
  mobilityScores: Array<MatchScore> = [];
  strengthScores: Array<MatchScore> = [];
  enduranceScores: Array<MatchScore> = [];
  agilityScores: Array<MatchScore> = [];
  coordinationScores: Array<MatchScore> = [];
  concentrationScores: Array<MatchScore> = [];
  emotionsControlScores: Array<MatchScore> = [];
  selfconfidenceScores: Array<MatchScore> = [];
  stressControlScores: Array<MatchScore> = [];
  attitudeScores: Array<MatchScore> = [];
  communicationScores: Array<MatchScore> = [];
  cooperationScores: Array<MatchScore> = [];
  determinationScores: Array<MatchScore> = [];
  disciplineScores: Array<MatchScore> = [];
  engagementScores: Array<MatchScore> = [];
  creativityScores: Array<MatchScore> = [];
  decisivenessScores: Array<MatchScore> = [];
  awarenessScores: Array<MatchScore> = [];

  ngOnInit(): void {
    this.tacticalPerformanceScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.TacticalPerformance);
    this.ballControlScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.BallControl);
    this.leftFootPassAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.LeftFootPassAccuracy);
    this.rightFootPassAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.RightFootPassAccuracy);
    this.leftFootDribblingAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.LeftFootDribblingAccuracy);
    this.rightFootDribblingAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.RightFootDribblingAccuracy);
    this.leftFootBallReceivingAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.LeftFootBallReceivingAccuracy);
    this.rightFootBallReceivingAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.RightFootBallReceivingAccuracy);
    this.leftFootShotsAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.LeftFootShotsAccuracy);
    this.rightFootShotsAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.RightFootShotsAccuracy);
    this.headingAccuracyScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.HeadingAccuracy);
    this.oneVsOneScoreScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.OneVsOneScore);
    this.mobilityScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Mobility);
    this.strengthScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Strength);
    this.enduranceScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Endurance);
    this.agilityScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Agility);
    this.coordinationScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Coordination);
    this.concentrationScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Concentration);
    this.emotionsControlScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.EmotionsControl);
    this.selfconfidenceScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Selfconfidence);
    this.stressControlScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.StressControl);
    this.attitudeScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Attitude);
    this.communicationScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Communication);
    this.cooperationScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Cooperation);
    this.determinationScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Determination);
    this.disciplineScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Discipline);
    this.engagementScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Engagement);
    this.creativityScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Creativity);
    this.decisivenessScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Decisiveness);
    this.awarenessScores = this.matchScores.filter(x => x.scoreType == MatchScoreType.Awareness);
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
