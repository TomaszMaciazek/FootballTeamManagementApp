import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { MatchScoreType } from 'src/app/enums/match-score-type';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { PlayerMatchScoreStatistics } from 'src/app/models/report/player-match-score-statistics.model';
import { PlayerMonthlyMatchScoreStatistics } from 'src/app/models/report/player-monthly-match-score-statistics.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { PlayerService } from 'src/app/services/player.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-player-match-scores',
  templateUrl: './player-match-scores.component.html',
  styleUrls: ['./player-match-scores.component.scss']
})
export class PlayerMatchScoresComponent implements OnInit {

  form: FormGroup;
  months :string[] = [];
  
  scores : PlayerMatchScoreStatistics;
  scoresMonthly: PlayerMonthlyMatchScoreStatistics;
  players: SimpleSelectPlayer[] = null;
  selectedPlayer: SimpleSelectPlayer;

  globalData = [];
  monthlyAvgData = [];
  monthlyMinData = [];
  monthlyMaxData = [];
  monthlyMedianData = [];

  selectedMatchScoreType: MatchScoreType;

  monthlyView : boolean = false;
  pointsView : boolean = false;

  showFilters : boolean = false

  public typeOptions: SelectItem[] = [
    {label: "tactical_performance", value: MatchScoreType.TacticalPerformance},
    {label: "ball_control", value: MatchScoreType.BallControl},
    {label: "left_foot_pass_accuracy", value: MatchScoreType.LeftFootPassAccuracy},
    {label: "right_foot_pass_accuracy", value: MatchScoreType.RightFootPassAccuracy},
    {label: "left_foot_dribbling_accuracy", value: MatchScoreType.LeftFootDribblingAccuracy},
    {label: "right_foot_dribbling_accuracy", value: MatchScoreType.RightFootDribblingAccuracy},
    {label: "left_foot_ball_receiving_accuracy", value: MatchScoreType.LeftFootBallReceivingAccuracy},
    {label: "right_foot_ball_receiving_accuracy", value: MatchScoreType.RightFootBallReceivingAccuracy},
    {label: "left_foot_shots_accuracy", value: MatchScoreType.LeftFootShotsAccuracy},
    {label: "right_foot_shots_accuracy", value: MatchScoreType.RightFootShotsAccuracy},
    {label: "heading_accuracy", value: MatchScoreType.HeadingAccuracy},
    {label: "one_vs_one", value: MatchScoreType.OneVsOneScore},
    {label: "mobility", value: MatchScoreType.Mobility},
    {label: "strength", value: MatchScoreType.Strength},
    {label: "endurance", value: MatchScoreType.Endurance},
    {label: "agility", value: MatchScoreType.Agility},
    {label: "coordination", value: MatchScoreType.Coordination},
    {label: "concentration", value: MatchScoreType.Concentration},
    {label: "emotions_control", value: MatchScoreType.EmotionsControl},
    {label: "selfconfidence", value: MatchScoreType.Selfconfidence},
    {label: "stress_control", value: MatchScoreType.StressControl},
    {label: "attitude", value: MatchScoreType.Attitude},
    {label: "communication", value: MatchScoreType.Communication},
    {label: "cooperation", value: MatchScoreType.Cooperation},
    {label: "determination", value: MatchScoreType.Determination},
    {label: "discipline", value: MatchScoreType.Discipline},
    {label: "engagement", value: MatchScoreType.Engagement},
    {label: "creativity", value: MatchScoreType.Creativity},
    {label: "decisiveness", value: MatchScoreType.Decisiveness},
    {label: "awareness", value: MatchScoreType.Awareness}
  ].sort((a: SelectItem, b: SelectItem) => {
    if (this.translationProvider.getTranslation(a.label) < this.translationProvider.getTranslation(b.label))
        return -1;
      if (this.translationProvider.getTranslation(a.label) > this.translationProvider.getTranslation(b.label))
        return 1;
      return 0;
  });

