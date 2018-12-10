import { Injectable } from '@angular/core';
import { throwError as observableThrowError, Observable } from 'rxjs';
import { map, catchError } from "rxjs/operators";
import { Subject } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { PostDto } from '../data/postDto';
import { KeyValuePair } from '../data/keyValuePair';
import { TagDto } from '../data/tagDto';

@Injectable({
  providedIn: 'root'
})
export class BlogService {
  hasData: boolean;

  public postsPerPage: number = 5;
  private subjectPost = new Subject<PostDto>();
  public post = this.subjectPost.asObservable();

  public blogs: PostDto;

  public tags: TagDto[] = [
    { description: 'Angular', id: '3ZbGNsHyNiWMGs0eC6g6yc', slug: 'angular' },
    { description: 'Azure', id: '6oGOA3XdUkiewoksuY2MiI', slug: 'azure' },
    { description: 'CSS', id: '5JvgOrRr7amACWAaW4eYi8', slug: 'css' },
    { description: '.Net', id: '5qBETCh41asWIyI0skseyk', slug: 'net' },
    { description: 'Sql Server', id: '4eqlmOhlTqIEa4OQ2Imei6', slug: 'sqlserver' },
    { description: 'Office 365', id: '4z2fygYdCEykcscIIUmkUW', slug: 'office365' },
    { description: 'Entity Framework', id: '1UzZhJshriuk0Cicq6YOGA', slug: 'ef' },
    { description: '.Net Core', id: '3F0gczsZwcoyOWUMScsSWc', slug: 'netcore' },
    { description: 'Python', id: '49mA6ts1DGsYw24AAmMUsC', slug: 'python' },
    { description: 'Other', id: '5Wtx7RmT2ouM4SUyW6Aq2o', slug: 'other' },
  ];

  constructor(private http: HttpClient) { }

  headers = new HttpHeaders()
    .append('Content-Type', 'application/json')
    .append('Accept', 'application/json')
    ;

  getPost(id: string): Observable<PostDto> {
    const url = `${environment.apiEndpoint}blogs/${id}`;
    var data = this.http.get(url).pipe(
      map(response => response),
      catchError(this.handleError));

    return data;
  }

  getPosts(skip: string = "0"): Observable<PostDto> {
    const url = `${environment.apiEndpoint}blogs/posts/${skip}`;
    var data = this.http.get(url).pipe(
      map(response => response),
      catchError(this.handleError));
    return data;
  }

  getPostByUrl(blogUrl: string): Observable<PostDto> {
    const url = `${environment.apiEndpoint}blogs/posts/urls/${blogUrl}`;
    var data = this.http.get(url).pipe(
      map(response => response),
      catchError(this.handleError));

    return data;
  }

  getPostsByTag(tag: string, skip: string = "1"): Observable<PostDto> {
    const url = `${environment.apiEndpoint}blogs/posts/tags/${tag}/${skip}`;
    var data = this.http.get(url).pipe(
      map(response => response),
      catchError(this.handleError));

    return data;
  }

  getPostsByYear(year: string, skip: string = "1"): Observable<PostDto> {
    const url = `${environment.apiEndpoint}blogs/posts/years/${year}/${skip}`;
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

  getTagBySlug(slug: string): TagDto {
    var tagDto = this.tags.filter(x => x.slug == slug)[0];
    return tagDto;
  }

}
