import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { CreateSurveyCommand } from '../models/commands/create-survey.model';
import { MySurveyListItem } from '../models/listItems/my-survey-list-item.model';
import { SurveyListItem } from '../models/listItems/survey-list-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { SurveyTemplateQuery } from '../models/queries/survey-template-query.model';
import { SurveyFillTemplate } from '../models/surveys/survey-fill-template.model';
import { SurveyQuestion } from '../models/surveys/survey-question.model';
import { Survey } from '../models/surveys/survey.model';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {

  private url = `${environment.apiUrl}/survey`
  
  constructor(private http: HttpClient) { }

  getSurveys(query: SurveyTemplateQuery) : Promise<PaginatedList<SurveyListItem>>{
    return this.http.get<PaginatedList<SurveyListItem>>(`${this.url}/all`, {params: toParams(query)})
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getSurveyById(id: string) : Promise<Survey>{
    return this.http.get<Survey>(`${this.url}/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }
  

  getSurveyQuestions(id: string) : Promise<Array<SurveyQuestion>>{
    return this.http.get<Array<SurveyQuestion>>(`${this.url}/questions/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getTemplateToFillById(id: string) : Promise<SurveyFillTemplate>{
    return this.http.get<SurveyFillTemplate>(`${this.url}/fillTemplate/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  getSurveysCreatedByMe(query: SurveyTemplateQuery) : Promise<PaginatedList<MySurveyListItem>>{
    return this.http.get<PaginatedList<MySurveyListItem>>(`${this.url}/createdByMe`, {params: toParams(query)})
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  createSurvey(command: CreateSurveyCommand){
    return this.http.post(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deleteSurvey(id: string){
    return this.http.delete(`${this.url}/${id}`)
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
