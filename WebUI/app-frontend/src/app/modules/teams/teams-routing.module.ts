import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeamComponent } from './components/teams/team/team.component';
import { TeamsComponent } from './components/teams/teams.component';

const routes: Routes = [
  { path: 'preview', component: TeamsComponent},
  { path: ':id', component: TeamComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamsRoutingModule { }
