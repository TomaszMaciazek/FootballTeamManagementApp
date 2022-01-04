import { SimpleUser } from "../user/simple-user.model";

export class SurveyListItem {
    public id : string;
    public title : string;
    public author : SimpleUser;
    public respondentsCount : number;
    public numberOfCompleatedResults : number;
}
