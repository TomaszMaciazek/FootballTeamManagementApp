import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BasicLayoutComponent } from './components/layout/basic-layout/basic-layout.component';
import { BlankLayoutComponent } from './components/layout/blank-layout/blank-layout.component';
import { LoginComponent } from './components/login/login.component';
import { RefreshComponent } from './components/refresh/refresh.component';

const routes: Routes = [
  {
    path: '', redirectTo: 'login', pathMatch: 'full'
  },
  {
    path: '', component: BlankLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'refresh', component: RefreshComponent },
    ]
  },
  {
    path: 'news', component: BasicLayoutComponent,
    loadChildren: () => import('./modules/news/news.module').then(m => m.NewsModule)
  },
  {
    path: 'admin', component: BasicLayoutComponent,
    loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule)
  },
  {
    path: 'results', component: BasicLayoutComponent,
    loadChildren: () => import('./modules/results/results.module').then(m => m.ResultsModule)
  },
  {
    path: 'teams', component: BasicLayoutComponent,
    loadChildren: () => import('./modules/teams/teams.module').then(m => m.TeamsModule)
  },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
