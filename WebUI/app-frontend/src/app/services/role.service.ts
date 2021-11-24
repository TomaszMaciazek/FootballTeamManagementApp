import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Role } from '../models/role.model';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private url = `${environment.apiUrl}/role`
  
  constructor(private http: HttpClient) { }

  getRoles(): Promise<Role[]> {
    return this.http.get<Role[]>(this.url).toPromise();
  }
}
