using App.Model.Entities.Common;
using App.Model.Entities.SurveyEntities.QuestionTemplates;

namespace App.Model.Entities.SurveyEntities.AnswersResults
{
    public class UserOptionsSurveyQuestionAnswer : BaseUserAnswer
    {
        public OptionsSurveyQuestionTemplate Question { get; set; }
        public int? Value { get; set; }
    }
}
