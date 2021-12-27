import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { TrainingService } from 'src/app/services/training.service';

@Component({
  selector: 'app-edit-training',
  templateUrl: './edit-training.component.html',
  styleUrls: ['./edit-training.component.scss']
})
export class EditTrainingComponent implements OnInit {

  public form: FormGroup;
  private id: string;
  
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private translationProvider: TranslationProvider,
    private trainingService: TrainingService
  ) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((routeParams: Params) => {
      this.id = routeParams['id'];
      this.createForm();
      this.trainingService.getById(this.id).then( res =>{
        this.form.controls['Description'].setValue(res.description);
        this.form.controls['Title'].setValue(res.title);
        this.form.controls['Date'].setValue(res.date);
      })
    });

  }

  createForm() {
    this.form = this.formBuilder.group({
        'Description': ["", Validators.required],
        'Title': ["", Validators.required],
        'Date': [new Date(), Validators.required]
    });
  }

}
