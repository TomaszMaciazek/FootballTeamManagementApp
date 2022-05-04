import { QuestionType } from "src/app/enums/question-type";
import { TestQuestionOption } from "./test-question-option.model";

export class TestQuestion {
    public id : string;
    public number : number;
    public type : QuestionType;
    public description : string;
    public options : Array<TestQuestionOption>
}
