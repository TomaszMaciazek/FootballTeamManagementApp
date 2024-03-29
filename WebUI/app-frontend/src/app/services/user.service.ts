import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { AccountUser } from '../models/account/account-user.model';
import { AddAdminCommand } from '../models/commands/add-admin.model';
import { AddCoachCommand } from '../models/commands/add-coach.model';
import { AddPlayerCommand } from '../models/commands/add-player.model';
import { ChangePassword } from '../models/commands/change-password.model';
import { UpdateUserPassword } from '../models/commands/update-user-password.model';
import { PaginatedList } from '../models/paginated-list.model';
import { UserQuery } from '../models/queries/user-query.model';
import { User } from '../models/user.model';
import { SelectUser } from '../models/user/select-user.model';
import { SimpleUser } from '../models/user/simple-user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private url = `${environment.apiUrl}/user`

  constructor(
    private http: HttpClient
  ) { }

  getMyAccount(id: string) : Promise<AccountUser>{
    return this.http.get<AccountUser>(`${this.url}/account/${id}`).toPromise();
  }

  getUsers(query: UserQuery): Promise<PaginatedList<User>> {
    return this.http.get<PaginatedList<User>>(this.url, {params: toParams(query)}).toPromise();
  }

  getAdministrators(query: UserQuery): Promise<PaginatedList<User>> {
    return this.http.get<PaginatedList<User>>(`${this.url}/administrators`, {params: toParams(query)}).toPromise();
  }

  getAllUsers(): Promise<Array<SelectUser>> {
    return this.http.get<Array<SelectUser>>(`${this.url}/all`).toPromise();
  }

  getRecipients(id: string): Promise<Array<SimpleUser>>{
    return this.http.get<Array<SimpleUser>>(`${this.url}/recipients/${id}`).toPromise();
  }

  getRespondentsList(): Promise<Array<SelectUser>>{
    return this.http.get<Array<SelectUser>>(`${this.url}/respondents`).toPromise();
  }

  createPlayer(command: AddPlayerCommand){
    return this.http.post(`${this.url}/players`, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  createCoach(command: AddCoachCommand){
    return this.http.post(`${this.url}/coaches`, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  createAdmin(command: AddAdminCommand){
    return this.http.post(`${this.url}/administrators`, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  updatePassword(command : ChangePassword){
    return this.http.patch(`${this.url}/account/changePassword`, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  updateUserPassword(command : UpdateUserPassword){
    return this.http.patch(`${this.url}/user/changePassword`, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  activate(id: string){
    return this.http.patch(`${this.url}/activate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deactivate(id: string){
    return this.http.patch(`${this.url}/deactivate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  delete(id: string){
    return this.http.delete(`${this.url}/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  private async handleError(error: any) {
    if (error.status === 404) {
      return Promise.reject('not_found');
    }
    if (error.status === 403) {
      return Promise.reject('incorrect_password');
    }
    else {
      return Promise.reject('something_went_wrong');
    }
  }
}
