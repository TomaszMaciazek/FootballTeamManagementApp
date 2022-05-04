import { SurveyQuestion } from "./survey-question.model";

export class SurveyFillTemplate {
    public id : string;
    public questions : Array<SurveyQuestion>;
    public title : string;
    public description : string;
}
