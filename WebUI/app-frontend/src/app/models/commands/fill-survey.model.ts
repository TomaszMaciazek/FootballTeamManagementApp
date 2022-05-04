import { SelectQuestionAnswer } from "./select-question-answer.model";
import { TextQuestionAnswer } from "./text-question-answer.model";

export class FillSurvey {
    public respondentId : string;
    public surveyId : string
    public textQuestionAnswers : Array<TextQuestionAnswer>
    public selectQuestionAnswers : Array<SelectQuestionAnswer>;
}
