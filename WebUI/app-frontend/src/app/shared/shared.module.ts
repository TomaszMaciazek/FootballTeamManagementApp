import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { ToastrModule } from 'ngx-toastr';
import {PanelModule} from 'primeng/panel';
import { FlexLayoutModule } from '@angular/flex-layout';
import {PaginatorModule} from 'primeng/paginator';
import {CardModule} from 'primeng/card';
import {ToolbarModule} from 'primeng/toolbar';
import {InputTextModule} from 'primeng/inputtext';
import {ReactiveFormsModule} from '@angular/forms';
import {InputSwitchModule} from 'primeng/inputswitch';
import {CalendarModule} from 'primeng/calendar';
import {TableModule} from 'primeng/table';
import {MultiSelectModule} from 'primeng/multiselect';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TranslateModule,
    DropdownModule,
    ButtonModule,
    ToastrModule,
    PanelModule,
    FlexLayoutModule,
    PaginatorModule,
    CardModule,
    ToolbarModule,
    InputTextModule,
    InputSwitchModule,
    CalendarModule,
    TableModule,
    MultiSelectModule
  ],
  exports: [
    ReactiveFormsModule,
    TranslateModule,
    DropdownModule,
    ButtonModule,
    ToastrModule,
    PanelModule,
    FlexLayoutModule,
    PaginatorModule,
    CardModule,
    ToolbarModule,
    InputTextModule,
    InputSwitchModule,
    CalendarModule,
    TableModule,
    MultiSelectModule
  ]
})
export class SharedModule { }
