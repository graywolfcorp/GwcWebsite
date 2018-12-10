import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of, throwError } from 'rxjs';
import { injectArgs } from '@angular/core/src/di/injector';
import { catchError, map } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { BlogService } from './blog.service';
import { PostDto } from '../data/postDto';

@Injectable({providedIn: 'root'})
export class BlogResolverService implements Resolve<PostDto> {

    constructor(
        private blogService: BlogService,
        private router: Router){}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<PostDto> {        
        var blogUrl = route.url[1].path;

        if (blogUrl == 'years'){
            return this.blogService.getPostsByYear(route.url[2].path)
                .pipe(
                    map(post =>{
                        if (post){                        
                            return post;
                        }
                        this.router.navigate(['/']);
                    } ),
                    catchError(this.handleError)
                );            
        } else if (blogUrl == 'tags'){
            var tagDto = this.blogService.getTagBySlug(route.url[2].path);

            return this.blogService.getPostsByTag(tagDto.id,"1")
                .pipe(
                    map(post =>{
                        if (post){                        
                            return post;
                        }
                        this.router.navigate(['/']);
                    } ),
                    catchError(this.handleError)
                );            
        }
        else {
            return this.blogService.getPostByUrl(blogUrl)
                .pipe(
                    map(post =>{
                        if (post){                        
                            return post;
                        }
                        this.router.navigate(['/']);
                    } ),
                    catchError(this.handleError)
                );
        }
    }

    private handleError(error: HttpErrorResponse): Observable<any> {        
        console.error(error);
        return throwError(error.error || 'Server error');
    }
}