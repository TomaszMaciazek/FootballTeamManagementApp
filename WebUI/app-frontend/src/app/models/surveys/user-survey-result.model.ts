import { SimpleSurvey } from "./simple-survey.model";
import { SurveySelectQuestionAnswer } from "./survey-select-question-answer.model";
import { SurveyTextQuestionAnswer } from "./survey-text-question-answer.model";

export class UserSurveyResult {
    public id : string;
    public survey : SimpleSurvey;
    public isCompleated : boolean;
    public textQuestionAnswers : Array<SurveyTextQuestionAnswer>;
    public selectQuestionAnswers : Array<SurveySelectQuestionAnswer>;
}
