using ChallengeCacibNY.Core.Logic;
using ChallengeCacibNY.Core.Models;

namespace ChallengeCacibNY.Tests
{
    public class StackAdderTests
    {
        private IStackAdder _sut;
        private ICalculator _calculator;
        private IItemChecker _itemChecker;

        [SetUp]
        public void SetUp()
        {
            _itemChecker = new ItemChecker();
            _calculator = new Calculator(_itemChecker);
            _sut = new StackAdder(_calculator);
        }

        [Test]
        [TestCase("1", "10")]
        [TestCase("2", "20")]
        public void Can_Transfer_Values(string item1, string item2)
        {
            var stackValue = new StackValue { Id = 1 };

            _sut.Add(stackValue, item1);
            _sut.Add(stackValue, item2);

            Assert.That(stackValue.Results[0], Is.EqualTo(item1));
            Assert.That(stackValue.Results[1], Is.EqualTo(item2));
        }
    }
}
