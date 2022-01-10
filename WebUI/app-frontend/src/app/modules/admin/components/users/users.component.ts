import { Component, OnInit } from '@angular/core';
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
  sortOrder: string = "";
  sortColumn: string = "";
  cols = [
    { field: 'name', header: 'name' },
    { field: 'email', header: 'email' },
    { field: 'role', header: 'role' },
  ];
  users: User[];
  roles: Role[];

  selectedRoles: Role[];

  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private roleService: RoleService,
    private userService: UserService,
    private translationProvider: TranslationProvider,
    private confirmationService: ConfirmationService,
    private tokenStorageProvider : TokenStorageProvider,
    private toastr : ToastrService
  ) { }

  ngOnInit(): void {
    this.spinner.show();
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
      isActive: null,
      queryString: null
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

}
