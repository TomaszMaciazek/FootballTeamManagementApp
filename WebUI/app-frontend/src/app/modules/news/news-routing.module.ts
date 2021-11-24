import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddNewsComponent } from './components/add-news/add-news.component';
import { EditNewsComponent } from './components/edit-news/edit-news.component';
import { NewsComponent } from './components/news/news.component';

const routes: Routes = [
  { path: 'preview', component: NewsComponent},
  { path: 'add', component: AddNewsComponent},
  { path: 'edit', component: EditNewsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NewsRoutingModule { }
