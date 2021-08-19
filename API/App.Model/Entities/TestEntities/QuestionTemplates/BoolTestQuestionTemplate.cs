using App.Model.Entities.TestEntities.AnswersResults;
using System.Collections.Generic;

namespace App.Model.Entities.TestEntities.QuestionTemplates
{
    public class BoolTestQuestionTemplate : BaseTestQuestionTemplate
    {
        public bool CorrectAnswer { get; set; }
        public ICollection<UserBoolTestQuestionAnswer> UsersAnswers { get; set; }
    }
}
