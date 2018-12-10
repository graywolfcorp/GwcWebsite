import { NgModule } from '@angular/core';
import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from '../shared/shared.module';
import { BlogModule } from '../blog/blog.module';
import { HomeComponent } from './home.component';
import { ServicesComponent } from './services.component';

@NgModule({
  imports: [
    SharedModule,
    HomeRoutingModule,
    BlogModule
  ],
  declarations: [
    HomeComponent, 
    ServicesComponent    
  ]
})
export class HomeModule { }
