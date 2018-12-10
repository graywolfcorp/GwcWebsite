import { Component, OnInit } from '@angular/core';
import { BlogService } from '../blog/blog.service';
import { KeyValuePair } from '../data/keyValuePair';

@Component({
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  public tags: KeyValuePair[] = []; 
  public blogs: any;
  public displayPagination: boolean = false;
  public collectionSize: number;
  public pageSize: number;
  public currentPage: number = 1;

  constructor(public blogService: BlogService ) { }

  ngOnInit() {
    
    this.tags = this.blogService.tags;

    this.blogService.getPosts("1").subscribe( data => {
      this.blogService.blogs = data;
      this.blogs =  this.blogService.blogs.items;      
      this.collectionSize = this.blogService.blogs.total;
      this.pageSize = this.blogService.postsPerPage;
      if (data.total > this.blogService.postsPerPage){
        this.displayPagination = true;
      }
      
    }); 
  }

  onPageChange(event){
    this.displayPagination = false;
    this.currentPage = event;

    this.blogService.getPosts(event).subscribe( data => {
      this.blogService.blogs = data;
      this.blogs =  this.blogService.blogs.items;      
      if (data.total > this.blogService.postsPerPage){
        this.displayPagination = true;
      }
    }); 

  }

}

export class DropDownValue{
  public description: string;
  public id:  string;

}