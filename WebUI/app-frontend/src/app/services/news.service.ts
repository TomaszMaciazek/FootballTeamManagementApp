import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { AddNewsCommand } from '../models/commands/add-news.model';
import { UpdateNewsCommand } from '../models/commands/update-news.model';
import { News } from '../models/news.model';
import { PaginatedList } from '../models/paginated-list.model';
import { NewsQuery } from '../models/queries/news-query.model';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  private url = `${environment.apiUrl}/news`

  constructor(
    private http: HttpClient
  ) { }

  getNews(query: NewsQuery): Promise<PaginatedList<News>> {
    return this.http.get<PaginatedList<News>>(this.url, {params: toParams(query)}).toPromise();
  }

  getById(id: string) : Promise<News>{
    return this.http.get<News>(`${this.url}/${id}`).toPromise();
  }

  createNews(news: AddNewsCommand){
    return this.http.post(this.url, news)
    .toPromise()
    .catch(this.handleError.bind(this));
  }

  updateNews(news: UpdateNewsCommand){
    return this.http.put(this.url, news)
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
