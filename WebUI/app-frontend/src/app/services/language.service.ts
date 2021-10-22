import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Language } from '../models/language.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoadingStatus } from '../models/loading-status.model';
import { Translate } from '../models/translate.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  private languageUrl = `${environment.apiUrl}/language`;
  private translationUrl = `${environment.apiUrl}/translation`;
  private translates: Translate[] = [];
  private languages: Language[] = [];
  private loadingStatus: LoadingStatus<Language>;
  private languages$: BehaviorSubject<LoadingStatus<Language>>;

  selectedLanguageId = "";

  private defaultLanguageCode = "PL";
  constructor(
    private http: HttpClient,
    private router: Router
  ) { 
    this.loadingStatus = new LoadingStatus<Language>({
      loading: true,
      loaded: false,
      updating: false,
      list: []
    });
    this.languages$ = new BehaviorSubject<LoadingStatus<Language>>(this.loadingStatus);
    this.refreshLanguages();
    this.translates = JSON.parse(sessionStorage.getItem('translates')) || [];
    if (this.translates.length === 0) {
      if(this.selectedLanguageId !== null){
        this.getTranslations(this.selectedLanguageId).subscribe((translates: Translate[]) => {
          sessionStorage.setItem('translates', JSON.stringify(translates));
          this.translates = translates;
        });
      }
      else{
        var defaultLanguageId = this.languages.find(lang => lang.code.toUpperCase() == this.defaultLanguageCode).id;
        this.getTranslations(defaultLanguageId).subscribe((translates: Translate[]) => {
          sessionStorage.setItem('translates', JSON.stringify(translates));
          this.translates = translates;
        });
      }
    }
  }

  public refreshLanguages() {
    return this.http.get<Language[]>(this.languageUrl).subscribe((languages) => {
      this.languages = languages;
      this.loadingStatus.loading = false;
      this.loadingStatus.loaded = true;
      this.loadingStatus.list = this.languages;
      this.languages$.next(this.loadingStatus);
    });
  }

  public getAllLanguages(): Observable<LoadingStatus<Language>> {
    if (this.languages.length === 0) {
      this.refreshLanguages();
    }
    return this.languages$;
  }

  public getTranslations(languageId : string) {
    return this.http.get(`${this.translationUrl}/${languageId}`);
  }

  public selectLanguage(id : string){
    this.selectedLanguageId = id;
    this.router.navigate([this.router.url]);
  }

  transform(key: string) {
    const translate = this.translates.find(translate => translate.key.toLocaleLowerCase() === key.toLocaleLowerCase() && translate.languageId === this.selectedLanguageId);
    return translate ? translate.value : key;
  }
}
