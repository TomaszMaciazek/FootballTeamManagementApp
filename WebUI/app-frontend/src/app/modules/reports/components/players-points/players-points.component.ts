import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { SelectItem } from 'primeng/api';
import { MatchPointType } from 'src/app/enums/match-point-type';
import { MatchType } from 'src/app/enums/match-type';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { MonthlyPlayerPoints } from 'src/app/models/report/monthly-player-points.model';
import { PlayerPoints } from 'src/app/models/report/player-points.model';
import { PlayerService } from 'src/app/services/player.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-players-points',
  templateUrl: './players-points.component.html',
  styleUrls: ['./players-points.component.scss']
})
export class PlayersPointsComponent implements OnInit {

  playerPoints: PlayerPoints;

  players: SimpleSelectPlayer[] = null;
  selectedPlayer: SimpleSelectPlayer;

  months :string[] = [];
  monthsPointTypes: string[] = [];
  playerMatchesPointsData: number[] = [];
  playerOwnGoalsData : number[] = [];

  playerCupMatchesPointsData: number[] = [];
  playerLeagueMatchesPointsData: number[] = [];
  playerFriendlyMatchesPointsData: number[] = [];

  monthlyPlayerPoints: MonthlyPlayerPoints = null;
  playerMonthlyMatchesPointsData: number[] = [];
  playerMonthlyOwnGoalsData: number[] = [];

  playerMonthlyMatchesInGamePointsData: number[] = [];
  playerMonthlyMatchesOwnGoalsData: number[] = [];
  playerMonthlyMatchesFreeKickData: number[] = [];
  playerMonthlyMatchesCornerKickData: number[] = [];
  playerMonthlyMatchesPenaltyGoalsData: number[] = [];

  maxDate = new Date();

  matchTypes: SelectItem[] = [
    {label: 'cup_match', value: MatchType.Cup},
    {label: 'league_match', value: MatchType.League},
    {label: 'friendly_match', value: MatchType.Friendly}
  ];

  selectedMatchType: MatchType = null;

  public form: FormGroup;

  monthlyView : boolean = false;
  pointsView : boolean = false;

  showFilters : boolean = false

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

  chartViewPointOptions = [
    {label: 'global_view', value: false},
    {label: 'point_type_view', value: true}
  ];

  constructor(
    private spinner: NgxSpinnerService,
    private playerService: PlayerService,
    private reportService: ReportService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    this.playerService.getAllPlayers().then(res => {
      this.players = res;
      this.spinner.hide();
    });
  }

  createForm(){
    this.form = this.formBuilder.group({
      'Start': [null, Validators.required],
      'End': [null, Validators.required],
    });
  }

  filter(){
    if(this.form.valid){
      let start : Date = this.form.controls['Start'].value;
      let end : Date = this.form.controls['End'].value;
      let startString = new Date(Date.UTC(start.getFullYear(), start.getMonth(), start.getDate())).toISOString();
      var endString = new Date(Date.UTC(end.getFullYear(), end.getMonth(), end.getDate())).toISOString();
      this.getMonthlyStatistics(this.selectedPlayer.id,startString, endString);
    }
  }

  resetFilter(){
    this.form.controls['Start'].setValue(null);
    this.form.controls['End'].setValue(null);

    let now = new Date();
    let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
    var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
    this.getMonthlyStatistics(this.selectedPlayer.id, previousMonthStart, thisMonthStart);
  }

  checkView($event: any){
    debugger;
    if(this.monthlyView && this.selectedPlayer != null && (this.monthlyPlayerPoints == null || this.monthlyPlayerPoints.playerId != this.selectedPlayer.id)){
      let now = new Date();
      let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
      var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
      this.getMonthlyStatistics(this.selectedPlayer.id, previousMonthStart, thisMonthStart);
    }
  }

  onTypeSelect($event: any){
    if(this.monthlyView){
      this.fillMonthlyPointsTables();
    }
  }

  onPlayerSelect($event){
    if(this.selectedPlayer != null && this.monthlyPlayerPoints == null || this.monthlyPlayerPoints.playerId != this.selectedPlayer.id){
      this.spinner.show();
      this.reportService.getPlayerPoints(this.selectedPlayer.id)
      .then(res => {
        this.playerPoints = res;
        this.playerMatchesPointsData = [
        res.cupMatchesPoints.filter(x => x.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0),
        res.leagueMatchesPoints.filter(x => x.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0),
        res.friendlyMatchPoints.filter(x => x.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0)
        ];
      this.playerOwnGoalsData = [
        res.cupMatchesPoints.filter(x => x.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0),
        res.leagueMatchesPoints.filter(x => x.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0),
        res.friendlyMatchPoints.filter(x => x.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0)
      ];
      this.playerCupMatchesPointsData = res.cupMatchesPoints.map(x => x.count);
      this.playerLeagueMatchesPointsData = res.leagueMatchesPoints.map(x => x.count);
      this.playerFriendlyMatchesPointsData = res.friendlyMatchPoints.map(x => x.count);
        if(this.monthlyView){
          debugger;
          this.resetFilter();
        }
        else{
          this.playerMonthlyMatchesPointsData = null;
          this.spinner.hide();
        }
      });
    }
  }

