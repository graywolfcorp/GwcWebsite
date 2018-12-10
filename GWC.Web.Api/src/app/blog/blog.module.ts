import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { BlogPostLineComponent } from './blog-post-line.component';
import { MarkdownModule, MarkdownComponent } from 'ngx-markdown';
import { FilterTagPipe } from './filter-tag.pipe';
import { BlogComponent } from './blog.component';
import { BlogRoutingModule } from './blog-routing.module';

@NgModule({
  imports: [
    MarkdownModule.forChild(),
    SharedModule,
    BlogRoutingModule
  ],
  declarations: [
    BlogPostLineComponent,
    FilterTagPipe,
    BlogComponent
  ],
  exports: [
    BlogPostLineComponent,
    FilterTagPipe
  ]
})
export class BlogModule { }
