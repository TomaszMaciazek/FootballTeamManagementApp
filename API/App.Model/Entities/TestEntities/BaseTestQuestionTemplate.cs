namespace App.Model.Entities.TestEntities
{
    public abstract class BaseTestQuestionTemplate : BaseQuestionTemplate
    {
        public TestTemplate Test { get; set; }
        public decimal PointsToEarn { get; set; }
    }
}
