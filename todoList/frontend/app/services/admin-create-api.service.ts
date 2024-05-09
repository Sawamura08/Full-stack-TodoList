import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AdminCreateApiService {
  constructor(private http: HttpClient) {}

  TaskTypeUrl = 'https://localhost:7034/api/Persons/AddType';

  createTaskType = (model: any): Observable<void> => {
    return this.http.post<void>(this.TaskTypeUrl, model);
  };
}
