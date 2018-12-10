import { Injectable } from '@angular/core';
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { map, catchError } from "rxjs/operators";
import { Subject }    from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { SourceDto } from '../data/sourceDto';

@Injectable({
  providedIn: 'root'
})
export class TrService {
  hasData:boolean;
  
  private subjectSource = new Subject<SourceDto>();
  public source = this.subjectSource.asObservable();

  constructor(private http: HttpClient) { }

  headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Accept', 'application/json');

   getSources(): Observable<SourceDto[]>{
    const url = `${environment.apiEndpoint }sources`;
    var data = this.http.get(url).pipe(
      map(response => response),                       
      catchError(this.handleError));

    return data;

  }

  private handleError(error: HttpErrorResponse): Observable<any> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    console.error(error);
    return observableThrowError(error.error || 'Server error');
  }
}
