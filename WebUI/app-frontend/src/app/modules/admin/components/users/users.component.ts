import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService, LazyLoadEvent } from 'primeng/api';
import { UserQuery } from 'src/app/models/queries/user-query.model';
import { Role } from 'src/app/models/role.model';
import { User } from 'src/app/models/user.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { RoleService } from 'src/app/services/role.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "asc";
  sortColumn: string = "Surname";
  cols = [
    { field: 'name', header: 'name' },
    { field: 'email', header: 'email' },
    { field: 'role', header: 'role' },
  ];

  boolFilterOptions = [
    {label: 'both', value: null},
    {label: 'yes', value: true},
    {label: 'no', value: false}
  ];

  users: User[];
  roles: Role[];
  
  searchQuery: string = null;
  isActive: boolean = null;

  showFilters : boolean = false;

  form: FormGroup;

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private roleService: RoleService,
    private userService: UserService,
    private translationProvider: TranslationProvider,
    private confirmationService: ConfirmationService,
    private tokenStorageProvider : TokenStorageProvider,
    private toastr : ToastrService,
    private formBuilder : FormBuilder
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.createForm();
    this.roleService.getRoles()
    .then(res => {
      this.roles = res;
      this.getUsers();
    });
  }

  getUsers(){
    var query = new UserQuery({
      pageNumber: this.pageNumber,
      pageSize: this.rowNumbers,
      orderByColumnName: this.sortColumn,
      orderByDirection: this.sortOrder,
      isActive: this.isActive,
      queryString: this.searchQuery
    });
    this.userService.getUsers(query)
    .then(res => {
      this.users = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    });
  }

  addNewUser(event: any) {
    this.router.navigate(['/admin/users/add']);
  }

  editUser(id: string, event: any) {
    this.router.navigate(['/admin/users/edit', { id: id }]);
  }

  loadUsers(event: LazyLoadEvent) {
    if(event.sortField){
      this.sortColumn = event.sortField;
    }
    if(event.sortOrder == -1){
      this.sortOrder = "desc";
    }
    else{
      this.sortOrder = "asc";
    }
    if(event.rows){
      this.rowNumbers = event.rows;
    }
    this.pageNumber = Math.floor(event.first / this.rowNumbers) + 1;
    this.spinner.show();
    this.getUsers();
  }

  confirmDeactivate(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_deactivate_user'),
        header: this.translationProvider.getTranslation('deactivate'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.spinner.show();
            this.userService.deactivate(id).then(x => {
              this.toastr.success(this.translationProvider.getTranslation('success'));
              this.getUsers();
            });
        }
    });
  }

  confirmActivate(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_activate_user'),
        header: this.translationProvider.getTranslation('activate'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            this.spinner.show();
            this.userService.activate(id).then(x => {
              this.toastr.success(this.translationProvider.getTranslation('success'));
              this.getUsers();
            });
        }
    });
  }

  confirmDelete(id: string) {
    this.confirmationService.confirm({
        message: this.translationProvider.getTranslation('confirm_delete_user'),
        header: this.translationProvider.getTranslation('delete'),
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
          this.spinner.show();
          this.userService.delete(id).then(x => {
            this.toastr.success(this.translationProvider.getTranslation('success'));
            this.getUsers();
          });
        }
    });
  }

  isCurrentUser(id: string){
    return id === this.tokenStorageProvider.getUserId();
  }

  createForm(){
    this.form = this.formBuilder.group({
      'SearchQuery': [null, Validators.nullValidator],
      'IsActive': [null, Validators.nullValidator]
    });
  }

  filter(){
    if(this.form.controls['SearchQuery'].value != null && this.form.controls['SearchQuery'].value.length > 0){
      this.searchQuery = this.form.controls['SearchQuery'].value;
    }
    else{
      this.searchQuery = null;
    }

    if(this.form.controls['IsActive'].value != null){
      this.isActive = this.form.controls['IsActive'].value;
    }
    else{
      this.isActive = null;
    }

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "Surname";
    this.getUsers();
  }

  resetFilter() {
    this.searchQuery = null;
    this.isActive = null;

    this.form.controls['SearchQuery'].setValue(null);
    this.form.controls['IsActive'].setValue(null);

    this.pageNumber = 1;
    this.sortOrder = "asc";
    this.sortColumn= "Surname";

    this.getUsers();
  }

}
