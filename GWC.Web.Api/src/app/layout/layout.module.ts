import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { HomeLayoutComponent } from './home-layout.component';
import { NavbarHeaderComponent } from './navbar-header.component';

@NgModule({
  declarations: [
    HomeLayoutComponent
    
  ],
  imports: [
    BrowserModule,
    RouterModule,
    NgbModule
  ]

})
export class LayoutModule { }
