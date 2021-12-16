import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { AddTrainingCommand } from '../models/commands/add-training.model';
import { TrainingItem } from '../models/listItems/training-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { TrainingQuery } from '../models/queries/training-query.model';
import { Training } from '../models/training.model';

@Injectable({
  providedIn: 'root'
})
export class TrainingService {

  private url = `${environment.apiUrl}/training`

  constructor(
    private http: HttpClient
  ) { }

  getTrainings(query: TrainingQuery) : Promise<PaginatedList<TrainingItem>>{
    return this.http.get<PaginatedList<TrainingItem>>(this.url, {params: toParams(query)}).toPromise();
  }

  getById(id: string) : Promise<Training>{
    return this.http.get<Training>(`${this.url}/${id}`).toPromise();
  }

  createTraining(command: AddTrainingCommand){
    return this.http.post(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  activateTraining(id: string){
    return this.http.patch(`${this.url}/activate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deactivateTraining(id: string){
    return this.http.patch(`${this.url}/deactivate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deleteTraining(id: string){
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
