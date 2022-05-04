import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { Gender } from 'src/app/enums/gender';
import { Coach } from 'src/app/models/coach/coach';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { CoachService } from 'src/app/services/coach.service';

@Component({
  selector: 'app-coach',
  templateUrl: './coach.component.html',
  styleUrls: ['./coach.component.scss']
})
export class CoachComponent implements OnInit {

  id:string;
  coach: Coach;

  displayChangePasswordDialog: boolean = false;

  gender = Gender;
  
  constructor(
    private activatedRoute: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private tokenStorageProvider: TokenStorageProvider,
    private translationProvider: TranslationProvider,
    private confirmationService: ConfirmationService,
    private coachService: CoachService,
    private router: Router
  ){}

  genderLabel = new Map<number, string>([
    [Gender.Female, 'female'],
    [Gender.Male, 'male']
  ]);

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      this.coachService.getCoachById(this.id).then(res => {
        this.coach = res;
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
            this.coachService.toggleCoachStatus(this.coach.id).then(x => {
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
