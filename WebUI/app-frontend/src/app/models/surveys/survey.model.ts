import { SurveyQuestionWithAnswers } from "./survey-question-with-answers.model";

export class Survey {
    public id : string;
    public questions : Array<SurveyQuestionWithAnswers>;
    public title : string;
    public description : string;
    public isAnonymous : boolean;
}
