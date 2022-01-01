using App.Model.Entities.Common;

namespace App.Model.Entities.SurveyEntities
{
    public class SurveyTextQuestionAnswer : EditableEntity
    {
        public SurveyQuestion Question { get; set; }
        public UserSurveyResult UserResult { get; set; }
        public string AnswerValue { get; set; }
    }
}
