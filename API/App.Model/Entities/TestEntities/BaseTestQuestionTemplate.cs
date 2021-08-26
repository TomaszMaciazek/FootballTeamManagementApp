using App.Model.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities.TestEntities
{
    public abstract class BaseTestQuestionTemplate : BaseQuestionTemplate
    {
        public TestTemplate Test { get; set; }

        [Required]
        public double PointsToEarn { get; set; }
    }
}
