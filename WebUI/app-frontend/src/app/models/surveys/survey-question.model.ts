import { QuestionType } from "src/app/enums/question-type";
import { SurveyQuestionOption } from "./survey-question-option.model";

export class SurveyQuestion {
    public id: string;
    public number : number;
    public type : QuestionType;
    public description : string;
    public isRequired : boolean;
    public options : Array<SurveyQuestionOption>
}
