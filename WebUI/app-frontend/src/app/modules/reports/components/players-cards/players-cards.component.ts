import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { SimpleSelectPlayer } from 'src/app/models/player/simple-select-player.model';
import { MonthlyPlayerCards } from 'src/app/models/report/monthly-player-cards.model';
import { PlayerCards } from 'src/app/models/report/player-cards.model';
import { PlayerService } from 'src/app/services/player.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'app-players-cards',
  templateUrl: './players-cards.component.html',
  styleUrls: ['./players-cards.component.scss']
})
export class PlayersCardsComponent implements OnInit {

  form: FormGroup;

  players: SimpleSelectPlayer[] = null;
  playerCards : PlayerCards = null;

  cardCounts : number[] = [];

  playerMonthlyCards : MonthlyPlayerCards = null;
  playerMonthlyRedCardsData: number[] = [];
  playerMonthlyYellowCardsData: number[] = [];

  selectedPlayer: SimpleSelectPlayer;
  selectedPlayerId: string = null;

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
      this.getMonthlyStatistics(this.selectedPlayer.id, startString, endString);
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
    if(this.monthlyView && this.selectedPlayer != null && this.playerMonthlyCards == null){
      let now = new Date();
      let thisMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth(), now.getDate())).toISOString();
      var previousMonthStart = new Date(Date.UTC(now.getFullYear(), now.getMonth()-2, 1)).toISOString();
      this.getMonthlyStatistics(this.selectedPlayer.id, previousMonthStart, thisMonthStart);
    }
  }

  onPlayerSelect($event){
    if(this.selectedPlayer != null && this.selectedPlayerId != this.selectedPlayer.id){
      this.selectedPlayerId = this.selectedPlayer.id;
      this.spinner.show();
      this.reportService.getPlayerCards(this.selectedPlayer.id)
      .then(res => {
        this.playerCards = res;
        this.cardCounts = [res.yellowCardsCount, res.redCardsCount];
        if(this.monthlyView){
          this.resetFilter();
        }
        else{
          this.playerMonthlyCards = null;
          this.spinner.hide();
        }
      });
    }
  }

  getMonthlyStatistics(id: string, startDate: string, endDate: string){
    this.spinner.show();
    this.reportService.getPlayerCardsMonthly(id, startDate, endDate)
    .then(res => {
      this.playerMonthlyCards = res;
      this.months = this.playerMonthlyCards.redCards.map(x =>
        x.month < 10 ? `0${x.month}-${x.year}` : `${x.month}-${x.year}`
      );
      this.playerMonthlyRedCardsData = this.playerMonthlyCards.redCards.map(x => x.count);
      this.playerMonthlyYellowCardsData = this.playerMonthlyCards.yellowCards.map(x => x.count);
      this.spinner.hide();
    });
  }

}
