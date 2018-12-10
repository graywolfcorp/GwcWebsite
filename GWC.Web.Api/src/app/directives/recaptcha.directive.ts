import { AfterViewInit, Directive, ElementRef, EventEmitter, forwardRef, Inject, Injectable, InjectionToken, Injector, Input, NgZone, OnInit, Output } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR, NgControl, Validators } from '@angular/forms';

export interface ControlValueAccessor {
  writeValue(obj: any) : void
  registerOnChange(fn: any) : void
  registerOnTouched(fn: any) : void
}

export interface ReCaptchaConfig {
    theme? : 'dark' | 'light';
    type? : 'audio' | 'image';
    size? : 'compact' | 'normal';
    tabindex? : number;
  }
  declare const grecaptcha : any;

  declare global {
    interface Window {
      grecaptcha : any;
      reCaptchaLoad : () => void
    }
  }

  @Directive({
    selector: '[nbRecaptcha]',
    exportAs: 'nbRecaptcha',
    providers: [
      {
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => ReCaptchaDirective),
        multi: true
      }
    ]    
  })
  export class ReCaptchaDirective implements OnInit, AfterViewInit, ControlValueAccessor {
    @Input() key : string;
    @Input() config : ReCaptchaConfig = {};
    @Input() lang : string;    
    
    @Output() captchaResponse = new EventEmitter<string>();
    @Output() captchaExpired = new EventEmitter();
    
    private control: FormControl;
    private widgetId : number;

    private onChange : ( value : string ) => void;
    private onTouched : ( value : string ) => void;

    constructor( private element : ElementRef,private  ngZone : NgZone, private injector : Injector  ) {}
  
    ngOnInit() {
      this.registerReCaptchaCallback();
      this.addScript();
    }
    
    ngAfterViewInit() {
      // this.control = this.injector.get(NgControl).control;
      // this.setValidator();
    }
  
    private setValidator() {
      // this.control.setValidators(Validators.required);
      // this.control.updateValueAndValidity();
    }

    writeValue( obj : any ) : void {
    }
  
    registerOnChange( fn : any ) : void {
      this.onChange = fn;
    }
  
    registerOnTouched( fn : any ) : void {
      this.onTouched = fn;
    }

    registerReCaptchaCallback() {
      window.reCaptchaLoad = () => {
        const config = {
          ...this.config,
          'sitekey': this.key,
          'callback': this.onSuccess.bind(this),
          'expired-callback': this.onExpired.bind(this)
        };
        this.widgetId = this.render(this.element.nativeElement, config);
      };
    }
  
    onExpired() {
      this.ngZone.run(() => {
        this.onChange(null);
        this.onTouched(null);
      });
    }
    
    onSuccess( token : string ) {
      this.ngZone.run(() => {
        this.onChange(token);
        this.onTouched(token);
        this.captchaResponse.emit(token);
      });
    }
    
    private render( element : HTMLElement, config ) : number {
      return grecaptcha.render(element, config);
    }

    reset() : void {
      if( !this.widgetId ) return;
      grecaptcha.reset(this.widgetId);
      this.onChange(null);
    }
  
    getResponse() : string {
      if( !this.widgetId )
        return grecaptcha.getResponse(this.widgetId);
    }

    addScript() {
      let script = document.createElement('script');
      const lang = this.lang ? '&hl=' + this.lang : '';
      script.src = `https://www.google.com/recaptcha/api.js?onload=reCaptchaLoad&render=explicit`;
      script.async = true;
      script.defer = true;
      document.body.appendChild(script);
    }
  
  }