import { SimpleUser } from "../user/simple-user.model";

export class UserTestResultListItem {
    public id : string;
    public testId: string;
    public title : string;
    public author : SimpleUser;
    public isCompleated : boolean;
}
