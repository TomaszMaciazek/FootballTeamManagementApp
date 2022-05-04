import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SelectItem } from 'primeng/api';
import { MatchScoreType } from 'src/app/enums/match-score-type';
import { MatchScoreRankingQuery } from 'src/app/models/queries/match-score-ranking-query.model';
import { PlayersMatchScoresRanking } from 'src/app/models/report/players-match-scores-ranking.model';
import { SimpleTeam } from 'src/app/models/team/simple-team.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { ReportService } from 'src/app/services/report.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-match-scores-ranking',
  templateUrl: './match-scores-ranking.component.html',
  styleUrls: ['./match-scores-ranking.component.scss']
})
export class MatchScoresRankingComponent implements OnInit {

  form : FormGroup;
  ranking: PlayersMatchScoresRanking = null;
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
      'Teams': [null, Validators.nullValidator],
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
      let query = new MatchScoreRankingQuery();
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

  getRanking(query: MatchScoreRankingQuery){
    this.spinner.show();
    this.reportService.getPlayersMatchScoreRanking(query)
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
