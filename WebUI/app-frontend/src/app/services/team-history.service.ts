import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { TeamHistory } from '../models/history/team-history.model';
import { TeamHistoryQuery } from '../models/queries/team-history-query.model';

@Injectable({
  providedIn: 'root'
})
export class TeamHistoryService {

  private url = `${environment.apiUrl}/teamHistory`

  constructor(
    private http: HttpClient
  ) { }

  getTeamHistory(query: TeamHistoryQuery) : Promise<TeamHistory>{
    return this.http.get<TeamHistory>(this.url, {params: toParams(query)}).toPromise();
  }

}
