import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrComponent } from './tr.component';
import { TrRoutingModule } from './tr-routing.module';
import { SourcesComponent } from './sources.component';
import { AgGridModule } from 'ag-grid-angular';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  imports: [  
    CommonModule,
    TrRoutingModule,
    AgGridModule.withComponents([]),
    NgbModule
  ],
  declarations: [TrComponent, SourcesComponent]
})
export class TrModule { }
