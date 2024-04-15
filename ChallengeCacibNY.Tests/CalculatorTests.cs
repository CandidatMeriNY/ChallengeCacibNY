using ChallengeCacibNY.Core.Logic;

namespace ChallengeCacibNY.Tests
{
    public class CalculatorTests
    {
        private IItemChecker _itemChecker;
        private ICalculator _sut;

        [SetUp]
        public void Setup()
        {
            _itemChecker = new ItemChecker();
            _sut = new Calculator(_itemChecker);
        }

        [Test]
        [TestCase(1, 1, "+", ExpectedResult = 2)]
        [TestCase(1, 1, "-", ExpectedResult = 0)]
        [TestCase(4, 2, "*", ExpectedResult = 8)]
        [TestCase(4, 2, "/", ExpectedResult = 2)]
        public double Can_Calculate_From_Operation(int v1, int v2, string op)
        {
            return _sut.Calculate(v1, v2, op);
        }

        [Test]
        [TestCase("1", "1", "+", ExpectedResult = "2")]
        [TestCase("1", "1", "-", ExpectedResult = "0")]
        [TestCase("4", "2", "*", ExpectedResult = "8")]
        [TestCase("4", "2", "/", ExpectedResult = "2")]
        public string Can_Calculate_From_Short_Queue(string item1, string item2, string item3)
        {
            Queue<string> items = new Queue<string>();
            items.Enqueue(item1);
            items.Enqueue(item2);
            items.Enqueue(item3);
            return _sut.Calculate(items).Single();
        }

        [Test]
        [TestCase("10", "5", "6", "+", "1", "*", "+", ExpectedResult = "21")]
        [TestCase("6", "4", "8", "*", "10", "/", "+", ExpectedResult = "9.2")]
        public string Can_Calculate_From_Long_Queue(string item1, string item2, string item3,
            string item4, string item5, string item6, string item7)
        {
            Queue<string> items = new Queue<string>();
            items.Enqueue(item1);
            items.Enqueue(item2);
            items.Enqueue(item3);
            items.Enqueue(item4);
            items.Enqueue(item5);
            items.Enqueue(item6);
            items.Enqueue(item7);
            return _sut.Calculate(items).Single();
        }

        [Test]
        [TestCase("10", "5", "6", "+")]
        public void Can_Calculate_From_Incomplete_Queue(string item1, string item2, string item3,
            string item4)
        {
            Queue<string> items = new Queue<string>();
            items.Enqueue(item1);
            items.Enqueue(item2);
            items.Enqueue(item3);
            items.Enqueue(item4);
            var result = _sut.Calculate(items).ToList();
            Assert.That(result[0], Is.EqualTo(item1));
            Assert.That(result[1], Is.EqualTo("11"));
        }
    }
}
