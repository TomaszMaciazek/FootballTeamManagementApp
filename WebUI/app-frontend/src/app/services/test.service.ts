import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { CreateTestCommand } from '../models/commands/create-test.model';
import { MyTestListItem } from '../models/listItems/my-test-list-item.model';
import { TestListItem } from '../models/listItems/test-list-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { TestTemplateQuery } from '../models/queries/test-template-query.model';
import { TestQuestion } from '../models/tests/test-question.model';
import { TestToFill } from '../models/tests/test-to-fill.model';
import { Test } from '../models/tests/test.model';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  private url = `${environment.apiUrl}/test`
  
  constructor(private http: HttpClient) { }

  getById(id: string) : Promise<Test>{
    return this.http.get<Test>(`${this.url}/${id}`).toPromise();
  }

  getTests(query: TestTemplateQuery) : Promise<PaginatedList<TestListItem>>{
    return this.http.get<PaginatedList<TestListItem>>(`${this.url}/all`, {params: toParams(query)})
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getTestQuestions(id: string) : Promise<Array<TestQuestion>>{
    return this.http.get<Array<TestQuestion>>(`${this.url}/questions/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getTestsCreatedByMe(query: TestTemplateQuery) : Promise<PaginatedList<MyTestListItem>>{
    return this.http.get<PaginatedList<MyTestListItem>>(`${this.url}/createdByMe`, {params: toParams(query)})
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  createTest(command: CreateTestCommand){
    return this.http.post(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  delete(id: string){
    return this.http.delete(`${this.url}/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getTemplateToFillById(id: string) : Promise<TestToFill>{
    return this.http.get<TestToFill>(`${this.url}/fillTemplate/${id}`)
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
