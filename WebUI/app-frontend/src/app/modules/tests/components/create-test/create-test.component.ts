import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, SelectItem } from 'primeng/api';
import { QuestionType } from 'src/app/enums/question-type';
import { CreateTestQuestionOption } from 'src/app/models/commands/create-test-question-option.model';
import { CreateTestQuestion } from 'src/app/models/commands/create-test-question.model';
import { CreateTestCommand } from 'src/app/models/commands/create-test.model';
import { SelectUser } from 'src/app/models/user/select-user.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TestService } from 'src/app/services/test.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.scss']
})
export class CreateTestComponent implements OnInit {

  users: SelectUser[];
  groupedUsers = null;

  form : FormGroup;

  questionType = QuestionType;

  selectedOption = [];
  respondents: SelectUser[];
  passedMinimalValue: number = 0;
  maxScore: number = 0;


  displayMinimalScoreDialog: boolean = false;

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService,
    private translationProvider: TranslationProvider,
    private tokenStorageProvider: TokenStorageProvider,
    private testService: TestService,
    private confirmationService: ConfirmationService,
    private userService: UserService
  ) { }

  public questionTypes: SelectItem[] = [
    {label: "question_single_choice", value: QuestionType.SingleChoice},
    {label: "question_multiple_choice", value: QuestionType.MutipleChoice},
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
    let testQuestions = new FormArray([]);

    this.form = new FormGroup({
      'Title': new FormControl('', [Validators.required]),
      'Description': new FormControl('', [Validators.required]),
      'Respondents': new FormControl([], [Validators.nullValidator]),
      'TestQuestions': testQuestions,
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
   
    const testQuestionItem = new FormGroup({
      'questionTitle': new FormControl('', Validators.required),
      'questionType': new FormControl(null, Validators.required),
      'questionGroup': new FormGroup({})
    });

    (<FormArray>this.form.get('TestQuestions')).push(testQuestionItem);

  }

  onRemoveQuestion(index) {

  
    this.form.controls.TestQuestions['controls'][index].controls.questionGroup = new FormGroup({});
    this.form.controls.TestQuestions['controls'][index].controls.questionType = new FormControl({});

    (<FormArray>this.form.get('TestQuestions')).removeAt(index);
    this.selectedOption.splice(index,1);

  }


  onSeletQuestionType(index, questionType : QuestionType) {
    if (questionType == QuestionType.SingleChoice || questionType == QuestionType.MutipleChoice) {
      this.addOptionControls(index, questionType)
    }
    else{
      this.clearFormArray((<FormArray>this.form.controls.TestQuestions['controls'][index].controls.questionGroup.controls.options));
    }
  }

  addOptionControls(index, questionType : QuestionType) {

    let options = new FormArray([]);

    (this.form.controls.TestQuestions['controls'][index].controls.questionGroup).addControl('options', options);

    this.clearFormArray((<FormArray>this.form.controls.TestQuestions['controls'][index].controls.questionGroup.controls.options));

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
      'optionPoints': new FormControl(0, Validators.required),
    });
    (<FormArray>this.form.controls.TestQuestions['controls'][index].controls.questionGroup.controls.options).push(optionGroup);
  }

  removeOption(questionIndex, itemIndex) {
    (<FormArray>this.form.controls.TestQuestions['controls'][questionIndex].controls.questionGroup.controls.options).removeAt(itemIndex);
  }

  confirmSubmit($event){
    this.passedMinimalValue = $event;
    this.displayMinimalScoreDialog = false;
    this.confirmationService.confirm({
      message: this.translationProvider.getTranslation('confirm_create_test'),
      header: this.translationProvider.getTranslation('test_new'),
      icon: 'pi pi-info-circle',
      accept: () => {
        this.submit();
      },
      reject: () => {this.passedMinimalValue = 0;}
    });
  }

  showDialog(){
    if(this.form != null){
      this.maxScore = 0;
      let formData = this.form.value;
      let testQuestions = formData.TestQuestions;

      testQuestions.forEach(question => {
        if (question.questionGroup.hasOwnProperty('options')) {
          if(question.questionType == QuestionType.SingleChoice){
            let currentMax = 0;
            question.questionGroup.options.forEach(option => {
              if(option.optionPoints > currentMax){
                currentMax = option.optionPoints;
              }
            });
            this.maxScore = currentMax;
          }
          else if(question.questionType == QuestionType.MutipleChoice){
            question.questionGroup.options.forEach(option => {
              if(option.optionPoints > 0){
                this.maxScore += option.optionPoints;
              }
            });
          }
        }
      });
    }
    this.displayMinimalScoreDialog = true;
  }

  submit(){
    this.spinner.show();
    let formData = this.form.value;

    var testCommand = new CreateTestCommand();
    testCommand.questions = [];
    testCommand.authorId = this.tokenStorageProvider.getUserId();
    testCommand.title = formData.Title;
    testCommand.description = formData.Description;
    testCommand.respondentsIds = formData.Respondents.map(x => x.id);
    testCommand.passedMinimalValue = this.passedMinimalValue;

    let testQuestions = formData.TestQuestions;

    testQuestions.forEach((question, index, array) => {

      var questionCommand = new CreateTestQuestion();
      questionCommand.options = [];
      questionCommand.type = question.questionType;
      questionCommand.number = index + 1;
      questionCommand.description = question.questionTitle;

      if (question.questionGroup.hasOwnProperty('options')) {
        question.questionGroup.options.forEach((option, index) => {
          let optionItem: CreateTestQuestionOption = {
            label: option.optionText,
            value: index,
            points : option.optionPoints
          }
          questionCommand.options.push(optionItem)
        });
      }
 
      testCommand.questions.push(questionCommand)


    });

    this.testService.createTest(testCommand)
    .then(res => {
      this.toastr.success(this.translationProvider.getTranslation('success'));
      this.spinner.hide();
      this.router.navigate(["/tests/created"]);
    })
    .catch(error => {
      this.toastr.error(this.translationProvider.getTranslation(error));
      this.spinner.hide();
    });


  }

}
