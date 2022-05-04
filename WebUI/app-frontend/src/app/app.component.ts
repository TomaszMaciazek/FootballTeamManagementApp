import { Component } from '@angular/core';
import { LanguageCode } from './enums/language-code';
import { TokenStorageProvider } from './providers/token-storage-provider.model';
import { TranslationProvider } from './providers/translation-provider.model';
import { MessageService } from './services/message.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Klub piłkarski';


  constructor(
    private translationProvider: TranslationProvider,
    private messageService: MessageService,
    private tokenStorageProvider: TokenStorageProvider
    ){
    this.setLanguage();
  }


  private setLanguage() {
    let langId = LanguageCode.DefaultLang.toString();
    this.translationProvider.setDefaultLanguage(langId as LanguageCode);
    this.translationProvider.setLanguage(langId as LanguageCode);
  }

  changeOfRoutes() {
    if(this.tokenStorageProvider.isLogged()){
      this.messageService.getNumberOfUnreadMessages(this.tokenStorageProvider.getUserId())
      .then((result) => {
        this.messageService.numberOfUnreadMessages.emit((result));
      })
    }
  }
}
