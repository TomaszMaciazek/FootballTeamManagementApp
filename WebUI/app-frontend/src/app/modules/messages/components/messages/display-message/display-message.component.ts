import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { MailboxType } from 'src/app/enums/mailbox-type';
import { ChangeMessageStatus } from 'src/app/models/commands/change-message-status.model';
import { CreateMessage } from 'src/app/models/commands/create-message.model';
import { SelectMessagesCommand } from 'src/app/models/commands/select-messages.model';
import { Message } from 'src/app/models/messages/message.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'display-message',
  templateUrl: './display-message.component.html',
  styleUrls: ['./display-message.component.scss']
})
export class DisplayMessageComponent implements OnInit {

  @Input() message: Message;

  @Output() displayingMessage = new EventEmitter<boolean>();

  @Output() reply = new EventEmitter<CreateMessage>();

  dateTimePipe: DatePipe = new DatePipe(this.translationProvider.getCurrentLanguageCode());

  mailboxes: MailboxType;

  boxType = MailboxType;

  constructor(
    private router: Router,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private tokenStorageProvider: TokenStorageProvider,
    private translationProvider: TranslationProvider,
    private messageService: MessageService
    ) { }

  ngOnInit(): void {
    this.spinner.show();
      let command = new ChangeMessageStatus({
      userId: this.tokenStorageProvider.getUserId(),
      messagesIds: [this.message.id],
      isRead: true
    });
    forkJoin([
      this.messageService.changeMessageStatus(command),
      this.messageService.getNumberOfUnreadMessages(this.tokenStorageProvider.getUserId())
      .then(res => this.messageService.numberOfUnreadMessages.emit(res))
    ])
    .subscribe(res => this.spinner.hide());
  }

  replyToMessage() {
    let message = new CreateMessage();
    message.recipientsIds = [this.message.sender.id];
    message.topic = "Re: " + this.message.topic;
    message.content = '';
    this.reply.emit(message);
  }

  deleteMessage() {
    let command = new SelectMessagesCommand();
    this.spinner.show();
    command.userId = this.tokenStorageProvider.getUserId();
    command.messagesIds = [this.message.id];
    this.messageService.deleteMessages(command)
      .then((result) => {
        this.displayingMessage.emit(false);
        this.spinner.hide();
      })
  }

  goBackToMailbox() {
    this.displayingMessage.emit(false);
  }

}
