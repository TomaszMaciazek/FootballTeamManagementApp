import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Country } from '../models/country.model';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private url = `${environment.apiUrl}/country`
  
  constructor(private http: HttpClient) { }

  getCountries(): Promise<Country[]> {
    return this.http.get<Country[]>(this.url).toPromise();
  }
}
