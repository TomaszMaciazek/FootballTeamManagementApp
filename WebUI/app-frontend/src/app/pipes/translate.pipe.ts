import { Injectable, Pipe, PipeTransform } from '@angular/core';
import { LanguageService } from '../services/language.service';

@Injectable({
  providedIn: 'root'
})
@Pipe({
  name: 'translate'
})
export class TranslatePipe implements PipeTransform {

  constructor(
    public languageService: LanguageService
  ) { }
  
  transform(key: string) {
    const translate = this.languageService.transform(key);
    return translate ? translate : key;
  }

}
