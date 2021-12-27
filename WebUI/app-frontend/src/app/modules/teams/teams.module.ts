import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TeamsRoutingModule } from './teams-routing.module';
import { TeamsComponent } from './components/teams/teams.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { AddTeamComponent } from './components/teams/add-team/add-team.component';
import { EditTeamComponent } from './components/teams/edit-team/edit-team.component';
import { TeamComponent } from './components/teams/team/team.component';
import { TeamAddPlayersComponent } from './components/teams/team-add-player/team-add-players.component';


@NgModule({
  declarations: [
    TeamsComponent,
    AddTeamComponent,
    EditTeamComponent,
    TeamComponent,
    TeamAddPlayersComponent
  ],
  imports: [
    CommonModule,
    TeamsRoutingModule,
    SharedModule
  ]
})
export class TeamsModule { }
