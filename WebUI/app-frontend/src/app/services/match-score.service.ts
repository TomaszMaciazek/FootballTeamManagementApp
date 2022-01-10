import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddMatchScoreCommand } from '../models/commands/add-match-score.model';
import { UpdateMatchScoreCommand } from '../models/commands/update-match-score.model';
import { MatchScore } from '../models/match-score.model';

@Injectable({
  providedIn: 'root'
})
export class MatchScoreService {

  private url = `${environment.apiUrl}/matchScore`

  constructor(
    private http: HttpClient
  ) { }

  getAllFromMatch(matchId: string){
    return this.http.get<Array<MatchScore>>(`${this.url}/match/${matchId}`).toPromise();
  }

  addScore(command: AddMatchScoreCommand){
    return this.http.post(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  editScoreValue(command: UpdateMatchScoreCommand){
    return this.http.put(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  activateScore(id: string){
    return this.http.patch(`${this.url}/activate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deactivateScore(id: string){
    return this.http.patch(`${this.url}/deactivate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deleteScore(id: string){
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
