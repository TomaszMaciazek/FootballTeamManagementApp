import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ClubCards } from 'src/app/models/report/club-cards.model';
import { MonthlyClubCards } from 'src/app/models/report/monthly-club-cards.model';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-club-cards',
  templateUrl: './club-cards.component.html',
  styleUrls: ['./club-cards.component.scss']
})
export class ClubCardsComponent implements OnInit {

  clubCards: ClubCards;

  months :string[] = [];
  clubPlayersCardsData: number[] = [];
  clubCoachesCardsData: number[] = [];

  monthlyClubCards: MonthlyClubCards = null;
  clubPlayersMonthlyRedCardsData: number[] = [];
  clubPlayersMonthlyYellowCardsData: number[] = [];
  clubCoachesMonthlyRedCardsData: number[] = [];
  clubCoachesMonthlyYellowCardsData: number[] = [];

  maxDate = new Date();

  public form: FormGroup;

  monthlyView : boolean = false;

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
  ]

  constructor(
    private spinner: NgxSpinnerService,
    private reportService: ReportService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.createForm();
    this.spinner.show();
    this.reportService.getClubCards().then(res => {
      this.clubCards = res;
      this.clubPlayersCardsData.push(res.playersYellowCards, res.playersRedCards);
      this.clubCoachesCardsData.push(res.coachesYellowCards, res.coachesRedCards);
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

  checkView($event: any){
    if(this.monthlyView && this.monthlyClubCards == null){
      let now = new Date();
      let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
      var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
      this.getStatistics(previousMonthStart, thisMonthStart);
    }
  }

  getStatistics(startDate: string, endDate: string){
    this.spinner.show();
    this.reportService.getClubCardsMonthly(startDate, endDate)
    .then(res => {
      this.monthlyClubCards = res;
      this.months = this.monthlyClubCards.coachesRedCards.map(x =>
        x.month < 10 ? `0${x.month}-${x.year}` : `${x.month}-${x.year}`
      );
      this.clubPlayersMonthlyRedCardsData = this.monthlyClubCards.playersRedCards.map(x => x.count);
      this.clubPlayersMonthlyYellowCardsData = this.monthlyClubCards.playersYellowCards.map(x => x.count);
      this.clubCoachesMonthlyRedCardsData = this.monthlyClubCards.coachesRedCards.map(x => x.count);
      this.clubCoachesMonthlyYellowCardsData = this.monthlyClubCards.coachesYellowCards.map(x => x.count);
      this.spinner.hide();
    });
  }

}
