import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn, FormArray, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { FormValidationService } from './form-validation.service';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonService } from './common.service';
import { environment } from './../../environments/environment';

@Component({
  selector: 'gwc-contact-us-modal',
  templateUrl: './contact-us-modal.component.html' 
})
export class ContactUsModalComponent implements OnInit {
  @Input()defaultIssue: string = 'General Questions';
  contactForm: FormGroup;
  emailMessage: string;

  apiEndpoint: string = environment.apiEndpoint;
  recaptchaSecret: string = environment.recaptchaSecret;

    contactHelpYou = [
      'General Questions',
      'Interested in Coding Services',
      'Interested in Partnering Opportunity',
      'Just testing'
  ];

  constructor(
    public activeModal: NgbActiveModal,  
    private fb: FormBuilder,
    public FormValidationService: FormValidationService, 
    private http: HttpClient,
    private _commonService: CommonService) {}

  ngOnInit(): void {
    this.contactForm = this.fb.group({
      name: ['', [Validators.required]],
      emailGroup: this.fb.group({
          email: [, [Validators.required, Validators.pattern('[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+')]],
          confirmEmail: [, Validators.required],
        }, {validator: this._commonService.emailMatcher}),
      phoneNumber: [, Validators.required],
      helpYou: [this.defaultIssue, Validators.required],
      comments: '',
      captcha: ['', Validators.required]

    });
      
  }
  
  closeModal() {
    this.activeModal.close();
  }  

  resolved(captchaResponse: string) {
    console.log(`Resolved captcha with response ${captchaResponse}:`);
  }
  
 submitForm() {
    this.emailMessage = '';
    this.FormValidationService.markFormGroupTouched(this.contactForm) 
    
    if (this.contactForm.valid) {

      let postData = {
        'name': this.contactForm.get('name').value,
        'email': this.contactForm.get('emailGroup.email').value,
        'phoneNumber': this.contactForm.get('phoneNumber').value,
        'helpyou': this.contactForm.get('helpYou').value,
         'comments': this.contactForm.get('comments').value,
        'clientResponse': this.contactForm.get('captcha').value
      };
  
      const _headers = new HttpHeaders();
      const headers = _headers.append('Content-Type', 'application/json')
                              .append('Accept', '*/*');
  
      this.http.post(this.apiEndpoint + 'home/contact',postData,
      {headers: headers}).subscribe(response => {
        this.emailMessage = 'Your email has been sent.';
      },(err: HttpErrorResponse) => {
        console.log(err.error);
        console.log(err.name);
       // console.log(err.message);
        console.log(err.status);
        this.emailMessage = 'Your email could not be sent. Please try again later.';
      });;
      
    } 
  }

}