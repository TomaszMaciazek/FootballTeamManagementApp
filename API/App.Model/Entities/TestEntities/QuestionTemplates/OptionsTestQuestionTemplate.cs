using App.Model.Entities.TestEntities.AnswersResults;
using App.Model.Entities.TestEntities.AnswersTemplates;
using App.Model.Enums.SurveyEnums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.TestEntities.QuestionTemplates
{
    public class OptionsTestQuestionTemplate : BaseTestQuestionTemplate
    {
        [Required]
        public OptionQuestionType Type { get; set; }
        public ICollection<TestOptionQuestionAnswerTemplate> Answers { get; set; }
        public ICollection<UserOptionsTestQuestionAnswer> UserAnswers { get; set; }
    }
}
