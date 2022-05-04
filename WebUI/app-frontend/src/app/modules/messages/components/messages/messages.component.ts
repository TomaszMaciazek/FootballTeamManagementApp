import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MailboxType } from 'src/app/enums/mailbox-type';
import { CreateMessage } from 'src/app/models/commands/create-message.model';
import { Message } from 'src/app/models/messages/message.model';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit {

  mailboxTypes = [MailboxType.Inbox, MailboxType.Sent, MailboxType.Trash];

  currentMailbox = MailboxType.Inbox;

  creatingMessage = false;
  
  displayedMessage: Message = null;

  displayingMessage = false;

  reply: CreateMessage = null;

  boxType = MailboxType;

  boxTypeLabel = new Map<number, string>([
    [MailboxType.Inbox, 'inbox'],
    [MailboxType.Sent, 'sent'],
    [MailboxType.Trash, 'trash']
  ]);

  constructor(
    ) { }

  ngOnInit(): void {
    this.creatingMessage = true;
  }

  getMailboxIcon(mailbox: MailboxType) {
    switch(mailbox) {
      case MailboxType.Inbox:
        return "fas fa-envelope";
      case MailboxType.Sent:
        return "fas fa-paper-plane";
      case MailboxType.Trash:
        return "fas fa-trash";
    }
  }

  getButtonColor(mailbox: MailboxType) {
    return this.currentMailbox === mailbox ? 'primary' : '';
  }

  changeMailbox(mailbox: MailboxType) {
    this.displayingMessage = false;
    this.creatingMessage = false;
    this.reply = null;
    switch(mailbox) {
      case MailboxType.Inbox:
        this.currentMailbox = MailboxType.Inbox;
        this.creatingMessage = false;
        break;
      case MailboxType.Sent:
        this.currentMailbox = MailboxType.Sent;
        this.creatingMessage = false;
        break;
      case MailboxType.Trash:
        this.currentMailbox = MailboxType.Trash;
        this.creatingMessage = false;
        break;
    }
  }

  createMessage() {
    this.displayingMessage = false;
    this.creatingMessage = true;
  }

  goBackToMailbox(event: boolean) {
    this.creatingMessage = event;
    this.currentMailbox = MailboxType.Sent;
  }

  displayMessage(event: Message) {
    this.displayedMessage = event;
    this.displayingMessage = true;
  }

  stopDisplayingMessage() {
    this.displayingMessage = false;
  }

  createReply(command: CreateMessage) {
    this.reply = command;
    this.displayingMessage = false;
    this.creatingMessage = true;
  }

}
