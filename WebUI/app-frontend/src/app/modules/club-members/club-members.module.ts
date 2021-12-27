import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClubMembersRoutingModule } from './club-members-routing.module';
import { PlayersComponent } from './components/players/players.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoachesComponent } from './components/coaches/coaches.component';


@NgModule({
  declarations: [
    PlayersComponent,
    CoachesComponent
  ],
  imports: [
    CommonModule,
    ClubMembersRoutingModule,
    SharedModule
  ]
})
export class ClubMembersModule { }
