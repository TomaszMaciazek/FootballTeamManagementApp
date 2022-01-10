using App.Model.Dtos.Common;
using App.Model.Enums;
using System;

namespace App.Model.Dtos
{
    public class MatchPointDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public Guid MatchId { get; set; }
        public Guid PlayerId { get; set; }
        public MatchPointType Point { get; set; }
        public int MinuteOfMatch { get; set; }
    }
}
