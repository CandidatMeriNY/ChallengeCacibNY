namespace ChallengeCacibNY.Core.Logic
{
    public interface ICalculator
    {
        double Calculate(double v1, double v2, string op);
        IEnumerable<string> Calculate(Queue<string> itemQueue);
    }
}
