import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoachesComponent } from './components/coaches/coaches.component';
import { PlayersComponent } from './components/players/players.component';

const routes: Routes = [
  { path: 'players', component: PlayersComponent},
  { path: 'coaches', component: CoachesComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClubMembersRoutingModule { }
