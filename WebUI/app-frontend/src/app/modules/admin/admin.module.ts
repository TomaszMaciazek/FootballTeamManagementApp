import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { UsersComponent } from './components/users/users.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { EditUserComponent } from './components/users/edit-user/edit-user.component';
import { AddUserComponent } from './components/users/add-user/add-user.component';


@NgModule({
  declarations: [
    UsersComponent,
    EditUserComponent,
    AddUserComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
