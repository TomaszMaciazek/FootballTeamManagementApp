import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SignInModel } from 'src/app/models/sign-in.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { faUser, faLock } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { TranslatePipe } from 'src/app/pipes/translate.pipe';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, AfterViewInit {


  signInFormSubmitted : boolean = false
  signInForm: FormGroup;
  
  faUser = faUser;
  faLock = faLock;

  constructor(
    public translatePipe: TranslatePipe,
    private authenticationService : AuthenticationService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) { 
  }

  ngOnInit(){
    this.spinner.show();
    this.prepareSignInForm();
  }

  ngAfterViewInit(){
    this.spinner.hide();
  }

  signIn(){
    this.spinner.show();
    this.signInFormSubmitted = true;
    if(this.signInForm.valid){
      const formData = this.signInForm.getRawValue();
      const model = new SignInModel({
        ...formData
      });
      this.authenticationService.signIn(model);
      this.prepareSignInForm();
    }
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

}
