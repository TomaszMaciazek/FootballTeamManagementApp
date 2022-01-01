import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { SimpleCoach } from '../models/coach/simple-coach.model';
import { CoachItem } from '../models/listItems/coach-item.model';
import { PaginatedList } from '../models/paginated-list.model';
import { CoachQuery } from '../models/queries/coach-query.model';

@Injectable({
  providedIn: 'root'
})
export class CoachService {

  private url = `${environment.apiUrl}/coach`

  constructor(
    private http: HttpClient
  ) { }

  getCoaches(query : CoachQuery): Promise<PaginatedList<CoachItem>> {
    return this.http.get<PaginatedList<CoachItem>>(this.url, {params: toParams(query)}).toPromise();
  }

  getAllCoaches(): Promise<Array<SimpleCoach>> {
    return this.http.get<Array<SimpleCoach>>(`${this.url}/all`).toPromise();
  }

  getWorkingCoaches(date: string): Promise<Array<SimpleCoach>> {
    return this.http.get<Array<SimpleCoach>>(`${this.url}/working`, {params: toParams({date: date})}).toPromise();
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
