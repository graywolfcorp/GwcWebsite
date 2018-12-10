import { Component, OnInit } from '@angular/core';
import { BlogService } from './blog.service';
import { PostDto, Fields } from '../data/postDto';
import { ActivatedRoute } from '@angular/router';
import { TagDto } from '../data/tagDto';

@Component({
  selector: 'gwc',
  templateUrl: './blog.component.html',
  styles: []
})
export class BlogComponent implements OnInit {
  public blogs: any;
  public summary:boolean = true;
  public headerText:string = '';
  public displayPagination: boolean = false;
  public collectionSize: number;
  public pageSize: number;
  public currentPage: number = 1;
  private postFilter: string;
  private tagDto: TagDto;
  private postYear: string;

  constructor(private blogService: BlogService, private route: ActivatedRoute) {   
    this.route.url.subscribe(url => {
      this.postFilter = url[1].path;
      if (this.postFilter == 'tags'){
        this.tagDto = this.blogService.getTagBySlug(url[2].path);
        this.headerText = this.tagDto.description + ' Posts'
      } 
      if (this.postFilter == 'years'){        
        this.postYear = url[2].path;
        this.headerText =  'Posts for ' + this.postYear; 
      }   
    })

    this.route.data.subscribe(data => {
      this.summary = data.blogResolve.items.length > 1;
      this.blogs = data.blogResolve.items;
      this.collectionSize = data.blogResolve.total;
      this.pageSize = this.blogService.postsPerPage;
      if (data.total > this.blogService.postsPerPage){
        this.displayPagination = this.collectionSize > 1;
      }
    })
  }

  ngOnInit() {}

  onPageChange(event){
    this.displayPagination = false;
    this.currentPage = event;

    if (this.postFilter == 'tags'){
      this.blogService.getPostsByTag(this.tagDto.id, event).subscribe( data => {
        this.blogService.blogs = data;
        this.blogs =  this.blogService.blogs.items;      
        if (data.total > this.blogService.postsPerPage){
          this.displayPagination = true;
        }
      });
    }

    if (this.postFilter == 'years'){
      this.blogService.getPostsByYear(this.postYear, event).subscribe( data => {
        this.blogService.blogs = data;
        this.blogs =  this.blogService.blogs.items;      
        if (data.total > this.blogService.postsPerPage){
          this.displayPagination = true;
        }
      });
    }
  } 
}
