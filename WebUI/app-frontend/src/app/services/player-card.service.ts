import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PlayerCard } from '../models/cards/player-card';

@Injectable({
  providedIn: 'root'
})
export class PlayerCardService {

  private url = `${environment.apiUrl}/playerCard`

  constructor(
    private http: HttpClient
  ) { }


  getAllFromMatch(id: string): Promise<Array<PlayerCard>>{
    return this.http.get<Array<PlayerCard>>(`${this.url}/match/${id}`)
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
    else {
      return Promise.reject('something_went_wrong');
    }
  }
}
