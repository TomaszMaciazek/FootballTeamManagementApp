import { Component } from '@angular/core';
import { LanguageCode } from './enums/language-code';
import { TranslationProvider } from './providers/translation-provider.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Klub piłkarski';


  constructor(private translationProvider: TranslationProvider){
    this.setLanguage();
  }


  private setLanguage() {
    let langId = LanguageCode.DefaultLang.toString();
    this.translationProvider.setDefaultLanguage(langId as LanguageCode);
    this.translationProvider.setLanguage(langId as LanguageCode);
}
}
