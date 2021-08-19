using App.Model.Entities.TestEntities.QuestionTemplates;
using System.Collections.Generic;

namespace App.Model.Entities.TestEntities
{
    public class TestTemplate : BaseQuestionSetEntity
    {
        public ICollection<BoolTestQuestionTemplate> BoolTestQuestions { get; set; }
        public ICollection<OptionsTestQuestionTemplate> OptionsTestQuestions { get; set; }
        public ICollection<UserTestResult> UserResults { get; set; }
    }
}
