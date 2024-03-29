import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SignInModel } from 'src/app/models/sign-in.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { ToastrService } from 'ngx-toastr';
import { UserContextProvider } from 'src/app/providers/user-context-provider.model';
import { Language } from 'src/app/models/language.model';
import { LanguageCode } from 'src/app/enums/language-code';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, AfterViewInit {


  signInFormSubmitted : boolean = false
  signInForm: FormGroup;
  languages: Language[] =[];
  selectedLanguage: Language;

  constructor(
    private authenticationService : AuthenticationService,
    private router: Router,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private tokenStorageProvider: TokenStorageProvider,
    private translationProvider: TranslationProvider,
    private userContextProvider : UserContextProvider
  ) { 
    this.languages = [
      {name: 'Polski', code: LanguageCode.Pl, imagePath: "assets/flags-svg/pl.svg"},
      {name: 'English', code: LanguageCode.En, imagePath: "assets/flags-svg/gb.svg"}
    ];
    this.selectedLanguage = this.languages[0];
  }

  ngOnInit(){
    if(this.tokenStorageProvider.isLogged()){
      this.prepareSignInForm();
      var url = this.userContextProvider.getDefaultPageUrl();
      this.router.navigate([url]);
    }
    else{
      this.spinner.show();
      this.prepareSignInForm();
    }
  }

  ngAfterViewInit(){
    this.spinner.hide();
  }

  async signIn(){
    this.signInFormSubmitted = true;
    if(this.signInForm.valid){
      this.spinner.show();
      const formData = this.signInForm.getRawValue();
      const model = new SignInModel({
        ...formData
      });
      this.authenticationService.signIn(model).then(res => {
          this.afterSignIn();
      }).catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
        this.signInForm.controls['password'].setValue("");
      });
    }
  }

  private afterSignIn() {
    this.spinner.hide();
    this.toastr.success(this.translationProvider.getTranslation('success'));
    var url = this.userContextProvider.getDefaultPageUrl();
    this.router.navigate([url]);
  }

  private prepareSignInForm() {
    this.signInForm = new FormGroup({
      login: new FormControl(
        null,
        [Validators.required]
      ),
      password: new FormControl(
        null,
        [Validators.required]
      )
    });
    this.signInFormSubmitted = false;
  }

  changeLanguage(event : any){
    if(event.value != null){
      this.translationProvider.setLanguage(event.value.code);
    }
  }

}
