import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { ChangePassword } from 'src/app/models/commands/change-password.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  @Input() userId: string;
  @Output() confirmUpdatePassword = new EventEmitter<boolean>();

  public form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private confirmationService: ConfirmationService,
    private translationProvider: TranslationProvider,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm() {
    this.form = this.formBuilder.group({
        'OldPassword': [null, Validators.required],
        'NewPassword': [null, Validators.required],
        'NewPasswordRepeat': [null, Validators.required]
    });
  }

  submit(){
    if(this.form.valid){
      if(this.form.controls['NewPasswordRepeat'].value == this.form.controls['NewPassword'].value){
        if(this.form.controls['OldPassword'].value != this.form.controls['NewPassword'].value){
          this.confirmationService.confirm({
            message: this.translationProvider.getTranslation('confirm_change_passsword'),
            header: this.translationProvider.getTranslation('change_password'),
            icon: 'pi pi-info-circle',
            accept: () => {
              var command = new ChangePassword({
                userId: this.userId,
                oldPassword: this.form.controls['OldPassword'].value,
                newPassword: this.form.controls['NewPassword'].value
              });
              this.spinner.show();
              this.userService.updatePassword(command)
              .then(res => {
                this.toastr.success(this.translationProvider.getTranslation('success'));
                this.spinner.hide();
                this.confirmUpdatePassword.emit(true);
              })
              .catch(error => {
                this.toastr.error(this.translationProvider.getTranslation(error));
                this.spinner.hide();
              });
            }
          });
        }
        else{
          this.toastr.warning('nowe hasło nie może być takie samo jak stare');
        }
      }
      else{
        this.toastr.warning('nowe hasło i potwierdzenie posiada różne wartości');
      }
    }
  }

  reset(){
    this.form.controls['OldPassword'].setValue(null);
    this.form.controls['NewPassword'].setValue(null);
    this.form.controls['NewPasswordRepeat'].setValue(5);
  }

}
