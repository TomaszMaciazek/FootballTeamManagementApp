import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { TeamHistoryEventType } from 'src/app/enums/team-history-event-type';
import { TeamHistoryEvent } from 'src/app/models/history/team-history-event.model';
import { TeamHistory } from 'src/app/models/history/team-history.model';
import { TeamHistoryQuery } from 'src/app/models/queries/team-history-query.model';
import { TeamHistoryService } from 'src/app/services/team-history.service';

@Component({
  selector: 'app-team-history',
  templateUrl: './team-history.component.html',
  styleUrls: ['./team-history.component.scss']
})
export class TeamHistoryComponent implements OnInit {

  id: string;
  history : TeamHistory;
  events: TeamHistoryEvent[];
  filteredEvents: TeamHistoryEvent[];

  teamHistoryEventType = TeamHistoryEventType;

  minDate: Date;
  maxDate: Date;

  public form: FormGroup;

  showFilters: boolean = false;

  compareEvent = (a: TeamHistoryEvent, b: TeamHistoryEvent) => {
    if(a.eventType == TeamHistoryEventType.Created){
      return -1;
    }
    if(b.eventType == TeamHistoryEventType.Created){
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
    private teamHistoryService: TeamHistoryService,
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
      var tmp = new TeamHistoryQuery({teamId: this.id, minDate: null, maxDate: null});
      this.teamHistoryService.getTeamHistory(tmp).then(res => {
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

  prepareEvents(history: TeamHistory){
    this.events = [];
    this.events.push(new TeamHistoryEvent({id: history.id, date: history.created, eventType: TeamHistoryEventType.Created}));

    if(history.coachAssignmentEvents){
      history.coachAssignmentEvents.forEach(x => this.events.push(new TeamHistoryEvent({id: x.id, date: x.date, eventType: TeamHistoryEventType.MainCoachAssigned})));
    }

    if(history.matchEvents){
      history.matchEvents.forEach(x => this.events.push(new TeamHistoryEvent({id: x.id, date: x.match.date, eventType: TeamHistoryEventType.MatchPlayed})));
    }

    if(history.playerJoinedTeamEvents){
      history.playerJoinedTeamEvents.forEach(x => this.events.push(new TeamHistoryEvent({id: x.id, date: x.date, eventType: TeamHistoryEventType.PlayerJoinedTeam})));
    }

    if(history.playerLeftTeamEvents){
      history.playerLeftTeamEvents.forEach(x => this.events.push(new TeamHistoryEvent({id: x.id, date: x.date, eventType: TeamHistoryEventType.PlayerLeftTeam})));
    }

    this.events = this.events.sort(this.compareEvent);
    this.filteredEvents = this.events;
  }

  getEventHeader(eventType: TeamHistoryEventType){
    switch(eventType){
      case TeamHistoryEventType.Created:
        return 'team_created_event';
      case TeamHistoryEventType.MainCoachAssigned:
        return 'main_coach_assigned';
      case TeamHistoryEventType.MatchPlayed:
        return 'match';
      case TeamHistoryEventType.PlayerJoinedTeam:
        return 'player_joined_team';
      case TeamHistoryEventType.PlayerLeftTeam:
        return 'player_left_team';
      default:
        return 'missing_label';
    }
  }

  getMatchEvent(id: string){
    return this.history.matchEvents.find(x => x.id == id);
  }

  getCoachAssignedEvent(id: string){
    return this.history.coachAssignmentEvents.find(x => x.id == id);
  }

  getPlayerJoinedTeamEvent(id: string){
    return this.history.playerJoinedTeamEvents.find(x => x.id == id);
  }

  getPlayerLeftTeamEvent(id: string){
    return this.history.playerLeftTeamEvents.find(x => x.id == id);
  }



}
