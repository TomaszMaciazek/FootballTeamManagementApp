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


  getAllFromMatch(id: string): Promise<PlayerCard>{
    return this.http.get<PlayerCard>(`${this.url}/${id}`)
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
