import { Component } from "@angular/core";

/**
 * Options passed when opening a confirmation modal
 */
export interface ModalDialogOptions {
    title: string,
    message: string,
    component: Component
  
  }