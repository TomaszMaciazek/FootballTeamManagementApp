import { MailboxType } from "src/app/enums/mailbox-type";
import { SimpleUser } from "../user/simple-user.model";

export class Message {
    public id : string;
    public sender : SimpleUser;
    public recipients : Array<SimpleUser>;
    public topic : string;
    public content : string;
    public sendDate : Date;
    public mailboxType : MailboxType;
    public isRead : boolean;
}
