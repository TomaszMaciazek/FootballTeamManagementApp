namespace App.Model.Dtos
{
    public class SelectUserPlayerDetailsDto
    {
        public int YearOfBirth { get; set; }
        public string Country { get; set; }
        public SimpleTeamDto Team { get; set; }
    }
}
