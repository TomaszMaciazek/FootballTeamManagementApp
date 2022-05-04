import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { TeamHistoryComponent } from './components/team-history/team-history.component';
import { TeamComponent } from './components/teams/team/team.component';
import { TeamsComponent } from './components/teams/teams.component';

const routes: Routes = [
  { path: 'preview', component: TeamsComponent, canActivate: [AuthGuard]},
  { path: ':id', component: TeamComponent, canActivate: [AuthGuard]},
  { path: 'history/:id', component: TeamHistoryComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamsRoutingModule { }
