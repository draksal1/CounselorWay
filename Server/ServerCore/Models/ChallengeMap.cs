namespace ServerCore.Models
{
    public class ChallengeMap
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Guid CampSeasonId { get; set; }
        public int WeekNumber { get; set; }
        public string Name { get; set; }
        public List<Challenge> Challenges { get; set; } = new();

        public ChallengeMap(Guid campSeasonId, int weekNumber, string name)
        {
            CampSeasonId = campSeasonId;
            WeekNumber = weekNumber;
            Name = name;
        }
    }
}