import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { MonthlyTeamCards } from 'src/app/models/report/monthly-team-cards.model';
import { TeamCards } from 'src/app/models/report/team-cards.model';
import { SimpleTeam } from 'src/app/models/team/simple-team.model';
import { ReportService } from 'src/app/services/report.service';
import { TeamService } from 'src/app/services/team.service';

@Component({
  selector: 'app-teams-cards',
  templateUrl: './teams-cards.component.html',
  styleUrls: ['./teams-cards.component.scss']
})
export class TeamsCardsComponent implements OnInit {

  form: FormGroup;

  teams: SimpleTeam[] = null;
  teamCards : TeamCards = null;

  cardCounts : number[] = [];

  teamMonthlyCards : MonthlyTeamCards = null;
  teamMonthlyRedCardsData: number[] = [];
  teamMonthlyYellowCardsData: number[] = [];

  selectedTeam: SimpleTeam;
  selectedTeamId: string = null;

  maxDate = new Date();

  monthlyView : boolean = false;

  showFilters : boolean = false;
  months :string[] = [];

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
  ]

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
      this.getMonthlyStatistics(this.selectedTeam.id, startString, endString);
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
    if(this.monthlyView && this.selectedTeam != null && this.teamMonthlyCards == null){
      let now = new Date();
      let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
      var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
      this.getMonthlyStatistics(this.selectedTeam.id, previousMonthStart, thisMonthStart);
    }
  }

  onTeamSelect($event){
    if(this.selectedTeam != null && this.selectedTeamId != this.selectedTeam.id){
      this.selectedTeamId = this.selectedTeam.id;
      this.spinner.show();
      this.reportService.getTeamCards(this.selectedTeam.id)
      .then(res => {
        this.teamCards = res;
        this.cardCounts = [res.yellowCardsCount, res.redCardsCount];
        if(this.monthlyView){
          this.resetFilter();
        }
        else{
          this.teamMonthlyCards = null;
          this.spinner.hide();
        }
      });
    }
  }

  getMonthlyStatistics(id: string, startDate: string, endDate: string){
    this.spinner.show();
    this.reportService.getTeamCardsMonthly(id, startDate, endDate)
    .then(res => {
      this.teamMonthlyCards = res;
      this.months = this.teamMonthlyCards.redCards.map(x =>
        x.month < 10 ? `0${x.month}-${x.year}` : `${x.month}-${x.year}`
      );
      this.teamMonthlyRedCardsData = this.teamMonthlyCards.redCards.map(x => x.count);
      this.teamMonthlyYellowCardsData = this.teamMonthlyCards.yellowCards.map(x => x.count);
      this.spinner.hide();
    });
  }

}
