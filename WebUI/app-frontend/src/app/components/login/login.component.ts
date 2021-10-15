import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SignInModel } from 'src/app/models/sign-in.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { faUser, faLock } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {


  signInFormSubmitted : boolean = false
  signInForm: FormGroup;
  
  faUser = faUser;
  faLock = faLock;

  constructor(
    private authenticationService : AuthenticationService
  ) { 
  }

  ngOnInit(): void {
    this.prepareSignInForm();
  }


  signIn(){
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
