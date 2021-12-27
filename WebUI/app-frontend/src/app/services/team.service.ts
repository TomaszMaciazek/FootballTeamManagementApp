import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { AddTeamCommand } from '../models/commands/add-team.model';
import { UpdateTeamCommand } from '../models/commands/update-team.model';
import { TeamItem } from '../models/listItems/team-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { TeamQuery } from '../models/queries/team-query.model';
import { SimpleTeam } from '../models/team/simple-team.model';
import { Team } from '../models/team/team.model';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  private url = `${environment.apiUrl}/team`

  constructor(
    private http: HttpClient
  ) { }

  getTeams(query: TeamQuery) : Promise<PaginatedList<TeamItem>>{
    return this.http.get<PaginatedList<TeamItem>>(this.url, {params: toParams(query)}).toPromise();
  }

  getAllTeams() : Promise<Array<SimpleTeam>>{
    return this.http.get<Array<SimpleTeam>>(`${this.url}/all`).toPromise();
  }

  getById(id: string) : Promise<Team>{
    return this.http.get<Team>(`${this.url}/${id}`).toPromise();
  }

  createTeam(command: AddTeamCommand){
    return this.http.post(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  editScoreValue(command: UpdateTeamCommand){
    return this.http.put(this.url, command)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  activateTeam(id: string){
    return this.http.patch(`${this.url}/activate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deactivateTeam(id: string){
    return this.http.patch(`${this.url}/deactivate/${id}`,null)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  deleteTeam(id: string){
    return this.http.delete(`${this.url}/${id}`)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  private async handleError(error: any) {
    if (error.status === 404) {
      return Promise.reject('not_found');
    }
    else if(error.status == 405){
      return Promise.reject('team_has_no_main_coach');
    }
    else {
      return Promise.reject('something_went_wrong');
    }
  }
}
