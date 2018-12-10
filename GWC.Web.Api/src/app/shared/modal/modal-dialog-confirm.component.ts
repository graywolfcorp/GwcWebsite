import { Component } from "@angular/core";
import { ModalDialogOptions } from "./modal-dialog-options.interface";
import { ModalDialogState } from "./modal-dialog-state.class";

@Component({
    templateUrl: './modal-dialog-confirm.component.html'
  })
  export class ModalDialogConfirmComponent {
  
    options: ModalDialogOptions;
  
    constructor(private state: ModalDialogState) {
      this.options = state.options;
    }
  
    ok() {
      this.state.modal.close();
    }
  
    cancel() {
      this.state.modal.dismiss();
    }
  }
  
  