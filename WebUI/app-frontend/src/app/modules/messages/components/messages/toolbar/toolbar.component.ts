import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MailboxType } from 'src/app/enums/mailbox-type';
import { ChangeMessageStatus } from 'src/app/models/commands/change-message-status.model';
import { SelectMessagesCommand } from 'src/app/models/commands/select-messages.model';
import { Message } from 'src/app/models/messages/message.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'messages-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {

  @Input() mailboxType: MailboxType;

  @Input() selectedMessages: Message[];

  @Output() updatedMessages = new EventEmitter<boolean>();

  @Output() searchMessages = new EventEmitter<string>();

  searchText: string;

  boxType = MailboxType;

  constructor(
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private tokenStorageProvider: TokenStorageProvider,
    private translationProvider: TranslationProvider,
    private messageService: MessageService
    ) { }

  search() {
    this.searchMessages.emit(this.searchText);
  }

  deleteMessages() {
    this.spinner.show();
    let command = new SelectMessagesCommand();
    command.userId = this.tokenStorageProvider.getUserId();
    command.messagesIds = this.selectedMessages.map(x => x.id);
    this.messageService.deleteMessages(command)
      .then(res => {
        this.spinner.hide();
        this.updatedMessages.emit(true);
      });
  }

  takeBackMessages() {
    this.spinner.show();
    let command = new SelectMessagesCommand();
    command.userId = this.tokenStorageProvider.getUserId();
    command.messagesIds = this.selectedMessages.map(x => x.id);
    this.messageService.takeMessagesFromTrash(command)
      .then(res => {
        this.spinner.hide();
        this.updatedMessages.emit(true);
      });
  }

  markMessagesAsRead() {
    this.spinner.show();
    let command = new ChangeMessageStatus({
    userId: this.tokenStorageProvider.getUserId(),
    messagesIds: this.selectedMessages.map(x => x.id),
    isRead: true
    })
    this.messageService.changeMessageStatus(command)
      .then(res => {
        this.updatedMessages.emit(true);
      });
  }

}
