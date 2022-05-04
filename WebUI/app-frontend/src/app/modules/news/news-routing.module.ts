import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { AddNewsComponent } from './components/add-news/add-news.component';
import { EditNewsComponent } from './components/edit-news/edit-news.component';
import { NewsComponent } from './components/news/news.component';

const routes: Routes = [
  { path: 'preview', component: NewsComponent, canActivate: [AuthGuard]},
  { path: 'add', component: AddNewsComponent, canActivate: [AuthGuard]},
  { path: 'edit', component: EditNewsComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NewsRoutingModule { }
