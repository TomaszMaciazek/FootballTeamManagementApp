import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { Gender } from 'src/app/enums/gender';
import { PlayerPosition } from 'src/app/enums/player-position';
import { Player } from 'src/app/models/player/player.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { PlayerService } from 'src/app/services/player.service';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.scss']
})
export class PlayerComponent implements OnInit {

  id:string;
  player: Player;

  displayChangePasswordDialog: boolean = false;

  gender = Gender;
  constructor(
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private translationProvider: TranslationProvider,
    private playerService: PlayerService,
    private router: Router,
    private confirmationService: ConfirmationService,

  ){}

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

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      this.playerService.getPlayerById(this.id).then(res => {
        this.player = res;
        this.spinner.hide();
      })
      .catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
      });
    });
  }

  closeDialog(){
    this.displayChangePasswordDialog = false;
  }

  reload(){
    this.router.navigate(['/refresh']);
  }

  confirmToggle() {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_toggle'),
        header: this.translationProvider.getTranslation('toggle_status'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.spinner.show();
            this.playerService.togglPlayerStatus(this.player.id).then(x => {
              this.toastr.success(this.translationProvider.getTranslation('success'));
              this.spinner.hide();
              this.reload();
            })
            .catch(error => {
              this.toastr.error(this.translationProvider.getTranslation(error));
              this.spinner.hide();
            });
        }
    });
  }

}
