import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TrComponent } from './tr.component';
import { HomeLayoutComponent } from '../layout/home-layout.component';
import { SourcesComponent } from './sources.component';


@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: 'tr',
        component: HomeLayoutComponent,
        canActivate: [],
        children: [
            { path: '', component: TrComponent },
            { path: 'sources', component: SourcesComponent }
        ]
      }
    ])
  ],
  exports: [ RouterModule ]
})
export class TrRoutingModule { }