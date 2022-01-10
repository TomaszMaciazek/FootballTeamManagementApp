import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { SelectItem } from 'primeng/api';
import { MatchPointType } from 'src/app/enums/match-point-type';
import { MatchType } from 'src/app/enums/match-type';
import { MonthlyTeamPoints } from 'src/app/models/report/monthly-team-points.model';
import { TeamPoints } from 'src/app/models/report/team-points.model';
import { SimpleTeam } from 'src/app/models/team/simple-team.model';
import { ReportService } from 'src/app/services/report.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-teams-points',
  templateUrl: './teams-points.component.html',
  styleUrls: ['./teams-points.component.scss']
})
export class TeamsPointsComponent implements OnInit {

  teamPoints: TeamPoints;

  teams: SimpleTeam[] = null;
  selectedTeam: SimpleTeam;

  months :string[] = [];
  monthsPointTypes: string[] = [];
  teamMatchesPointsData: number[] = [];
  teamOwnGoalsData : number[] = [];

  teamCupMatchesPointsData: number[] = [];
  teamLeagueMatchesPointsData: number[] = [];
  teamFriendlyMatchesPointsData: number[] = [];

  monthlyTeamPoints: MonthlyTeamPoints = null;
  teamMonthlyMatchesPointsData: number[] = [];
  teamMonthlyOwnGoalsData: number[] = [];

  teamMonthlyMatchesInGamePointsData: number[] = [];
  teamMonthlyMatchesOwnGoalsData: number[] = [];
  teamMonthlyMatchesFreeKickData: number[] = [];
  teamMonthlyMatchesCornerKickData: number[] = [];
  teamMonthlyMatchesPenaltyGoalsData: number[] = [];

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
    private teamService: TeamService,
    private reportService: ReportService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    this.teamService.getAllTeams().then(res => {
      this.teams = res;
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
      this.getMonthlyStatistics(this.selectedTeam.id,startString, endString);
    }
  }

  resetFilter(){
    this.form.controls['Start'].setValue(null);
    this.form.controls['End'].setValue(null);

    let now = new Date();
    let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
    var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
    this.getMonthlyStatistics(this.selectedTeam.id, previousMonthStart, thisMonthStart);
  }

  checkView($event: any){
    if(this.monthlyView && this.selectedTeam != null && (this.monthlyTeamPoints == null || this.monthlyTeamPoints.teamId != this.selectedTeam.id)){
      let now = new Date();
      let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
      var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
      this.getMonthlyStatistics(this.selectedTeam.id, previousMonthStart, thisMonthStart);
    }
  }

  onTypeSelect($event: any){
    if(this.monthlyView){
      this.fillMonthlyPointsTables();
    }
  }

  onTeamSelect($event){
    if(this.selectedTeam != null && this.monthlyTeamPoints == null || this.monthlyTeamPoints.teamId != this.selectedTeam.id){
      this.spinner.show();
      this.reportService.getTeamPoints(this.selectedTeam.id)
      .then(res => {
        this.teamPoints = res;
        this.teamMatchesPointsData = [
        res.cupMatchesPoints.filter(x => x.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0),
        res.leagueMatchesPoints.filter(x => x.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0),
        res.friendlyMatchPoints.filter(x => x.type != MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0)
        ];
      this.teamOwnGoalsData = [
        res.cupMatchesPoints.filter(x => x.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0),
        res.leagueMatchesPoints.filter(x => x.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0),
        res.friendlyMatchPoints.filter(x => x.type == MatchPointType.Own).map(x => x.count).reduce((acc, curr) => acc + curr, 0)
      ];
      this.teamCupMatchesPointsData = res.cupMatchesPoints.map(x => x.count);
      this.teamLeagueMatchesPointsData = res.leagueMatchesPoints.map(x => x.count);
      this.teamFriendlyMatchesPointsData = res.friendlyMatchPoints.map(x => x.count);
        if(this.monthlyView){
          this.resetFilter();
        }
        else{
          this.teamMonthlyMatchesPointsData = null;
          this.spinner.hide();
        }
      });
    }
  }

  getMonthlyStatistics(playerId: string, startDate: string, endDate: string){
    this.spinner.show();
    this.reportService.getTeamPointsMonthly(playerId, startDate, endDate)
    .then(res => {
      this.monthlyTeamPoints = res;
      this.months = this.monthlyTeamPoints.cupMatchesPoints.map(x =>
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

      this.teamMonthlyMatchesPointsData = clubMonthlyPointsDataSums;
      this.teamMonthlyOwnGoalsData = clubMonthlyOwnGoalsDataSums;

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
      this.monthlyTeamPoints.cupMatchesPoints.forEach(x => {
        inGamePoints.push(x.pointsByType.filter(x => x.type == MatchPointType.InGame).map(x => x.count));
        cornerKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.CornerKick).map(x => x.count));
        freeKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.FreeKick).map(x => x.count));
        penaltyPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.Penalty).map(x => x.count));
        ownGoals.push(x.pointsByType.filter(x => x.type == MatchPointType.Own).map(x => x.count));
      });
    }
    else if(this.selectedMatchType == MatchType.League){
      this.monthlyTeamPoints.leagueMatchesPoints.forEach(x => {
        inGamePoints.push(x.pointsByType.filter(x => x.type == MatchPointType.InGame).map(x => x.count));
        cornerKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.CornerKick).map(x => x.count));
        freeKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.FreeKick).map(x => x.count));
        penaltyPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.Penalty).map(x => x.count));
        ownGoals.push(x.pointsByType.filter(x => x.type == MatchPointType.Own).map(x => x.count));
      });
    }
    else if(this.selectedMatchType == MatchType.Friendly){
      this.monthlyTeamPoints.friendlyMatchesPoints.forEach(x => {
        inGamePoints.push(x.pointsByType.filter(x => x.type == MatchPointType.InGame).map(x => x.count));
        cornerKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.CornerKick).map(x => x.count));
        freeKickPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.FreeKick).map(x => x.count));
        penaltyPoints.push(x.pointsByType.filter(x => x.type == MatchPointType.Penalty).map(x => x.count));
        ownGoals.push(x.pointsByType.filter(x => x.type == MatchPointType.Own).map(x => x.count));
      });
    }

    this.teamMonthlyMatchesInGamePointsData = inGamePoints;
    this.teamMonthlyMatchesCornerKickData = cornerKickPoints;
    this.teamMonthlyMatchesFreeKickData = freeKickPoints;
    this.teamMonthlyMatchesPenaltyGoalsData = penaltyPoints;
    this.teamMonthlyMatchesOwnGoalsData = ownGoals;
  }

}
