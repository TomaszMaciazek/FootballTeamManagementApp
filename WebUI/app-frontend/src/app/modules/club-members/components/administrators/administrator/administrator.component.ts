import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AccountUser } from 'src/app/models/account/account-user.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.scss']
})
export class AdministratorComponent implements OnInit {

  id:string;
  user: AccountUser;

  displayChangePasswordDialog: boolean = false;
  
  constructor(
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private translationProvider: TranslationProvider,
    private userService: UserService
  ){}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      this.userService.getMyAccount(this.id).then(res =>{
        this.user = res;
        this.spinner.hide();
      });
    });
  }

  closeDialog(){
    this.displayChangePasswordDialog = false;
  }

}
