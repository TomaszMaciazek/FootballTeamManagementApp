export class ChangeMessageStatus {
    public userId : string;
    public isRead : boolean;
    public messagesIds : Array<string>;

    constructor(data: {
        userId : string,
        isRead : boolean,
        messagesIds : Array<string>
    }){
        Object.assign(this, data);
    }
}
