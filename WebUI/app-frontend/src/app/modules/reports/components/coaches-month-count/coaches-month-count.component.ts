import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { MonthDataCountStatistics } from 'src/app/models/report/month-data-count-statistics.model';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-coaches-month-count',
  templateUrl: './coaches-month-count.component.html',
  styleUrls: ['./coaches-month-count.component.scss']
})
export class CoachesMonthCountComponent implements OnInit {

  statistics: MonthDataCountStatistics[];

  months :string[] = [];
  data: number[] = [];

  maxDate = new Date();

  public form: FormGroup;

  showFilters : boolean = false

  basicOptions = {
    plugins: {
        legend: {
            labels: {
                color: '#495057'
            },
            position: 'bottom',
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
    }
};

  constructor(
    private spinner: NgxSpinnerService,
    private reportService: ReportService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.createForm();
    let now = new Date();
    let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
    var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
    this.getStatistics(previousMonthStart, thisMonthStart);
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

  getStatistics(startDate: string, endDate: string){
    this.spinner.show();
    this.reportService.getNewCoachesCount(startDate, endDate)
    .then(res => {
      this.statistics = res;
      this.months = this.statistics.map(x =>
        x.month < 10 ? `0${x.month}-${x.year}` : `${x.month}-${x.year}`
      );
      this.data = this.statistics.map(x => x.count);
      this.spinner.hide();
    });
  }

}
