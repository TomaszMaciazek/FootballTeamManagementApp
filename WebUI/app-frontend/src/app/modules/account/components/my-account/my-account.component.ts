import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Gender } from 'src/app/enums/gender';
import { PlayerPosition } from 'src/app/enums/player-position';
import { AccountUser } from 'src/app/models/account/account-user.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.scss']
})
export class MyAccountComponent implements OnInit {

  user: AccountUser;
  gender = Gender;

  displayChangePasswordDialog: boolean = false;

  genderLabel = new Map<number, string>([
    [Gender.Female, 'female'],
    [Gender.Male, 'male']
  ]);

  playerPositionsLabel = new Map<number, string>([
    [PlayerPosition.AttackingMidfielder, 'position_attackingMidfielder'],
    [PlayerPosition.CenterBack, 'position_centerBack'],
    [PlayerPosition.DefensiveMiedfielder, 'position_defensiveMidfielder'],
    [PlayerPosition.Goalkeeper, 'position_goalkeeper'],
    [PlayerPosition.LeftBack, 'position_leftBack'],
    [PlayerPosition.RightBack, 'position_rightBack'],
    [PlayerPosition.Striker, 'position_striker']
  ]);


  constructor(
    private router: Router,
    private tokenStorageProvider: TokenStorageProvider,
    private spinner: NgxSpinnerService,
    private userService: UserService,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit(): void {
    let id = this.tokenStorageProvider.getUserId();
    this.spinner.show();
    this.userService.getMyAccount(id).then(res =>{
      this.user = res;
      console.log(this.user);
      this.spinner.hide();
    });
  }

  logout(){
    this.authenticationService.logOut();
    this.router.navigateByUrl('/login');
  }

}
