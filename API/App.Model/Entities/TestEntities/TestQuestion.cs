using App.Model.Entities.Common;
using App.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Entities.TestEntities
{
    public class TestQuestion : EditableEntity
    {
        public int Number { get; set; }
        public QuestionType Type { get; set; }
        public string Description { get; set; }
        public TestTemplate Test { get; set; }
        public ICollection<TestQuestionOption> Options { get; set; }
        public ICollection<TestQuestionAnswer> QuestionAnswers { get; set; }
    }
}
