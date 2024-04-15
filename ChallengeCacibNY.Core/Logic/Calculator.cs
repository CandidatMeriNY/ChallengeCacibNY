namespace ChallengeCacibNY.Core.Logic
{
    public class Calculator : ICalculator
    {
        private readonly IItemChecker _itemChecker;

        public Calculator(IItemChecker itemChecker)
        {
            _itemChecker = itemChecker;
        }

        public double Calculate(double v1, double v2, string op)
        {
            if (op == Constants.Operators[0])
                return v1 + v2;

            if (op == Constants.Operators[1])
                return v1 - v2;

            if (op == Constants.Operators[2])
                return v1 * v2;

            if (op == Constants.Operators[3])
                return v1 / v2;

            return 0;
        }

        public IEnumerable<string> Calculate(Queue<string> itemQueue)
        {
            var stack = new Stack<double>();

            while (itemQueue.Any())
            {
                var symbol = itemQueue.Dequeue().Trim();

                if (_itemChecker.IsNumber(symbol))
                {
                    stack.Push(double.Parse(symbol));
                    continue;
                }

                if (_itemChecker.IsOperator(symbol))
                {
                    var val1 = stack.Pop();
                    var val2 = stack.Pop();
                    var output = Calculate(val2, val1, symbol);
                    stack.Push(output);
                    continue;
                }
            }

            return stack.Reverse().Select(s => s.ToString()).ToList();
        }
    }
}
