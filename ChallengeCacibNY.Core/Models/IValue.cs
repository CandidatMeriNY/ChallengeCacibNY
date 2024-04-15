namespace ChallengeCacibNY.Core.Models
{
    public interface IValue<I>
    {
        I Id { get; }
        DateTime Created { get; }
        DateTime Updated { get; }
    }
}
