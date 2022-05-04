import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddTrainingScoreCommand } from '../models/commands/add-training-score.model';
import { UpdateTrainingScoreCommand } from '../models/commands/update-training-score.model';
import { TrainingScore } from '../models/training-score.model';

@Injectable({
  providedIn: 'root'
})
export class TrainingScoreService {

  private url = `${environment.apiUrl}/trainingScore`

  constructor(
    private http: HttpClient
  ) { }

  getAllFromTraining(trainingId: string){
    return this.http.get<Array<TrainingScore>>(`${this.url}/training/${trainingId}`).toPromise();
  }

  addScore(command: AddTrainingScoreCommand){
    return this.http.post(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  editScoreValue(command: UpdateTrainingScoreCommand){
    return this.http.put(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  activateTrainingScore(id: string){
    return this.http.patch(`${this.url}/activate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deactivateTrainingScore(id: string){
    return this.http.patch(`${this.url}/deactivate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deleteTrainingScore(id: string){
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