  basicOptions = {
    plugins: {
        legend: {
            position: 'bottom',
            labels: {
                color: '#495057'
            }
        }
    },
    scales: {
        x: {
            ticks: {
                color: '#495057'
            },
            grid: {
                color: '#ebedef'
            }
        },
        y: {
            ticks: {
                precision: 0,
                color: '#495057'
            },
            grid: {
                color: '#ebedef'
            }
        }
    },
  };

  chartViewOptions = [
    {label: 'global_chart', value: false},
    {label: 'monthly_chart', value: true}
  ];

  constructor(
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private reportService: ReportService,
    private playerService: PlayerService,
    private translationProvider: TranslationProvider,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    this.playerService.getAllPlayers().then(res => {
      this.players = res;
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  createForm(){
    this.form = this.formBuilder.group({
      'Start': [null, Validators.required],
      'End': [null, Validators.required],
    });
  }

  checkView($event: any){
    if(this.monthlyView && this.scoresMonthly == null && this.selectedMatchScoreType != null){
      let now = new Date();
      let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
      var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
      this.getStatistics(previousMonthStart, thisMonthStart);
    }
  }

  onPlayerSelect($event){
    if(this.selectedPlayer != null){
      this.spinner.show();
      this.reportService.getPlayerMatchScores(this.selectedPlayer.id)
      .then(res => {
        this.scores = res;
        if(this.selectedMatchScoreType != null){
          let score = this.scores.scores.find(x => x.scoreType == this.selectedMatchScoreType);
          this.globalData = [score.avg, score.min, score.max, score.median];
          if(!this.scoresMonthly || this.scoresMonthly.playerId != this.selectedPlayer.id){
            this.resetFilter();
          }
          else{
            this.filterMontlyData();
          }
        }
        this.spinner.hide();
      })
      .catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
      });
    }
  }

  onScoreTypeSelect($event){
    if(this.selectedMatchScoreType != null && this.selectedPlayer){
      let score = this.scores.scores.find(x => x.scoreType == this.selectedMatchScoreType);
      this.globalData = [score.avg, score.min, score.max, score.median];
      if(!this.scoresMonthly){
        this.resetFilter();
      }
      else{
        this.filterMontlyData();
      }
    }
  }

  filter(){
    if(this.form.valid){
      let start : Date = this.form.controls['Start'].value;
      let end : Date = this.form.controls['End'].value;
      let startString = new Date(Date.UTC(start.getFullYear(), start.getMonth(), start.getDate())).toISOString();
      var endString = new Date(Date.UTC(end.getFullYear(), end.getMonth(), end.getDate())).toISOString();
      this.getStatistics(startString, endString);
    }
  }

  resetFilter(){
    this.form.controls['Start'].setValue(null);
    this.form.controls['End'].setValue(null);

    let now = new Date();
    let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
    var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
    this.getStatistics(previousMonthStart, thisMonthStart);
  }

  getStatistics(startDate: string, endDate: string){
    this.spinner.show();
    this.reportService.getPlayerMatchScoresMonthly(this.selectedPlayer.id, startDate, endDate)
    .then(res => {
      this.scoresMonthly = res;
      this.months = this.scoresMonthly.scoreStatistics.map(x =>
        x.month < 10 ? `0${x.month}-${x.year}` : `${x.month}-${x.year}`
      );
      this.filterMontlyData();
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

  filterMontlyData(){
    var avgResults = [];
      var minResults = [];
      var maxResults = [];
      var medianResults = [];
      this.scoresMonthly.scoreStatistics.forEach(x =>{
        let typeStats = x.scores.find(y => y.scoreType == this.selectedMatchScoreType);
        avgResults.push(typeStats.avg);
        minResults.push(typeStats.min);
        maxResults.push(typeStats.max)
        medianResults.push(typeStats.median);
      })
      this.monthlyAvgData = avgResults;
      this.monthlyMinData = minResults;
      this.monthlyMaxData = maxResults;
      this.monthlyMedianData = medianResults;
  }

}
