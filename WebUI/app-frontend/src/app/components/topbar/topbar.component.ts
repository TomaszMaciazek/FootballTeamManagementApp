import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LanguageCode } from 'src/app/enums/language-code';
import { Language } from 'src/app/models/language.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})
export class TopbarComponent implements OnInit {

  languages: Language[];
  selectedLanguage: Language;

  messagesNumber: number = 0;
  
  constructor(
    private translationProvider: TranslationProvider,
    private router: Router,
    private authenticationService: AuthenticationService,
    private messagesService: MessageService
  ) { 
    this.languages = [
      {name: 'Polski', code: LanguageCode.Pl, imagePath: "assets/flags-svg/pl.svg"},
      {name: 'English', code: LanguageCode.En, imagePath: "assets/flags-svg/gb.svg"}
    ];
    this.selectedLanguage = this.languages[0];
  }

  ngOnInit(): void {
    this.messagesService.numberOfUnreadMessages.subscribe((result) => {
      this.messagesNumber = result;
    });
  }

  changeLanguage(event : any){
    if(event.value != null){
      this.translationProvider.setLanguage(event.value.code);
    }
  }

  logout(){
    this.authenticationService.logOut();
    this.router.navigateByUrl('/login');
  }

  getBadgeText() {
    return this.messagesNumber < 1 ? '' : this.messagesNumber;
  }

  goToMessages(){
    this.router.navigateByUrl('/messages/box');
  }

}
