using App.Model.Entities.TestEntities.AnswersTemplates;
using App.Model.Enums.SurveyEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Entities.TestEntities.QuestionTemplates
{
    public class OptionsTestQuestionTemplate : BaseTestQuestionTemplate
    {
        public OptionQuestionType Type { get; set; }
        public ICollection<TestOptionQuestionAnswerTemplate> Answers { get; set; }
    }
}
