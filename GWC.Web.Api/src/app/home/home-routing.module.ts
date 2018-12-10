import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home.component';
import { HomeLayoutComponent } from '../layout/home-layout.component';
import { ServicesComponent } from './services.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: HomeLayoutComponent,
        canActivate: [],
        children: [
            { path: '', component: HomeComponent },
            { path: 'services', component: ServicesComponent }
        ]
      }
    ])
  ],
  exports: [ RouterModule ]
})
export class HomeRoutingModule { }
