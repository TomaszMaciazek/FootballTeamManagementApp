import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, SelectItem } from 'primeng/api';
import { QuestionType } from 'src/app/enums/question-type';
import { CreateSurveyQuestionOption } from 'src/app/models/commands/create-survey-question-option.model';
import { CreateSurveyQuestion } from 'src/app/models/commands/create-survey-question.model';
import { CreateSurveyCommand } from 'src/app/models/commands/create-survey.model';
import { SelectUser } from 'src/app/models/user/select-user.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { SurveyService } from 'src/app/services/survey.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-survey',
  templateUrl: './create-survey.component.html',
  styleUrls: ['./create-survey.component.scss']
})
export class CreateSurveyComponent implements OnInit {

  users: SelectUser[];
  groupedUsers = null;

  form : FormGroup;

  questionType = QuestionType;

  selectedOption = [];
  respondents: SelectUser[];

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private translationProvider: TranslationProvider,
    private tokenStorageProvider: TokenStorageProvider,
    private surveyService: SurveyService,
    private confirmationService: ConfirmationService,
    private userService: UserService
  ) { }

  public questionTypes: SelectItem[] = [
    {label: "question_text", value: QuestionType.Text},
    {label: "question_single_choice", value: QuestionType.SingleChoice},
    {label: "question_multiple_choice", value: QuestionType.MutipleChoice},
    {label: "question_rating", value: QuestionType.Rating}
  ];

  comparePlayer = (a: SelectUser, b: SelectUser) => {
    if (a.surname < b.surname)
      return -1;
    if (a.surname > b.surname)
      return 1;
    return 0;
  };

  ngOnInit() {
    this.createForm();
    this.spinner.show();
    this.userService.getRespondentsList().then(res => {
      this.users = res.sort(this.comparePlayer);
      this.spinner.hide();
      this.groupedUsers = this.groupUsers(res);
    });
  }

  private createForm() {
    let surveyQuestions = new FormArray([]);

    this.form = new FormGroup({
      'Title': new FormControl('', [Validators.required]),
      'Description': new FormControl('', [Validators.required]),
      'Respondents': new FormControl([], [Validators.nullValidator]),
      'SurveyQuestions': surveyQuestions,
      'IsAnonymous': new FormControl(false, [Validators.required])
    });

    this.onAddQuestion();

  }

  groupUsers(users: SelectUser[]){
    return [
      {
        label: 'players', value: 'players', items: users.filter(x => x.role.name == 'player')

      },
      {
        label: 'coaches', value: 'coaches', items: users.filter(x => x.role.name == 'coach')
      },
      {
        label: 'administrators', value: 'administrators', items:  users.filter(x => x.role.name == 'admin')
      }
    ]
  }

  onAddQuestion() {
   
    const surveyQuestionItem = new FormGroup({
      'questionTitle': new FormControl('', Validators.required),
      'questionType': new FormControl(null, Validators.required),
      'isRequired' : new FormControl(false, Validators.required),
      'questionGroup': new FormGroup({})
    });

    (<FormArray>this.form.get('SurveyQuestions')).push(surveyQuestionItem);

  }

  onRemoveQuestion(index) {

  
    this.form.controls.SurveyQuestions['controls'][index].controls.questionGroup = new FormGroup({});
    this.form.controls.SurveyQuestions['controls'][index].controls.questionType = new FormControl({});

    (<FormArray>this.form.get('SurveyQuestions')).removeAt(index);
    this.selectedOption.splice(index,1);

  }


  onSeletQuestionType(index, questionType : QuestionType) {
    if (questionType == QuestionType.SingleChoice || questionType == QuestionType.MutipleChoice) {
      this.addOptionControls(index, questionType)
    }
    else{
      this.clearFormArray((<FormArray>this.form.controls.SurveyQuestions['controls'][index].controls.questionGroup.controls.options));
    }
  }

  addOptionControls(index, questionType : QuestionType) {

    let options = new FormArray([]);

    (this.form.controls.SurveyQuestions['controls'][index].controls.questionGroup).addControl('options', options);

    this.clearFormArray((<FormArray>this.form.controls.SurveyQuestions['controls'][index].controls.questionGroup.controls.options));

    this.addOption(index);
    this.addOption(index);
  }


  private clearFormArray(formArray: FormArray) {
    while (formArray && formArray.length !== 0) {
      formArray.removeAt(0)
    }
  }


  addOption(index) {
    const optionGroup = new FormGroup({
      'optionText': new FormControl('', Validators.required),
    });
    (<FormArray>this.form.controls.SurveyQuestions['controls'][index].controls.questionGroup.controls.options).push(optionGroup);
  }

  removeOption(questionIndex, itemIndex) {
    (<FormArray>this.form.controls.SurveyQuestions['controls'][questionIndex].controls.questionGroup.controls.options).removeAt(itemIndex);
  }

  confirmSubmit(){
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_create_survey'),
      header: this.translationProvider.getTranslation('survey_new'),
      icon: 'pi pi-info-circle',
      accept: () => {
        this.submit();
      }
    });
  }

  submit(){
    this.spinner.show();
    let formData = this.form.value;

    var surveyCommand = new CreateSurveyCommand();
    surveyCommand.questions = [];
    surveyCommand.authorId = this.tokenStorageProvider.getUserId();
    surveyCommand.title = formData.Title;
    surveyCommand.isAnonymous = formData.IsAnonymous;
    surveyCommand.description = formData.Description;
    surveyCommand.respondentsIds = formData.Respondents.map(x => x.id);

    let surveyQuestions = formData.SurveyQuestions;

    surveyQuestions.forEach((question, index, array) => {

      var questionCommand = new CreateSurveyQuestion();
      questionCommand.options = [];
      questionCommand.type = question.questionType;
      questionCommand.isRequired = question.isRequired;
      questionCommand.number = index + 1;
      questionCommand.description = question.questionTitle;

      if (question.questionGroup.hasOwnProperty('options')) {
        question.questionGroup.options.forEach((option, index) => {
          let optionItem: CreateSurveyQuestionOption = {
            label: option.optionText,
            value: index
          }
          questionCommand.options.push(optionItem)
        });
      }
 
      surveyCommand.questions.push(questionCommand)


    });


    this.surveyService.createSurvey(surveyCommand)
    .then(res => {
      this.toastr.success(this.translationProvider.getTranslation('success'));
      this.spinner.hide();
      this.router.navigate(["/surveys/created"]);
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });


  }

}
