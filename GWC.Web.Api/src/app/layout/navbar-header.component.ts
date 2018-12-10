import { Component, OnInit, Input } from '@angular/core';
//import { ContactUsModalComponent } from '../shared/contact-us-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ContactUsModalComponent } from '../shared/contact-us-modal.component';

@Component({
  selector: 'gwc-navbar-header',
  templateUrl: './navbar-header.component.html',
  styleUrls: []
})
export class NavbarHeaderComponent implements OnInit {
  @Input()header:string = '';
  
  menuOpen: boolean = false;
  constructor(private _modalService: NgbModal) { }

  ngOnInit() {
  }

  toggleMenu(): void {
    let bool = this.menuOpen;
    this.menuOpen = bool === false ? true : false; 
  }

  showContactForm(defaultIssue: string):void{
    const modalRef = this._modalService.open(ContactUsModalComponent);
    modalRef.componentInstance.defaultIssue = defaultIssue;
    modalRef.result.then();
  }
}
