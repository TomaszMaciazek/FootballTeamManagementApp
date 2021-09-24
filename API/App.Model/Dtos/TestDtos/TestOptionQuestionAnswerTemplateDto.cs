using App.Model.Dtos.Common;

namespace App.Model.Dtos.TestDtos
{
    public class TestOptionQuestionAnswerTemplateDto : AuditableEntityDto
    {
        public OptionsTestQuestionTemplateDto Question { get; set; }
        public int Value { get; set; }
        public string Content { get; set; }
        public int QuestionNumber { get; set; }
        public bool IsCorrect { get; set; }
        public double PointsForAnswer { get; set; }
    }
}
