using App.Model.Entities.Common;
using App.Model.Entities.SurveyEntities;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public abstract class BaseSurveyQuestionTemplate : BaseQuestionTemplate
    {
        public SurveyTemplate Survey { get; set; }

        [Required]
        public bool IsRequired { get; set; }
    }
}
