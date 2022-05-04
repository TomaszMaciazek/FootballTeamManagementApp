import { SimpleUser } from "../user/simple-user.model";

export class UserSurveyResultListItem {
    public id : string;
    public surveyId: string;
    public title : string;
    public author : SimpleUser;
    public isCompleated : boolean;
}
