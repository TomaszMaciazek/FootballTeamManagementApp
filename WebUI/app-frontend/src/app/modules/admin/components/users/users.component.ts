import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { LazyLoadEvent } from 'primeng/api';
import { UserQuery } from 'src/app/models/queries/user-query.model';
import { Role } from 'src/app/models/role.model';
import { User } from 'src/app/models/user.model';
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
    private userService: UserService
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
      isActive: true,
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

}
