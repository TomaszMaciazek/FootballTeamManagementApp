import { Component, Input, OnInit } from '@angular/core';
import { MatchType } from 'src/app/enums/match-type';
import { PlayerMatchEvent } from 'src/app/models/history/player-match-event.model';

@Component({
  selector: 'player-match-event',
  templateUrl: './player-match-event.component.html'
})
export class PlayerMatchEventComponent {

  @Input() event : PlayerMatchEvent;

  matchTypeOptionsLabel = new Map<number, string>([
    [MatchType.Cup, 'cup_match'],
    [MatchType.League, 'league_match'],
    [MatchType.Friendly, 'friendly_match']
  ]);
}
