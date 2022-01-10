import { MailboxType } from "src/app/enums/mailbox-type";

export class MessageQuery {
    public pageNumber : number;
    public pageSize : number;
    public orderByColumnName : string;
    public orderByDirection : string;
    public userId : string;
    public mailboxType : MailboxType;
    public searchText : string;
}
