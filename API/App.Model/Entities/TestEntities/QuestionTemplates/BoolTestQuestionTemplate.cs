using App.Model.Entities.TestEntities.AnswersResults;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.TestEntities.QuestionTemplates
{
    public class BoolTestQuestionTemplate : BaseTestQuestionTemplate
    {
        [Required]
        public bool CorrectAnswer { get; set; }
        public ICollection<UserBoolTestQuestionAnswer> UsersAnswers { get; set; }
    }
}
