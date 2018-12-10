import { Component, OnInit, Input, Inject, LOCALE_ID } from '@angular/core';
import { DropDownValue } from '../home/home.component';
import { KeyValuePair } from '../data/keyValuePair';
import { TagDto } from '../data/tagDto';
import { BlogService } from './blog.service';
//import { DatePipe } from '@angular/common';

import { formatDate } from '@angular/common';


@Component({
  selector: 'gwc-blog-post-line',
  templateUrl: './blog-post-line.component.html',
  styles: []
})
export class BlogPostLineComponent implements OnInit {
  @Input()title: string;
  @Input()url: string;
  @Input()blogUrlYear: string;
  @Input()id: string;
  @Input()date: string;
  @Input()content: string;
  @Input()tags: TagDto[];
  @Input() gistId: string;
  @Input() gistHeight: string;
  @Input() summary: boolean = false;

  blogDate:string;
  allTags: TagDto[];
  
  constructor(private blogService: BlogService,
            @Inject(LOCALE_ID) private locale: string) {
    this.allTags = blogService.tags;
  }

  ngOnInit() {

    this.url = '/blogs/posts/' +this.url;
    this.blogUrlYear = '/blogs/posts/years/' + formatDate(this.date,'yyyy', this.locale);
    this.blogDate = formatDate(this.date,'MM/dd/yyyy', this.locale); 

    if (this.summary){
      var summaryLink = '...  [Read more](' + this.url +' "Read more")';
      var lineConent = this.content.substring(0,300)
      var lastIndex = lineConent.lastIndexOf(" ");
      this.content = lineConent.substring(0, lastIndex) + summaryLink;
    }
    
  }

}
