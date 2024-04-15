namespace ChallengeCacibNY.Core.Models
{
    public class StackValue : IValue<int>
    {
        public int Id { get; set; }
        public List<string> HistoricalItems { get; set; } = new List<string>();
        public List<string> Results { get; set; } = new List<string>();
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
