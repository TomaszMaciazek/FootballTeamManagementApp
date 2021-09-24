using App.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Dtos
{
    public class MatchPlayerDto
    {
        public MatchDto Match { get; set; }
        public PlayerDto Player { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
    }
}
