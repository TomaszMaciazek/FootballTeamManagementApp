using App.Model.Entities.Common;
using App.Model.Entities.SurveyEntities;

namespace App.Model.Entities
{
    public abstract class BaseSurveyQuestionTemplate : BaseQuestionTemplate
    {
        public SurveyTemplate Survey { get; set; }
        public bool IsRequired { get; set; }
    }
}
