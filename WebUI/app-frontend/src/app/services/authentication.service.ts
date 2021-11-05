import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthenticationResult } from '../models/authentication-result.model';
import { SignInModel } from '../models/sign-in.model';
import { TokenStorageProvider } from '../providers/token-storage-provider.model';
import { TranslationProvider } from '../providers/translation-provider.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private url = `${environment.apiUrl}/user/account`;
  private headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*'
  });
  
  constructor(
    private http: HttpClient,
    private tokenStorageProvider : TokenStorageProvider
    ) { }

  signIn(model : SignInModel) : Promise<any>{
    return this.http.post<AuthenticationResult>(`${this.url}/signIn`, model, {headers: this.headers})
    .toPromise()
    .then(res =>{
      this.tokenStorageProvider.setToken(res.token);
      return Promise.resolve();
    })
    .catch(this.handleError.bind(this));
  }

  logOut() {
    this.tokenStorageProvider.removeToken();
  }

  private async handleError(error: any) {
    if (error.status === 401) {
      return Promise.reject('account_not_found');
    } else if (error.status === 409) {
      return Promise.reject('wrong_password');
    } else {
      return Promise.reject('something_went_wrong');
    }
}
}
