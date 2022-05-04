import { SimpleTest } from "./simple-test.model";
import { TestQuestionAnswer } from "./test-question-answer.model";

export class UserTestResult {
    public id : string;
    public test : SimpleTest
    public isCompleated : boolean;
    public passed : boolean;
    public scoredPoints : number;
    public questionAnswers : Array<TestQuestionAnswer>
}