  getMonthlyStatistics(playerId: string, startDate: string, endDate: string){
    this.spinner.show();
    this.reportService.getPlayerPointsMonthly(playerId, startDate, endDate)
    .then(res => {
      this.monthlyPlayerPoints = res;
      this.months = this.monthlyPlayerPoints.cupMatchesPoints.map(x =>
        x.month < 10 ? `0${x.month}-${x.year}` : `${x.month}-${x.year}`
      );

      var monthlyCupMatchesPoints = [];
      var monthlyCupMatchesOwnGoals = [];
      res.cupMatchesPoints.map(x => x.pointsByType).forEach(x => {
        monthlyCupMatchesPoints.push(x.filter(y => y.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0));
        monthlyCupMatchesOwnGoals.push(x.filter(y => y.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0))
      });

      var monthlyLeagueMatchesPoints = [];
      var monthlyLeagueMatchesOwnGoals = [];
      res.leagueMatchesPoints.map(x => x.pointsByType).forEach(x => {
        monthlyLeagueMatchesPoints.push(x.filter(y => y.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0));
        monthlyLeagueMatchesOwnGoals.push(x.filter(y => y.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0))
      });

      var monthlyFriendlyMatchesPoints = [];
      var monthlyFriendlyMatchesOwnGoals = [];
      res.friendlyMatchesPoints.map(x => x.pointsByType).forEach(x => {
        monthlyFriendlyMatchesPoints.push(x.filter(y => y.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0));
        monthlyFriendlyMatchesOwnGoals.push(x.filter(y => y.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0))
      });

      var clubMonthlyPointsDataSums = [];
      var clubMonthlyOwnGoalsDataSums = [];

      for(var i = 0; i< this.months.length; i++){
        clubMonthlyPointsDataSums.push(monthlyFriendlyMatchesPoints[i] + monthlyLeagueMatchesPoints[i] + monthlyCupMatchesPoints[i]);
        clubMonthlyOwnGoalsDataSums.push(monthlyFriendlyMatchesOwnGoals[i] + monthlyLeagueMatchesOwnGoals[i] + monthlyCupMatchesOwnGoals[i])
      }

      this.playerMonthlyMatchesPointsData = clubMonthlyPointsDataSums;
      this.playerMonthlyOwnGoalsData = clubMonthlyOwnGoalsDataSums;

      if(this.monthlyView && this.selectedMatchType != null){
        this.fillMonthlyPointsTables();
      }
      this.spinner.hide();
    });
  }

  fillMonthlyPointsTables(){
    var inGamePoints = [];
    var freeKickPoints = [];
    var penaltyPoints = [];
    var cornerKickPoints = [];
    var ownGoals = [];
    if(this.selectedMatchType == MatchType.Cup){
      this.monthlyPlayerPoints.cupMatchesPoints.forEach(x => {
        inGamePoints.push(x.pointsByType.filter(x => x.type == MatchPointType.InGame).map(x => x.count));
        cornerKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.CornerKick).map(x => x.count));
        freeKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.FreeKick).map(x => x.count));
        penaltyPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.Penalty).map(x => x.count));
        ownGoals.push(x.pointsByType.filter(x => x.type == MatchPointType.Own).map(x => x.count));
      });
    }
    else if(this.selectedMatchType == MatchType.League){
      this.monthlyPlayerPoints.leagueMatchesPoints.forEach(x => {
        inGamePoints.push(x.pointsByType.filter(x => x.type == MatchPointType.InGame).map(x => x.count));
        cornerKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.CornerKick).map(x => x.count));
        freeKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.FreeKick).map(x => x.count));
        penaltyPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.Penalty).map(x => x.count));
        ownGoals.push(x.pointsByType.filter(x => x.type == MatchPointType.Own).map(x => x.count));
      });
    }
    else if(this.selectedMatchType == MatchType.Friendly){
      this.monthlyPlayerPoints.friendlyMatchesPoints.forEach(x => {
        inGamePoints.push(x.pointsByType.filter(x => x.type == MatchPointType.InGame).map(x => x.count));
        cornerKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.CornerKick).map(x => x.count));
        freeKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.FreeKick).map(x => x.count));
        penaltyPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.Penalty).map(x => x.count));
        ownGoals.push(x.pointsByType.filter(x => x.type == MatchPointType.Own).map(x => x.count));
      });
    }

    this.playerMonthlyMatchesInGamePointsData = inGamePoints;
    this.playerMonthlyMatchesCornerKickData = cornerKickPoints;
    this.playerMonthlyMatchesFreeKickData = freeKickPoints;
    this.playerMonthlyMatchesPenaltyGoalsData = penaltyPoints;
    this.playerMonthlyMatchesOwnGoalsData = ownGoals;
  }

}
