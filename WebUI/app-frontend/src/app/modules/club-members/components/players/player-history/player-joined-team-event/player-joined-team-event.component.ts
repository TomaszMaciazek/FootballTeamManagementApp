import { Component, Input, OnInit } from '@angular/core';
import { PlayerHistoryPlayerJoinedTeamEvent } from 'src/app/models/history/player-history-player-joined-team-event.model';

@Component({
  selector: 'history-player-joined-team-event',
  templateUrl: './player-joined-team-event.component.html'
})
export class PlayerJoinedTeamEventComponent {

  @Input() event : PlayerHistoryPlayerJoinedTeamEvent;

}
