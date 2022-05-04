import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CreateMessage } from 'src/app/models/commands/create-message.model';
import { SimpleUser } from 'src/app/models/user/simple-user.model';
import { TokenStorageProvider } from 'src/app/providers/token-storage-provider.model';
import { TranslationProvider } from 'src/app/providers/translation-provider.model';
import { MessageService } from 'src/app/services/message.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'create-message',
  templateUrl: './create-message.component.html',
  styleUrls: ['./create-message.component.scss']
})
export class CreateMessageComponent implements OnInit {

  @Input() reply: CreateMessage;

  @Output() creatingMessage: EventEmitter<boolean> = new EventEmitter<boolean>();

  selectedRecipients : SimpleUser[];

  newMessage: CreateMessage = new CreateMessage();

  recipients: SimpleUser[];

  displayAlert = false;

  contactedUserId: string;

  constructor(
    private userService: UserService,
    private messageService: MessageService,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService,
    private tokenStorageProvider: TokenStorageProvider,
    private translationProvider: TranslationProvider
    ) { }

  ngOnInit(): void {

    this.newMessage.content = '';
    this.userService.getRecipients(this.tokenStorageProvider.getUserId())
      .then(res => {
        this.recipients = res;
        if (this.reply) {
          this.selectedRecipients = [];
          this.newMessage.topic = this.reply.topic;
          this.newMessage.content = this.reply.content;
          this.reply.recipientsIds.forEach(element => {
            let recipient = this.recipients.find(x => x.id == element);
            this.selectedRecipients.push(recipient);
          })
        }
        if (this.contactedUserId) {
          this.selectedRecipients = [];
          let recipient = this.recipients.find(x => x.id == this.contactedUserId);
          this.selectedRecipients.push(recipient);
        }
      })
  }

  closeAlert() {
    this.displayAlert = false;
  }

  sendMessage() {
    if (!this.newMessage.content || !this.newMessage.topic || this.selectedRecipients.length < 1) {
      this.displayAlert = true;
    }
    else {
      this.newMessage.senderId = this.tokenStorageProvider.getUserId();
      this.newMessage.recipientsIds = [];
      this.selectedRecipients.forEach(x => {
        this.newMessage.recipientsIds.push(x.id);
      });
      this.messageService.sendMessage(this.newMessage)
        .then(res => {
          this.creatingMessage.emit(false);
        })
    }
  }

}
