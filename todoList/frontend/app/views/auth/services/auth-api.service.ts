import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthApiService {
  constructor(private http: HttpClient) {}

  loginUrl: string = 'https://localhost:7034/api/Persons/Login';

  login = (model: any): Observable<any> => {
    return this.http.post<any>(this.loginUrl, model);
  };

  registerUrl: string = 'https://localhost:7034/api/Persons/CreatePersonInfo';

  register = (model: any): Observable<any> => {
    return this.http.post<any>(this.registerUrl, model);
  };
}
