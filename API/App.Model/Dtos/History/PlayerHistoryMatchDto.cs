using System;

namespace App.Model.Dtos.History
{
    public class PlayerHistoryMatchDto
    {
        public Guid Id { get; set; }
        public SimpleMatchDto Match { get; set; }
    }
}
