import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { PlayerHistory } from '../models/history/player-history.model';

@Injectable({
  providedIn: 'root'
})
export class PlayerHistoryService {

  private url = `${environment.apiUrl}/playerHistory`

  constructor(
    private http: HttpClient
  ) { }

  getPlayerHistory(id: string) : Promise<PlayerHistory>{
    return this.http.get<PlayerHistory>(this.url, {params: toParams({playerId: id})}).toPromise();
  }
}
