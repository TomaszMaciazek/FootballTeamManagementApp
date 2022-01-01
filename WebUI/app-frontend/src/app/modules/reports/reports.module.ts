import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportsRoutingModule } from './reports-routing.module';
import { PlayersMonthCountComponent } from './components/players-month-count/players-month-count.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CoachesMonthCountComponent } from './components/coaches-month-count/coaches-month-count.component';
import { ClubCardsComponent } from './components/club-cards/club-cards.component';
import { PlayersCardsComponent } from './components/players-cards/players-cards.component';
import { CoachesCardsComponent } from './components/coaches-cards/coaches-cards.component';
import { ClubPointsComponent } from './components/club-points/club-points.component';
import { PlayersPointsComponent } from './components/players-points/players-points.component';


@NgModule({
  declarations: [
    PlayersMonthCountComponent,
    CoachesMonthCountComponent,
    ClubCardsComponent,
    PlayersCardsComponent,
    CoachesCardsComponent,
    ClubPointsComponent,
    PlayersPointsComponent
  ],
  imports: [
    CommonModule,
    ReportsRoutingModule,
    SharedModule
  ]
})
export class ReportsModule { }
