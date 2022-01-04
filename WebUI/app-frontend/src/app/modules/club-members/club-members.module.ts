import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClubMembersRoutingModule } from './club-members-routing.module';
import { PlayersComponent } from './components/players/players.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoachesComponent } from './components/coaches/coaches.component';
import { PlayerHistoryComponent } from './components/players/player-history/player-history.component';
import { PlayerMatchEventComponent } from './components/players/player-history/player-match-event/player-match-event.component';
import { PlayerJoinedTeamEventComponent } from './components/players/player-history/player-joined-team-event/player-joined-team-event.component';
import { PlayerLeftTeamEventComponent } from './components/players/player-history/player-left-team-event/player-left-team-event.component';
import { PlayerComponent } from './components/players/player/player.component';
import { CoachComponent } from './components/coaches/coach/coach.component';


@NgModule({
  declarations: [
    PlayersComponent,
    CoachesComponent,
    PlayerHistoryComponent,
    PlayerMatchEventComponent,
    PlayerJoinedTeamEventComponent,
    PlayerLeftTeamEventComponent,
    PlayerComponent,
    CoachComponent
  ],
  imports: [
    CommonModule,
    ClubMembersRoutingModule,
    SharedModule
  ]
})
export class ClubMembersModule { }
