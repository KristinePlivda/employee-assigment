import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { IEmployee } from '../shared/interfaces';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private url: string = 'api/employee/employeelist/';
  private urlbase: string = 'api/employee/';

  constructor(private http: HttpClient) { }

  getEmployees(): Observable<IEmployee[]> {
    return this.http.get<IEmployee[]>(this.url)
      .pipe(
        map(employees => {
          return employees;
        }),
        catchError(this.handleError)
      );
  }

  addEmployee(employee: IEmployee) {
    return this.http.post(this.urlbase, employee)
      .pipe(
        map(data => {
          return data;
        }),
        catchError(this.handleError)
      );
  }

  handleError(error: any) {
    console.error(error);
    return Observable.throw(error.json().error || 'Server error');
  }
}
