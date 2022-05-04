using App.Model.Entities.Common;

namespace App.Model.Entities.SurveyEntities
{
    public class SurveySelectQuestionAnswer : EditableEntity
    {
        public SurveyQuestion Question { get; set; }
        public UserSurveyResult UserResult { get; set; }
        public int? AnswerValue { get; set; }
    }
}
