using System;

namespace App.Model.ViewModels.Commands
{
    public class CreateTrainingScoreVM
    {
        public Guid PlayerId { get; set; }
        public Guid TrainingId { get; set; }
        /// <summary>
        /// Kontrola piłki
        /// </summary>
        public int? BallControl { get; set; }
        /// <summary>
        /// Skuteczność podań lewą nogą
        /// </summary>
        public int? LeftFootPassAccuracy { get; set; }
        /// <summary>
        /// Skuteczność podań prawą nogą
        /// </summary>
        public int? RightFootPassAccuracy { get; set; }
        /// <summary>
        /// Skuteczność prowadzenia piłki lewą nogą
        /// </summary>
        public int? LeftFootDribblingAccuracy { get; set; }
        /// <summary>
        /// Skuteczność prowadzenia piłki prawą nogą
        /// </summary>
        public int? RightFootDribblingAccuracy { get; set; }
        /// <summary>
        /// Skuteczność przyjęcia piłki lewą nogą
        /// </summary>
        public int? LeftFootBallReceivingAccuracy { get; set; }
        /// <summary>
        /// Skuteczność przyjęcia piłki prawą nogą
        /// </summary>
        public int? RightFootBallReceivingAccuracy { get; set; }
        /// <summary>
        /// Skuteczność strzałów lewą nogą
        /// </summary>
        public int? LeftFootShotsAccuracy { get; set; }
        /// <summary>
        /// Skuteczność strzałów prawą nogą
        /// </summary>
        public int? RightFootShotsAccuracy { get; set; }
        /// <summary>
        /// Skuteczność zagrań głową
        /// </summary>
        public int? HeadingAccuracy { get; set; }
        /// <summary>
        /// Skuteczność gry 1 vs 1
        /// </summary>
        public int? OneVsOneScore { get; set; }
        /// <summary>
        /// Mobilność
        /// </summary>
        public int? Mobility { get; set; }
        /// <summary>
        /// Siła
        /// </summary>
        public int? Strength { get; set; }
        /// <summary>
        /// Wytrzymałość
        /// </summary>
        public int? Endurance { get; set; }
        /// <summary>
        /// Zwinność
        /// </summary>
        public int? Agility { get; set; }
        /// <summary>
        /// Koordynacja
        /// </summary>
        public int? Coordination { get; set; }
        /// <summary>
        /// Koncentracja na zadaniach
        /// </summary>
        public int? Concentration { get; set; }
        /// <summary>
        /// Kontrola emocji
        /// </summary>
        public int? EmotionsControl { get; set; }
        /// <summary>
        /// Kontrola emocji
        /// </summary>
        public int? Selfconfidence { get; set; }
        /// <summary>
        /// Radzenie sobie ze stresem
        /// </summary>
        public int? StressControl { get; set; }
        /// <summary>
        /// Nastawienie
        /// </summary>
        public int? Attitude { get; set; }
        /// <summary>
        /// Komunikacja
        /// </summary>
        public int? Communication { get; set; }
        /// <summary>
        /// Współpraca
        /// </summary>
        public int? Cooperation { get; set; }
        /// <summary>
        /// Determinacja
        /// </summary>
        public int? Determination { get; set; }
        /// <summary>
        /// Dyscyplina
        /// </summary>
        public int? Discipline { get; set; }
        /// <summary>
        /// Zaangażowanie
        /// </summary>
        public int? Engagement { get; set; }
        /// <summary>
        /// Kreatywność
        /// </summary>
        public int? Creativity { get; set; }
        /// <summary>
        /// Decyzyjność
        /// </summary>
        public int? Decisiveness { get; set; }
        /// <summary>
        /// Świadomość
        /// </summary>
        public int? Awareness { get; set; }
    }
}
