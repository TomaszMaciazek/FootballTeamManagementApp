import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Survey } from 'src/app/models/surveys/survey.model';
import { SimpleUser } from 'src/app/models/user/simple-user.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { SurveyService } from 'src/app/services/survey.service';
import { UserSurveyResultService } from 'src/app/services/user-survey-result.service';

@Component({
  selector: 'app-survey-results',
  templateUrl: './survey-results.component.html',
  styleUrls: ['./survey-results.component.scss']
})
export class SurveyResultsComponent implements OnInit {

  id: string;
  survey: Survey;
  respondents: Array<SimpleUser> = null;

  constructor(
    private activatedRoute: ActivatedRoute,
    private translationProvider: TranslationProvider,
    private userSurveyResultService: UserSurveyResultService,
    private surveyService: SurveyService,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.spinner.show();
      this.id = routeParams['id'];
      this.surveyService.getSurveyById(this.id).then(res => {
        this.survey = res;
        console.log(this.survey);
        if(!this.survey.isAnonymous){
          this.userSurveyResultService.getRespondentsFromSurvey(this.id).then(res2 =>{
            this.respondents = res2;
            console.log(res2);
            this.spinner.hide();
          })
        }
        else{
          this.spinner.hide();
        }
      })
      .catch(error => {
        this.toastr.error(this.translationProvider.getTranslation(error));
        this.spinner.hide();
      });
    });
  }

}
