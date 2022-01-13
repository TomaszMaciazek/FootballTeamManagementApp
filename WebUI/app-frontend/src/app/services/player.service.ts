import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { AddPlayersToTeam } from '../models/commands/add-player-to-team.model';
import { RemovePlayerFromTeam } from '../models/commands/remove-player-from-team.model';
import { PlayerItem } from '../models/listItems/player-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { Player } from '../models/player/player.model';
import { SimpleSelectPlayer } from '../models/player/simple-select-player.model';
import { PlayerQuery } from '../models/queries/player-query.model';
import { PlayingPlayerQuery } from '../models/queries/playing-player-query.model';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  private url = `${environment.apiUrl}/player`

  constructor(
    private http: HttpClient
  ) { }
  
  getPlayers(query : PlayerQuery): Promise<PaginatedList<PlayerItem>> {
    return this.http.get<PaginatedList<PlayerItem>>(this.url, {params: toParams(query)}).toPromise();
  }

  getPlayerById(id: string) : Promise<Player>{
    return this.http.get<Player>(`${this.url}/${id}`).toPromise();
  }

  getAllPlayers(): Promise<Array<SimpleSelectPlayer>> {
    return this.http.get<Array<SimpleSelectPlayer>>(`${this.url}/all`).toPromise();
  }

  getPlayingPlayers(query: PlayingPlayerQuery): Promise<Array<SimpleSelectPlayer>> {
    return this.http.get<Array<SimpleSelectPlayer>>(`${this.url}/playing`, {params: toParams(query)}).toPromise();
  }

  getPlayersWithoutTeam(): Promise<Array<SimpleSelectPlayer>> {
    return this.http.get<Array<SimpleSelectPlayer>>(`${this.url}/withoutTeam`).toPromise();
  }

  addPlayerToTeam(command: AddPlayersToTeam){
    return this.http.post(`${this.url}/addToTeam`, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  removePlayerFromTeam(command: RemovePlayerFromTeam){
    return this.http.post(`${this.url}/removeFromTeam`, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  togglPlayerStatus(id: string){
    return this.http.patch(`${this.url}/toggleStatus/${id}`, null).toPromise();
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
