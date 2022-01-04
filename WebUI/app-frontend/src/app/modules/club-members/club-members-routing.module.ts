import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { CoachComponent } from './components/coaches/coach/coach.component';
import { CoachesComponent } from './components/coaches/coaches.component';
import { PlayerHistoryComponent } from './components/players/player-history/player-history.component';
import { PlayerComponent } from './components/players/player/player.component';
import { PlayersComponent } from './components/players/players.component';

const routes: Routes = [
  { path: 'players', component: PlayersComponent, canActivate: [AuthGuard]},
  { path: 'player/history/:id', component: PlayerHistoryComponent, canActivate: [AuthGuard]},
  { path: 'player/:id', component: PlayerComponent, canActivate: [AuthGuard]},
  { path: 'coaches', component: CoachesComponent, canActivate: [AuthGuard]},
  { path: 'coach/:id', component: CoachComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClubMembersRoutingModule { }
