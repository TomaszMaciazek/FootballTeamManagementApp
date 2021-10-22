import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthenticationResult } from '../models/authentication-result.model';
import { SignInModel } from '../models/sign-in.model';
import { TokenStorageProvider } from '../providers/token-storage-provider.model';

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
    private toastr : ToastrService,
    private tokenStorageProvider : TokenStorageProvider,
    private spinner: NgxSpinnerService
    ) { }

  signIn(model : SignInModel){
    this.http.post<AuthenticationResult>(`${this.url}/signIn`, model, {headers: this.headers})
    .toPromise()
    .then(res =>{
      this.tokenStorageProvider.setToken(res.token);
      this.toastr.success("udało się");
      this.spinner.hide();
    })
    .catch(this.handleError.bind(this));
  }

  logOut() {
    this.tokenStorageProvider.removeToken();
  }

  private async handleError(error: any) {
    if (error.status === 401) {
      debugger;
      this.toastr.error("brak użytkownika o podanym loginie");
      return Promise.reject({});
    } else if (error.status === 400) {
      this.toastr.error("błąd");
      return Promise.reject(error);
    } else {
      this.toastr.error("Hasło nieprawidłowe");
      return Promise.reject({});
    }
}
}
