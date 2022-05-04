import { SimpleUser } from "../user/simple-user.model";

export class SimpleUserTestResultListItem {
    public id : string;
    public testId : string;
    public respondent : SimpleUser;
    public isCompleated : boolean;
    public passed : boolean;
    public scoredPoints : number;
}
