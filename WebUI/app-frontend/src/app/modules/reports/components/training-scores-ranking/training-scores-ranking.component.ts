import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { TrainingScoreType } from 'src/app/enums/training-score-type';
import { TrainingScoreRankingQuery } from 'src/app/models/queries/training-score-ranking-query.model';
import { PlayersTrainingScoresRanking } from 'src/app/models/report/players-training-scores-ranking.model';
import { SimpleTeam } from 'src/app/models/team/simple-team.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { ReportService } from 'src/app/services/report.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-training-scores-ranking',
  templateUrl: './training-scores-ranking.component.html',
  styleUrls: ['./training-scores-ranking.component.scss']
})
export class TrainingScoresRankingComponent implements OnInit {

  form : FormGroup;
  ranking: PlayersTrainingScoresRanking = null;
  maxDate = new Date();
  playerNames: string[] = [];
  playerScores: number[] = [];

  teams: SimpleTeam[];

  boolFilterOptions = [
    {label: 'both', value: null},
    {label: 'yes', value: true},
    {label: 'no', value: false}
  ];

  constructor(
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private reportService: ReportService,
    private teamService: TeamService,
    private translationProvider: TranslationProvider,
    private formBuilder: FormBuilder
  ) { }

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

  ngOnInit(): void {
    this.createForm();
    this.spinner.show();
    this.teamService.getAllTeams()
    .then(res => {
      this.teams = [];
      let emptyTeam = new SimpleTeam;
      emptyTeam.id = null;
      emptyTeam.name = 'players_without_team';
      this.teams.push(emptyTeam);
      res.forEach(x => this.teams.push(x));
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
      'ScoreType': [null, Validators.required],
      'Count': [10, Validators.required],
      'OnlyPlaying': [null, Validators.nullValidator],
      'Teams': [null, Validators.nullValidator]
    });
  }

  submit(){
    if(this.form.valid){
      let start : Date = this.form.controls['Start'].value;
      let end : Date = this.form.controls['End'].value;
      let startString = new Date(Date.UTC(start.getFullYear(), start.getMonth(), start.getDate())).toISOString();
      var endString = new Date(Date.UTC(end.getFullYear(), end.getMonth(), end.getDate())).toISOString();
      let scoreType = this.form.controls['ScoreType'].value;
      let count = this.form.controls['Count'].value;
      let onlyPlaying = this.form.controls['OnlyPlaying'].value;
      var teamsIds = null;
      var getPlayersWithoutTeam = false
      if(this.form.controls['Teams'].value != null){
        let teams = this.form.controls['Teams'].value;
        if(teams.filter(x => x.id == null).length > 0){
          getPlayersWithoutTeam = true;
        }
        teamsIds = teams.filter(x => x.id != null).map(x => x.id);
      }
      let query = new TrainingScoreRankingQuery();
      query.count = count;
      query.scoreType = scoreType;
      query.onlyStillPlaying = onlyPlaying;
      query.startDate = startString;
      query.endDate = endString;
      query.teamsIds = teamsIds;
      query.getPlayersWithoutTeam = getPlayersWithoutTeam;
      this.getRanking(query);
    }
  }

  getRanking(query: TrainingScoreRankingQuery){
    this.spinner.show();
    this.reportService.getPlayersTrainingScoreRanking(query)
    .then(res => {
      this.ranking = res;
      this.playerNames = this.ranking.playerScores.map(x => `${x.player.name} ${x.player.middleName} ${x.player.surname}`);
      this.playerScores = this.ranking.playerScores.map(x => x.avg);
      this.spinner.hide();
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });
  }

}
