import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoachesComponent } from './components/coaches/coaches.component';
import { PlayerHistoryComponent } from './components/players/player-history/player-history.component';
import { PlayerComponent } from './components/players/player/player.component';
import { PlayersComponent } from './components/players/players.component';

const routes: Routes = [
  { path: 'players', component: PlayersComponent},
  { path: 'player/history/:id', component: PlayerHistoryComponent},
  { path: 'player/:id', component: PlayerComponent},
  { path: 'coaches', component: CoachesComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClubMembersRoutingModule { }
