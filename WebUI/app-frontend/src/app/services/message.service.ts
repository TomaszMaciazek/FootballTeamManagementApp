import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { toParams } from '../app-helpers';
import { ChangeMessageStatus } from '../models/commands/change-message-status.model';
import { CreateMessage } from '../models/commands/create-message.model';
import { SelectMessagesCommand } from '../models/commands/select-messages.model';
import { UpdateMessage } from '../models/commands/update-message.model';
import { Message } from '../models/messages/message.model';
import { SimpleMessage } from '../models/messages/simple-message.model';
import { PaginatedList } from '../models/paginated-list.model';
import { MessageQuery } from '../models/queries/message-query.model';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private url = `${environment.apiUrl}/message`;

  public numberOfUnreadMessages: EventEmitter<number> = new EventEmitter<number>();

  constructor(
    private http: HttpClient
  ) { }

  getMessages(query: MessageQuery) {
    return this.http.get<PaginatedList<Message>>(`${this.url}`, {params: toParams(query)}).toPromise();
  }

  getById(id: string) : Promise<Message>{
    return this.http.get<Message>(`${this.url}/${id}`).toPromise();
  }

  sendMessage(command: CreateMessage) {
    return this.http.post(`${this.url}`, command).toPromise();
  }

  getNumberOfUnreadMessages(id: string) : Promise<number> {
    return this.http.get<number>(`${this.url}/unread/${id}`).toPromise();
  }

  changeMessageStatus(command: ChangeMessageStatus) {
    return this.http.patch(`${this.url}/status`, command).toPromise();
  }

  takeMessagesFromTrash(command: SelectMessagesCommand) {
    return this.http.put(`${this.url}/takeFromTrash`, command).toPromise();
  }

  deleteMessages(command: SelectMessagesCommand) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: command,
    };
    return this.http.delete(`${this.url}`, options).toPromise();
  }
  

}
