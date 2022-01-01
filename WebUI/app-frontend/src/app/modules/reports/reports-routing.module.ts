import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClubCardsComponent } from './components/club-cards/club-cards.component';
import { ClubPointsComponent } from './components/club-points/club-points.component';
import { CoachesCardsComponent } from './components/coaches-cards/coaches-cards.component';
import { CoachesMonthCountComponent } from './components/coaches-month-count/coaches-month-count.component';
import { PlayersCardsComponent } from './components/players-cards/players-cards.component';
import { PlayersMonthCountComponent } from './components/players-month-count/players-month-count.component';
import { PlayersPointsComponent } from './components/players-points/players-points.component';

const routes: Routes = [
  { path: 'players/count', component: PlayersMonthCountComponent},
  { path: 'coaches/count', component: CoachesMonthCountComponent},
  { path: 'cards/all', component: ClubCardsComponent},
  { path: 'cards/players', component: PlayersCardsComponent},
  { path: 'cards/coaches', component: CoachesCardsComponent},
  { path: 'points/all', component: ClubPointsComponent},
  { path: 'points/players', component: PlayersPointsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
