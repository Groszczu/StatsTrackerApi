namespace RiotApiClient.Models
{
    public class MatchList
    {
        public MatchOverview[]? Matches { get; set; }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int TotalGames { get; set; }
    }
}