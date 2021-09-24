using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model.Dtos
{
    public class MatchDetailsDto : MatchDto
    {
        public ICollection<PlayerCardDto> PlayersCards { get; set; }
        public ICollection<CoachCardDto> CoachesCards { get; set; }
        public ICollection<CoachDto> Coaches { get; set; }
        public ICollection<MatchPointDto> MatchPoints { get; set; }
    }
}
