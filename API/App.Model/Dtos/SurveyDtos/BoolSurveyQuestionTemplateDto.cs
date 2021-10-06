﻿using App.Model.Dtos.Common;

namespace App.Model.Dtos.SurveyDtos
{
    public class BoolSurveyQuestionTemplateDto : AuditableEntityDto
    {
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public int QuestionNumber { get; set; }
        public SurveyTemplateDto Survey { get; set; }
        public bool IsRequired { get; set; }
    }
}