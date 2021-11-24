import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NewsRoutingModule } from './news-routing.module';
import { NewsComponent } from './components/news/news.component';
import { SharedModule } from '../../shared/shared.module'
import { EditNewsComponent } from './components/edit-news/edit-news.component';
import {EditorModule} from 'primeng/editor';
import { AddNewsComponent } from './components/add-news/add-news.component';



@NgModule({
  declarations: [
    NewsComponent,
    EditNewsComponent,
    AddNewsComponent
  ],
  imports: [
    CommonModule,
    NewsRoutingModule,
    SharedModule,
    EditorModule
  ]
})
export class NewsModule { }
