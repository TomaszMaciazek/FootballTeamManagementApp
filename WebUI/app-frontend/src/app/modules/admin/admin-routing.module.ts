import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddUserComponent } from './components/users/add-user/add-user.component';
import { EditUserComponent } from './components/users/edit-user/edit-user.component';
import { UsersComponent } from './components/users/users.component';

const routes: Routes = [
  { path: 'users', component: UsersComponent},
  { path: 'users/edit', component: EditUserComponent},
  { path: 'users/add', component: AddUserComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
