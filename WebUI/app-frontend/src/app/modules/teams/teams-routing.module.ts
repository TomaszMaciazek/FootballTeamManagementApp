import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeamHistoryComponent } from './components/team-history/team-history.component';
import { TeamComponent } from './components/teams/team/team.component';
import { TeamsComponent } from './components/teams/teams.component';

const routes: Routes = [
  { path: 'preview', component: TeamsComponent},
  { path: ':id', component: TeamComponent},
  { path: 'history/:id', component: TeamHistoryComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamsRoutingModule { }
