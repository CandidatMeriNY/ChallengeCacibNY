namespace ChallengeCacibNY.Core.Logic
{
    public interface IItemChecker
    {
        bool IsOperator(string item);
        bool IsNumber(string item);
    }
}
