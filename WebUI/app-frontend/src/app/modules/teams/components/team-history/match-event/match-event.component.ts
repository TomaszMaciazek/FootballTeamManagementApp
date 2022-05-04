import { Component, Input, OnInit } from '@angular/core';
import { MatchType } from 'src/app/enums/match-type';
import { TeamMatchEvent } from 'src/app/models/history/team-match-event.model';

@Component({
  selector: 'match-event',
  templateUrl: './match-event.component.html'
})
export class MatchEventComponent{

  @Input() event : TeamMatchEvent;

  matchTypeOptionsLabel = new Map<number, string>([
    [MatchType.Cup, 'cup_match'],
    [MatchType.League, 'league_match'],
    [MatchType.Friendly, 'friendly_match']
  ]);

}
