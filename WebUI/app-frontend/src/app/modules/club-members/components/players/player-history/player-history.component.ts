import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { PlayerHistoryEventType } from 'src/app/enums/player-history-event-type';
import { PlayerHistoryEvent } from 'src/app/models/history/player-history-event.model';
import { PlayerHistory } from 'src/app/models/history/player-history.model';
import { PlayerHistoryService } from 'src/app/services/player-history.service';

@Component({
  selector: 'app-player-history',
  templateUrl: './player-history.component.html',
  styleUrls: ['./player-history.component.scss']
})
export class PlayerHistoryComponent implements OnInit {

  id: string;
  history : PlayerHistory;
  events: PlayerHistoryEvent[];
  filteredEvents: PlayerHistoryEvent[];

  playerHistoryEventType = PlayerHistoryEventType;

  minDate: Date;
  maxDate: Date;

  public form: FormGroup;

  showFilters: boolean = false;

  compareEvent = (a: PlayerHistoryEvent, b: PlayerHistoryEvent) => {
    if(a.eventType == PlayerHistoryEventType.Created){
      return -1;
    }
    if(b.eventType == PlayerHistoryEventType.Created){
      return 1;
    }
    if (a.date < b.date)
      return -1;
    if (a.date > b.date)
      return 1;
    if(a.date == b.date){
      if(a.eventType > b.eventType){
        return -1;
      }
      else if(a.eventType < b.eventType){
        return 1;
      }
    }
    return 0;
  };

  constructor(
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private playerHistoryService: PlayerHistoryService,
    private formBuilder: FormBuilder
  ) { }

  createForm(){
    this.form = this.formBuilder.group({
      'MinDate': [null, Validators.nullValidator],
      'MaxDate': [null, Validators.nullValidator]
    });
  }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.id = routeParams['id'];
      this.playerHistoryService.getPlayerHistory(this.id).then(res => {
        this.history = res;
        console.log(this.history);
        this.prepareEvents(this.history);
        this.spinner.hide();
      });
    });
  }

  filter(){
    
    if(this.form.controls['MinDate'].value != null){
      this.minDate = this.form.controls['MinDate'].value;
    }
    else{
      this.minDate = null;
    }

    if(this.form.controls['MaxDate'].value != null){
      this.maxDate = this.form.controls['MaxDate'].value;
    }
    else{
      this.maxDate = null;
    }
  }

  resetFilter() {
    this.minDate = null;
    this.maxDate = null;

    this.form.controls['MinDate'].setValue(null);
    this.form.controls['MaxDate'].setValue(null);
  }

  prepareEvents(history: PlayerHistory){
    this.events = [];
    this.events.push(new PlayerHistoryEvent({id: history.id, date: history.created, eventType: PlayerHistoryEventType.Created}));


    if(history.matchEvents){
      history.matchEvents.forEach(x => this.events.push(new PlayerHistoryEvent({id: x.id, date: x.match.date, eventType: PlayerHistoryEventType.MatchPlayed})));
    }

    if(history.playerJoinedTeamEvents){
      history.playerJoinedTeamEvents.forEach(x => this.events.push(new PlayerHistoryEvent({id: x.id, date: x.date, eventType: PlayerHistoryEventType.PlayerJoinedTeam})));
    }

    if(history.playerLeftTeamEvents){
      history.playerLeftTeamEvents.forEach(x => this.events.push(new PlayerHistoryEvent({id: x.id, date: x.date, eventType: PlayerHistoryEventType.PlayerLeftTeam})));
    }

    this.events = this.events.sort(this.compareEvent);
    this.filteredEvents = this.events;
  }

  getEventHeader(eventType: PlayerHistoryEventType){
    switch(eventType){
      case PlayerHistoryEventType.Created:
        return 'player_created_event';
      case PlayerHistoryEventType.MatchPlayed:
        return 'match';
      case PlayerHistoryEventType.PlayerJoinedTeam:
        return 'player_joined_team';
      case PlayerHistoryEventType.PlayerLeftTeam:
        return 'player_left_team';
      default:
        return 'missing_label';
    }
  }

  getMatchEvent(id: string){
    return this.history.matchEvents.find(x => x.id == id);
  }

  getPlayerJoinedTeamEvent(id: string){
    return this.history.playerJoinedTeamEvents.find(x => x.id == id);
  }

  getPlayerLeftTeamEvent(id: string){
    return this.history.playerLeftTeamEvents.find(x => x.id == id);
  }

}
