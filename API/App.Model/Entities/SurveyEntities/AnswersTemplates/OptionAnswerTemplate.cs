using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersTemplates
{
    public class OptionAnswerTemplate : BaseQuestionAswerTemplate
    {
        public OptionsQuestionTemplate Question { get; set; }
        public int Value { get; set; }
    }
}
