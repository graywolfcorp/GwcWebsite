import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BlogComponent } from './blog.component';
import { BlogResolverService } from './blog-resolver.service';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: 'blogs',
        canActivate: [],
        children: [
          { 
            path: '**', 
            component: BlogComponent,
            data: 
            {
              pageTitle: 'Service Information',
              meta: 'Test meta infomration' 
            },
            resolve: {
              blogResolve: BlogResolverService
            } 

          }
        ]
      }
    ])
  ],
  exports: [ RouterModule ],
  providers: [BlogResolverService]
})
export class BlogRoutingModule { }
