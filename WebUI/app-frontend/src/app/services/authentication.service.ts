import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthenticationResult } from '../models/authentication-result.model';
import { SignInModel } from '../models/sign-in.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private url = `${environment.apiUrl}/account`;
  private headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*'
  });
  
  constructor(
    private http: HttpClient,
    private toastr : ToastrService
    ) { }

  signIn(model : SignInModel){
    this.http.post<AuthenticationResult>(`${this.url}/signIn`, model, {headers: this.headers})
    .toPromise()
    .then(res =>{
      localStorage.setItem("jwt", res.token)
      this.toastr.success("udało się")
    })
    .catch(this.handleError.bind(this));
  }

  logOut() {
    localStorage.removeItem("jwt");
  }

  private async handleError(error: any) {
    if (error.status === 401) {
      this.toastr.error("brak użytkownika o podanym loginie");
      return Promise.reject({});
    } else if (error.status === 400) {
      this.toastr.error("błąd");
      return Promise.reject(error);
    } else {
      return Promise.reject({});
    }
}
}
