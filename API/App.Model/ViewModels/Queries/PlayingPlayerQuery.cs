using App.Model.Enums;
using System;

namespace App.Model.ViewModels.Queries
{
    public class PlayingPlayerQuery
    {
        public PlayersGender? PlayersGender { get; set; }
        public DateTime? Date { get; set; }
    }
}
