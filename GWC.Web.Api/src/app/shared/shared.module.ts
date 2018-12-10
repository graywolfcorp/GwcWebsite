import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FilterByPipe } from './pipes/filter-by.pipe';
import { TelephonePipe } from './pipes/telephone.pipe';
import { CommonService } from './common.service';
import { ContactUsModalComponent } from './contact-us-modal.component';
import { FormValidationService } from './form-validation.service';
import { ModalDialogConfirmComponent } from './modal/modal-dialog-confirm.component';
import { ModalDialogService } from './modal/modal-dialog.service';
import { ModalDialogState } from './modal/modal-dialog-state.class';
import { ReCaptchaDirective } from '../directives/recaptcha.directive';
import { GistComponent } from './gist.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule    
  ],
  declarations: [
    FilterByPipe,
    TelephonePipe,
    ContactUsModalComponent,
    ReCaptchaDirective,
    ModalDialogConfirmComponent,
    GistComponent
  ],
  providers: [
    CommonService,
    FormValidationService,
    ModalDialogService,
    ModalDialogState
  ],
  exports: [
    CommonModule,
    FormsModule,
    GistComponent,
    ReactiveFormsModule,
    NgbModule,
    FilterByPipe,
    ModalDialogConfirmComponent
  ]
})
export class SharedModule { }
