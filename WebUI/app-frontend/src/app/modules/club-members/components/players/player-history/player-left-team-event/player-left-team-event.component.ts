import { Component, Input, OnInit } from '@angular/core';
import { PlayerHistoryPlayerLeftTeamEvent } from 'src/app/models/history/player-history-player-left-team-event.model';

@Component({
  selector: 'history-player-left-team-event',
  templateUrl: './player-left-team-event.component.html'
})
export class PlayerLeftTeamEventComponent {

  @Input() event : PlayerHistoryPlayerLeftTeamEvent;

}
