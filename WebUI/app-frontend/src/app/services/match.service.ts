import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { AddMatchCommand } from '../models/commands/add-match.model';
import { MatchItem } from '../models/listItems/match-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { MatchQuery } from '../models/queries/match-query.model';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  private url = `${environment.apiUrl}/match`

  constructor(
    private http: HttpClient
  ) { }

  getMatches(query : MatchQuery): Promise<PaginatedList<MatchItem>> {
    return this.http.get<PaginatedList<MatchItem>>(this.url, {params: toParams(query)}).toPromise();
  }

  createMatch(command: AddMatchCommand){
    return this.http.post(this.url, command)
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
