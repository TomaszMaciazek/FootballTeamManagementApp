using App.Model.Dtos.Common;

namespace App.Model.Dtos.TestDtos
{
    public class BoolTestQuestionTemplateDto : AuditableEntityDto
    {
        public TestTemplateDto Test { get; set; }
        public bool CorrectAnswer { get; set; }
        public double PointsToEarn { get; set; }
    }
}
