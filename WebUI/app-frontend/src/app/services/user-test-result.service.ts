import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { FillTestCommand } from '../models/commands/fill-test.model';
import { SimpleUserTestResultListItem } from '../models/listItems/simple-user-test-result-list-item.model';
import { UserTestResultListItem } from '../models/listItems/user-test-result-list-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { TestResultsQuery } from '../models/queries/test-results-query.model';
import { UserTestResultQuery } from '../models/queries/user-test-result-query.model';
import { UserTestResult } from '../models/tests/user-test-result.model';
import { SimpleUser } from '../models/user/simple-user.model';

@Injectable({
  providedIn: 'root'
})
export class UserTestResultService {

  private url = `${environment.apiUrl}/userTestResult`
  
  constructor(private http: HttpClient) { }

  getResults(query: UserTestResultQuery) : Promise<PaginatedList<UserTestResultListItem>>{
    return this.http.get<PaginatedList<UserTestResultListItem>>(`${this.url}`, {params: toParams(query)})
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getTestResults(query: TestResultsQuery) : Promise<PaginatedList<SimpleUserTestResultListItem>>{
    return this.http.get<PaginatedList<SimpleUserTestResultListItem>>(`${this.url}/test`, {params: toParams(query)})
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getRespondentsFromTest(id: string) : Promise<Array<SimpleUser>>{
    return this.http.get<Array<SimpleUser>>(`${this.url}/respondents/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getById(id: string) : Promise<UserTestResult>{
    return this.http.get<UserTestResult>(`${this.url}/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  fillTest(command: FillTestCommand) : Promise<string>{
    return this.http.put<string>(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  private async handleError(error: any) {
    if (error.status === 404) {
      return Promise.reject('not_found');
    }
    else {
      return Promise.reject('something_went_wrong');
    }
  }
}
