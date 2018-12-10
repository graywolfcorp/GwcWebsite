import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gwc-home-layout',
  template: 
    `
    
    <router-outlet></router-outlet>
    `,
    styleUrls: [ ]
})
export class HomeLayoutComponent implements OnInit {
  constructor() { }

  ngOnInit() {
  }

}
