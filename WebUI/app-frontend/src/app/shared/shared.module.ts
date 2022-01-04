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
import {SpeedDialModule} from 'primeng/speeddial';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {DialogModule} from 'primeng/dialog';
import {InputNumberModule} from 'primeng/inputnumber';
import {AccordionModule} from 'primeng/accordion';
import {FieldsetModule} from 'primeng/fieldset';
import {ChipsModule} from 'primeng/chips';
import {SelectButtonModule} from 'primeng/selectbutton';
import {StepsModule} from 'primeng/steps';
import {TimelineModule} from 'primeng/timeline';
import {ChartModule} from 'primeng/chart';
import {TooltipModule} from 'primeng/tooltip';
import {RatingModule} from 'primeng/rating';



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
    MultiSelectModule,
    SpeedDialModule,
    ConfirmDialogModule,
    DialogModule,
    InputNumberModule,
    AccordionModule,
    FieldsetModule,
    ChipsModule,
    SelectButtonModule,
    StepsModule,
    TimelineModule,
    ChartModule,
    TooltipModule,
    RatingModule
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
    MultiSelectModule,
    SpeedDialModule,
    ConfirmDialogModule,
    DialogModule,
    InputNumberModule,
    AccordionModule,
    FieldsetModule,
    ChipsModule,
    SelectButtonModule,
    StepsModule,
    TimelineModule,
    ChartModule,
    TooltipModule,
    RatingModule
  ]
})
export class SharedModule { }
