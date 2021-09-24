﻿using App.Model.Dtos.Common;
using App.Model.Enums;

namespace App.Model.Dtos
{
    public class PlayerCardDto : AuditableEntityDto
    {
        public PlayerDto Player { get; set; }
        public MatchDto Match { get; set; }
        public CardColor Color { get; set; }
    }
}
