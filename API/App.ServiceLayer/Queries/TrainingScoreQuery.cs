using App.Model.Enums;
using System;
using System.Collections.Generic;

namespace App.ServiceLayer.Queries
{
    public class TrainingScoreQuery : PaginationQuery
    {
        public Guid TrainingId { get; set; }
        public IEnumerable<TrainingScoreType> ScoreTypes { get; set; }
    }
}
