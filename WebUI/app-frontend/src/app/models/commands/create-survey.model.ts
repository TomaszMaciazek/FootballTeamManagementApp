import { CreateSurveyQuestion } from "./create-survey-question.model";

export class CreateSurveyCommand {
    public authorId : string;
    public title : string;
    public description : string;
    public isAnonymous : boolean;
    public questions : Array<CreateSurveyQuestion>;
    public respondentsIds : Array<string>;
}
