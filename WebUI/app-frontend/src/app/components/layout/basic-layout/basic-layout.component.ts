import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { detectBody, goTop } from 'src/app/app-helpers';
import { MenuItem } from 'src/app/models/menu-item.model';
import { UserContextProvider } from 'src/app/providers/user-context-provider.model';
import { NavigationService } from 'src/app/services/navigation.service';

@Component({
  selector: 'app-basic-layout',
  templateUrl: './basic-layout.component.html',
  styleUrls: ['./basic-layout.component.scss']
})
export class BasicLayoutComponent implements OnInit {

  public menuItems : Array<MenuItem>;
  public isSidebarActive = false;

  constructor(
    private router: Router,
    private navigationService: NavigationService,
    private userContextProvider: UserContextProvider
    ) { }

  ngOnInit(): void {
    var menuItemsArray = this.navigationService.getMenuItems();
    menuItemsArray.forEach((x) => {
        if (x.SubMenuItems && x.SubMenuItems.length > 0) {
            x.SubMenuItems = x.SubMenuItems.filter((item: MenuItem) => {
                return !item.isEmpty();
            });
        }
    });
    this.menuItems = menuItemsArray;
  }

  goToDefault() {
    var url = this.userContextProvider.getDefaultPageUrl();
   this.goTo(url);
  }

  goTo(link: string) {
    this.router.navigate([link]);
    goTop();
  }

  onResize() {
    detectBody();
  }

  hideSidebar(){
    this.isSidebarActive = false; 
  }

  showSidebar(){
    this.isSidebarActive = true;
  }
}
