using ChallengeCacibNY.Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCacibNY.Tests
{
    public class ItemCheckerTests
    {
        private IItemChecker _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ItemChecker();
        }

        [Test]
        [TestCase("+", ExpectedResult = true)]
        [TestCase("-", ExpectedResult = true)]
        [TestCase("*", ExpectedResult = true)]
        [TestCase("/", ExpectedResult = true)]
        [TestCase("1", ExpectedResult = false)]
        [TestCase("10", ExpectedResult = false)]
        [TestCase("100", ExpectedResult = false)]
        [TestCase("@", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = false)]
        public bool Can_Check_Operator(string item)
        {
            return _sut.IsOperator(item);
        }

        [Test]
        [TestCase("+", ExpectedResult = false)]
        [TestCase("-", ExpectedResult = false)]
        [TestCase("*", ExpectedResult = false)]
        [TestCase("/", ExpectedResult = false)]
        [TestCase("1", ExpectedResult = true)]
        [TestCase("10", ExpectedResult = true)]
        [TestCase("100", ExpectedResult = true)]
        [TestCase("@", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = false)]
        public bool Can_Check_Number(string item)
        {
            return _sut.IsNumber(item);
        }
    }
}
