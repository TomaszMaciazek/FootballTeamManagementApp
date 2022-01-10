import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatCardModule} from '@angular/material/card';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatChipsModule} from '@angular/material/chips';
import {MatRadioModule} from '@angular/material/radio';

import { MessagesRoutingModule } from './messages-routing.module';
import { MessagesComponent } from './components/messages/messages.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DisplayMessageComponent } from './components/messages/display-message/display-message.component';
import { CreateMessageComponent } from './components/messages/create-message/create-message.component';
import { InboxComponent } from './components/messages/inbox/inbox.component';
import { SentMessagesComponent } from './components/messages/sent-messages/sent-messages.component';
import { TrashComponent } from './components/messages/trash/trash.component';
import { ToolbarComponent } from './components/messages/toolbar/toolbar.component';
import {EditorModule} from 'primeng/editor';


@NgModule({
  declarations: [
    MessagesComponent,
    DisplayMessageComponent,
    CreateMessageComponent,
    InboxComponent,
    SentMessagesComponent,
    TrashComponent,
    ToolbarComponent
  ],
  imports: [
    CommonModule,
    MessagesRoutingModule,
    SharedModule,
    EditorModule,
    MatInputModule,
    MatFormFieldModule,
    MatSlideToggleModule,
    MatCardModule,
    MatSelectModule,
    MatButtonModule,
    MatChipsModule,
    MatRadioModule
  ]
})
export class MessagesModule { }
