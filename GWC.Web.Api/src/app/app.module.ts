import { BrowserModule } from '@angular/platform-browser';
import { AgGridModule } from 'ag-grid-angular';

import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeModule } from './home/home.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LayoutModule } from './layout/layout.module';
import { ContactUsModalComponent } from './shared/contact-us-modal.component';
import { ModalDialogConfirmComponent } from './shared/modal/modal-dialog-confirm.component';
import { DirectivesModule } from './directives/directives.module';
import { TrModule } from './tr/tr.module';
import { NavbarHeaderComponent } from './layout/navbar-header.component';
import { MarkdownModule } from 'ngx-markdown';
import { BlogModule } from './blog/blog.module';

// import { DirectivesModule } from './directives/directives.module';
// import { FormModalComponent } from './shared/form-modal.component';
// import { ContactUsModalComponent } from './shared/contact-us-modal.component';
// import { ModalDialogConfirmComponent } from './shared/modal/modal-dialog-confirm.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarHeaderComponent
  ],
  imports: [
    NgbModule.forRoot(),
    MarkdownModule.forRoot(),
    BrowserModule,      
    DirectivesModule,
    HttpClientModule,    
    LayoutModule,
    HomeModule,   
    BlogModule,
    TrModule,     
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [    
    ContactUsModalComponent,
    ModalDialogConfirmComponent      
  ] 
})
export class AppModule { }
