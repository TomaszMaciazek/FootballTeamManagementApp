using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Queries
{
    public class PlayerQuery : UserQuery
    {
        public int? CountryId { get; set; }
        public Gender? Gender { get; set; }
        public Guid? TeamId { get; set; }
        public bool? IsStillPlaying { get; set; }
    }
}
