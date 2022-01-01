import { Component, Input, OnInit } from '@angular/core';
import { PlayerLeftTeamEvent } from 'src/app/models/history/player-left-team-event.model';

@Component({
  selector: 'player-left-event',
  templateUrl: './player-left-event.component.html'
})
export class PlayerLeftEventComponent {

  @Input() event : PlayerLeftTeamEvent;

}
