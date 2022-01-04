import { SurveyQuestion } from "./survey-question.model";
import { Survey } from "./survey.model";

export class SurveyTextQuestionAnswer {
    public id : Survey;
    public questionId : string;
    public userId : string;
    public answerValue : string;
}
