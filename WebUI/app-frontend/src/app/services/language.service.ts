import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Language } from '../models/language.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoadingStatus } from '../models/loading-status.model';
@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  private url = `api/language`;
  private languages: Language[] = [];
  private loadingStatus: LoadingStatus<Language>;
  private languages$: BehaviorSubject<LoadingStatus<Language>>;
  constructor(
    private http: HttpClient
  ) { 
    this.loadingStatus = new LoadingStatus<Language>({
      loading: true,
      loaded: false,
      updating: false,
      list: []
    });
    this.languages$ = new BehaviorSubject<LoadingStatus<Language>>(this.loadingStatus);
  }

  public refresh() {
    return this.http.get<Language[]>(this.url).subscribe((languages) => {
      this.languages = languages;
      this.loadingStatus.loading = false;
      this.loadingStatus.loaded = true;
      this.loadingStatus.list = this.languages;
      this.languages$.next(this.loadingStatus);
    });
  }

  public getAll(): Observable<LoadingStatus<Language>> {
    if (this.languages.length === 0) {
      this.refresh();
    }
    return this.languages$;
  }
}
