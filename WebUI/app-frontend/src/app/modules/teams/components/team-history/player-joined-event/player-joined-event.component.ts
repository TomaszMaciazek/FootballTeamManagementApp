import { Component, Input, OnInit } from '@angular/core';
import { TeamHistoryPlayerJoinedTeamEvent } from 'src/app/models/history/player-joined-team-event.model';

@Component({
  selector: 'player-joined-event',
  templateUrl: './player-joined-event.component.html'
})
export class PlayerJoinedEventComponent {

  @Input() event : TeamHistoryPlayerJoinedTeamEvent;

}
