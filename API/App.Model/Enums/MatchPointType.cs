namespace App.Model.Enums
{
    public enum MatchPointType
    {
        /// <summary>
        /// Zdobyty w normalnym przebiegu rozgrywki
        /// </summary>
        InGame = 0,
        
        /// <summary>
        /// Zdobyty z rzuty karnego
        /// </summary>
        Penalty = 1,
        
        /// <summary>
        /// Zdobyty z rzutu wolnego
        /// </summary>
        FreeKick = 2,

        /// <summary>
        /// Zdobyry z rzutu rożnego
        /// </summary>
        CornerKick = 3,

        /// <summary>
        /// Gol samobójczy
        /// </summary>
        Own = 4
    }
}
