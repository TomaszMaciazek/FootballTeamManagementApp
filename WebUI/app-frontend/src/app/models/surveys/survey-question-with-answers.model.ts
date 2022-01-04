import { QuestionType } from "src/app/enums/question-type";
import { SurveyQuestionOption } from "./survey-question-option.model";
import { SurveySelectQuestionAnswer } from "./survey-select-question-answer.model";
import { SurveyTextQuestionAnswer } from "./survey-text-question-answer.model";

export class SurveyQuestionWithAnswers {
    public id: string;
    public number : number;
    public type : QuestionType;
    public description : string;
    public options : Array<SurveyQuestionOption>;
    public textQuestionAnswers : Array<SurveyTextQuestionAnswer>;
    public selectQuestionAnswers : Array<SurveySelectQuestionAnswer>;
}
