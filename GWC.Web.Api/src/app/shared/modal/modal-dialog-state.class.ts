import { Injectable, Component } from "@angular/core";
import { NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
import { ModalDialogOptions } from "./modal-dialog-options.interface";

@Injectable()
export class ModalDialogState {
  options: ModalDialogOptions;
  modal: NgbModalRef;
}