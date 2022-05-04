import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { FillSurvey } from '../models/commands/fill-survey.model';
import { UserSurveyResultListItem } from '../models/listItems/user-survey-result-list-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { UserSurveyResultQuery } from '../models/queries/user-survey-result-query.model';
import { UserSurveyResult } from '../models/surveys/user-survey-result.model';
import { SimpleUser } from '../models/user/simple-user.model';

@Injectable({
  providedIn: 'root'
})
export class UserSurveyResultService {

  private url = `${environment.apiUrl}/userSurveyResult`
  
  constructor(private http: HttpClient) { }

  getResults(query: UserSurveyResultQuery) : Promise<PaginatedList<UserSurveyResultListItem>>{
    return this.http.get<PaginatedList<UserSurveyResultListItem>>(`${this.url}`, {params: toParams(query)})
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getRespondentsFromSurvey(id: string) : Promise<Array<SimpleUser>>{
    return this.http.get<Array<SimpleUser>>(`${this.url}/respondents/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getById(id: string) : Promise<UserSurveyResult>{
    return this.http.get<UserSurveyResult>(`${this.url}/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  fillSurvey(command: FillSurvey){
    return this.http.put(this.url, command)
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
