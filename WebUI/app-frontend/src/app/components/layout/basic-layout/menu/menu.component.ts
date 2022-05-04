import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MenuItem } from 'src/app/models/menu-item.model';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent{

  @Input() menuItems : MenuItem[];
  @Output() goToEvent = new EventEmitter<string>();

  goTo(link: string) {
    this.goToEvent.emit(link)
  }
}
