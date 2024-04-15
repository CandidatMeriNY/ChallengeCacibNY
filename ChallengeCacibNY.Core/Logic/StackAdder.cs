using ChallengeCacibNY.Core.Models;

namespace ChallengeCacibNY.Core.Logic
{
    public class StackAdder : IStackAdder
    {
        ICalculator _calculator;

        public StackAdder(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public StackValue Add(StackValue stackValue, string newItem)
        {
            stackValue.HistoricalItems.Add(newItem);
            stackValue.Results.Add(newItem);
            stackValue.Results = _calculator.Calculate(new Queue<string>(stackValue.Results)).ToList();
            return stackValue;
        }
    }
}
