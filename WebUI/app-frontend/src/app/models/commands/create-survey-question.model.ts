import { QuestionType } from "src/app/enums/question-type";
import { CreateSurveyQuestionOption } from "./create-survey-question-option.model";

export class CreateSurveyQuestion {
    public number : number;
    public type : QuestionType;
    public description : string;
    public isRequired : boolean;
    public options : Array<CreateSurveyQuestionOption>;
}
