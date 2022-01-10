import { TestQuestionAnswerCommand } from "./test-question-answer.model";

export class FillTestCommand {
    public respondentId : string;
    public testId : string;
    public questionAnswers : Array<TestQuestionAnswerCommand>;
}
