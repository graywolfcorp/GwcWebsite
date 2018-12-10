import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable()
export class CommonService {

  constructor() { }

  scrollTo(name: string, options: object = null): void {
    if (name == undefined || name == null || name == '') return;
    if (options == null){
      options = {behavior: 'smooth', block: 'start', inline: 'nearest'};
    }
    const element = document.querySelector('#' + name);
    if (element) {
        setTimeout(() => {
            element.scrollIntoView(options);
        }, 500 );
    }
  }

  emailMatcher(c: AbstractControl): {[key: string]: boolean} | null {
    let emailControl = c.get('email');
    let confirmControl = c.get('confirmEmail');
  
    if (emailControl.pristine || confirmControl.pristine) {
      return null;
    }
  
    if (emailControl.value === confirmControl.value) {
        return null;
    }
    return { 'match': true };
  }

  dynamicSort(property) {
    var sortOrder = 1;
    if(property[0] === "-") {
        sortOrder = -1;
        property = property.substr(1);
    }
    return function (a,b) {
        var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
        return result * sortOrder;
    }
}
}
