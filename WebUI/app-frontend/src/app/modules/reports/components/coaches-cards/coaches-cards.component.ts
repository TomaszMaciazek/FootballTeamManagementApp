import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { SimpleCoach } from 'src/app/models/coach/simple-coach.model';
import { CoachCards } from 'src/app/models/report/coach-cards.model';
import { MonthlyCoachCards } from 'src/app/models/report/monthly-coach-cards.model';
import { CoachService } from 'src/app/services/coach.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-coaches-cards',
  templateUrl: './coaches-cards.component.html',
  styleUrls: ['./coaches-cards.component.scss']
})
export class CoachesCardsComponent implements OnInit {

  form: FormGroup;

  coaches: SimpleCoach[] = null;
  coachCards : CoachCards = null;

  cardCounts : number[] = [];

  coachMonthlyCards : MonthlyCoachCards = null;
  coachMonthlyRedCardsData: number[] = [];
  coachMonthlyYellowCardsData: number[] = [];

  selectedCoach: SimpleCoach;
  selectedCoachId: string = null;

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
    private coachService: CoachService,
    private reportService: ReportService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    this.coachService.getAllCoaches().then(res => {
      this.coaches = res;
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
      this.getMonthlyStatistics(this.selectedCoach.id, startString, endString);
    }
  }

  resetFilter(){
    this.form.controls['Start'].setValue(null);
    this.form.controls['End'].setValue(null);

    let now = new Date();
    let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
    var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
    this.getMonthlyStatistics(this.selectedCoach.id, previousMonthStart, thisMonthStart);
  }

  checkView($event: any){
    if(this.monthlyView && this.selectedCoach != null && this.coachMonthlyCards == null){
      let now = new Date();
      let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
      var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
      this.getMonthlyStatistics(this.selectedCoach.id, previousMonthStart, thisMonthStart);
    }
  }

  onCoachSelect($event){
    if(this.selectedCoach != null && this.selectedCoachId != this.selectedCoach.id){
      this.selectedCoachId = this.selectedCoach.id;
      this.spinner.show();
      this.reportService.getCoachCards(this.selectedCoach.id)
      .then(res => {
        this.coachCards = res;
        this.cardCounts = [res.yellowCardsCount, res.redCardsCount];
        if(this.monthlyView){
          this.resetFilter();
        }
        else{
          this.coachMonthlyCards = null;
          this.spinner.hide();
        }
      });
    }
  }

  getMonthlyStatistics(id: string, startDate: string, endDate: string){
    this.spinner.show();
    this.reportService.getCoachCardsMonthly(id, startDate, endDate)
    .then(res => {
      this.coachMonthlyCards = res;
      this.months = this.coachMonthlyCards.redCards.map(x =>
        x.month < 10 ? `0${x.month}-${x.year}` : `${x.month}-${x.year}`
      );
      this.coachMonthlyRedCardsData = this.coachMonthlyCards.redCards.map(x => x.count);
      this.coachMonthlyYellowCardsData = this.coachMonthlyCards.yellowCards.map(x => x.count);
      this.spinner.hide();
    });
  }

}
