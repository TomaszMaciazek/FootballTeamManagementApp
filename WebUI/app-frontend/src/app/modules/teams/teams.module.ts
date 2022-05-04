import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeamsRoutingModule } from './teams-routing.module';
import { TeamsComponent } from './components/teams/teams.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { AddTeamComponent } from './components/teams/add-team/add-team.component';
import { EditTeamComponent } from './components/teams/edit-team/edit-team.component';
import { TeamComponent } from './components/teams/team/team.component';
import { TeamAddPlayersComponent } from './components/teams/team-add-player/team-add-players.component';
import { TeamHistoryComponent } from './components/team-history/team-history.component';
import { MatchEventComponent } from './components/team-history/match-event/match-event.component';
import { PlayerJoinedEventComponent } from './components/team-history/player-joined-event/player-joined-event.component';
import { PlayerLeftEventComponent } from './components/team-history/player-left-event/player-left-event.component';
import { MainCoachEventComponent } from './components/team-history/main-coach-event/main-coach-event.component';


@NgModule({
  declarations: [
    TeamsComponent,
    AddTeamComponent,
    EditTeamComponent,
    TeamComponent,
    TeamAddPlayersComponent,
    TeamHistoryComponent,
    MatchEventComponent,
    PlayerJoinedEventComponent,
    PlayerLeftEventComponent,
    MainCoachEventComponent
  ],
  imports: [
    CommonModule,
    TeamsRoutingModule,
    SharedModule
  ]
})
export class TeamsModule { }
