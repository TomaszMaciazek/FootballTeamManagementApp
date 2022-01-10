import { QuestionType } from "src/app/enums/question-type";
import { CreateTestQuestionOption } from "./create-test-question-option.model";

export class CreateTestQuestion {
    public number : number;
    public type : QuestionType;
    public description : string;
    public options : Array<CreateTestQuestionOption>;
}
