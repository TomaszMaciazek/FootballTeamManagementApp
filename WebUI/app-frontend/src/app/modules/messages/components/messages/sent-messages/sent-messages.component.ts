import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LazyLoadEvent } from 'primeng/api';
import { MailboxType } from 'src/app/enums/mailbox-type';
import { Message } from 'src/app/models/messages/message.model';
import { MessageQuery } from 'src/app/models/queries/message-query.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'sent-messages',
  templateUrl: './sent-messages.component.html',
  styleUrls: ['./sent-messages.component.scss']
})
export class SentMessagesComponent implements OnInit {

  @Output() displayingMessage: EventEmitter<Message> = new EventEmitter<Message>();

  mailboxType = MailboxType.Sent;

  messages: Message[];

  selectedMessages: Message[];


  pageNumber : number = 1;
  rowNumbers : number = 20;
  rowNumbersOptions = [20, 30, 50];
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  sortOrder: string = "desc";
  sortColumn: string = "Date";

  searchText = '';
  
  constructor(
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private tokenStorageProvider: TokenStorageProvider,
    private translationProvider: TranslationProvider,
    private messageService: MessageService
    ) { }

  ngOnInit(): void {
    this.getMessages();
  }

  getMessages() {
    this.spinner.show();
    let query = new MessageQuery();
    query.searchText = this.searchText;
    query.pageNumber= this.pageNumber;
    query.pageSize= this.rowNumbers;
    query.orderByColumnName= this.sortColumn;
    query.orderByDirection = this.sortOrder;
    query.mailboxType = MailboxType.Sent;
    query.userId = this.tokenStorageProvider.getUserId();
    this.messageService.getMessages(query)
    .then(res => {
      this.messages = res.items;
      this.totalCount = res.totalCount;
      this.spinner.hide();
    });
  }

  displayMessage(message: Message) {
    this.displayingMessage.emit(message);
  }

  updateMessages(e: boolean) {
    if(e) {
      this.pageNumber = 1;
      this.getMessages();
    }
  }

  search(e: string) {
    this.pageNumber = 1;
    this.searchText = e;
    this.getMessages();
  }

  loadMessages(event: LazyLoadEvent) {
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
    this.getMessages();
  }

}
