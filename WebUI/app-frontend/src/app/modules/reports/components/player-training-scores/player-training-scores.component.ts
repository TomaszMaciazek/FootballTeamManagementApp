import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { TrainingScoreType } from 'src/app/enums/training-score-type';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { PlayerMonthlyTrainingScoreStatistics } from 'src/app/models/report/player-monthly-training-score-statistics.model';
import { PlayerTrainingScoreStatistics } from 'src/app/models/report/player-training-score-statistics.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { PlayerService } from 'src/app/services/player.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-player-training-scores',
  templateUrl: './player-training-scores.component.html',
  styleUrls: ['./player-training-scores.component.scss']
})
export class PlayerTrainingScoresComponent implements OnInit {

  form: FormGroup;
  months :string[] = [];
  
  scores : PlayerTrainingScoreStatistics;
  scoresMonthly: PlayerMonthlyTrainingScoreStatistics;
  players: SimpleSelectPlayer[] = null;
  selectedPlayer: SimpleSelectPlayer;

  globalData = [];
  monthlyAvgData = [];
  monthlyMinData = [];
  monthlyMaxData = [];
  monthlyMedianData = [];

  selectedTrainingScoreType: TrainingScoreType;

  monthlyView : boolean = false;
  pointsView : boolean = false;

  showFilters : boolean = false

  public typeOptions: SelectItem[] = [
    {label: "ball_control", value :TrainingScoreType.BallControl},
    {label: "left_foot_pass_accuracy", value :TrainingScoreType.LeftFootPassAccuracy},
    {label: "right_foot_pass_accuracy", value :TrainingScoreType.RightFootPassAccuracy},
    {label: "left_foot_dribbling_accuracy", value :TrainingScoreType.LeftFootDribblingAccuracy},
    {label: "right_foot_dribbling_accuracy", value :TrainingScoreType.RightFootDribblingAccuracy},
    {label: "left_foot_ball_receiving_accuracy", value :TrainingScoreType.LeftFootBallReceivingAccuracy},
    {label: "right_foot_ball_receiving_accuracy", value :TrainingScoreType.RightFootBallReceivingAccuracy},
    {label: "left_foot_shots_accuracy", value :TrainingScoreType.LeftFootShotsAccuracy},
    {label: "right_foot_shots_accuracy", value :TrainingScoreType.RightFootShotsAccuracy},
    {label: "heading_accuracy", value :TrainingScoreType.HeadingAccuracy},
    {label: "one_vs_one", value :TrainingScoreType.OneVsOneScore},
    {label: "mobility", value :TrainingScoreType.Mobility},
    {label: "strength", value :TrainingScoreType.Strength},
    {label: "endurance", value :TrainingScoreType.Endurance},
    {label: "agility", value :TrainingScoreType.Agility},
    {label: "coordination", value :TrainingScoreType.Coordination},
    {label: "concentration", value :TrainingScoreType.Concentration},
    {label: "emotions_control", value :TrainingScoreType.EmotionsControl},
    {label: "selfconfidence", value :TrainingScoreType.Selfconfidence},
    {label: "stress_control", value :TrainingScoreType.StressControl},
    {label: "attitude", value :TrainingScoreType.Attitude},
    {label: "communication", value :TrainingScoreType.Communication},
    {label: "cooperation", value :TrainingScoreType.Cooperation},
    {label: "determination", value :TrainingScoreType.Determination},
    {label: "discipline", value :TrainingScoreType.Discipline},
    {label: "engagement", value :TrainingScoreType.Engagement},
    {label: "creativity", value :TrainingScoreType.Creativity},
    {label: "decisiveness", value :TrainingScoreType.Decisiveness},
    {label: "awareness", value :TrainingScoreType.Awareness}
  ].sort((a: SelectItem, b: SelectItem) => {
    if (this.translationProvider.getTranslation(a.label) < this.translationProvider.getTranslation(b.label))
        return -1;
      if (this.translationProvider.getTranslation(a.label) > this.translationProvider.getTranslation(b.label))
        return 1;
      return 0;
  })

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
    if(this.monthlyView && this.scoresMonthly == null && this.selectedTrainingScoreType != null){
      let now = new Date();
      let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
      var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
      this.getStatistics(previousMonthStart, thisMonthStart);
    }
  }

  onPlayerSelect($event){
    if(this.selectedPlayer != null){
      this.spinner.show();
      this.reportService.getPlayerTrainingScores(this.selectedPlayer.id)
      .then(res => {
        this.scores = res;
        if(this.selectedTrainingScoreType != null){
          let score = this.scores.scores.find(x => x.scoreType == this.selectedTrainingScoreType);
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
    if(this.selectedTrainingScoreType != null && this.selectedPlayer){
      let score = this.scores.scores.find(x => x.scoreType == this.selectedTrainingScoreType);
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
    this.reportService.getPlayerTrainingScoresMonthly(this.selectedPlayer.id, startDate, endDate)
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
        let typeStats = x.scores.find(y => y.scoreType == this.selectedTrainingScoreType);
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
