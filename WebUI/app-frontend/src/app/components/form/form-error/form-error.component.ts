import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-form-error',
  templateUrl: './form-error.component.html'
})
export class FormErrorComponent{
  @Input() control: AbstractControl;
  @Input() submitted: boolean;
  @Input() error: string;
}
