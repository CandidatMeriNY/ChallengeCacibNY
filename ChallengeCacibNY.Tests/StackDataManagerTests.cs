using ChallengeCacibNY.Core.Data;
using ChallengeCacibNY.Core.Models;

namespace ChallengeCacibNY.Tests
{
    public class StackDataManagerTests
    {
        private IStackDataManager _sut;
        private StackValue someValue;

        [SetUp]
        public void Setup()
        {
            _sut = new StackDataManager();
            someValue = new StackValue();
        }

        private async Task InsertOrUpdate(int i, int v)
        {
            someValue.Id = i;
            someValue.Results = new List<string> { v.ToString() };
            await _sut.UpdateOrInsert(i, someValue);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public async Task CanInsertAndGet(int i)
        {
            await InsertOrUpdate(i, i);
            var saved = await _sut.Get(i);
            Assert.That(saved.Results.First(), Is.EqualTo(someValue.Results.First()));
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(10, 20)]
        [TestCase(100, 200)]
        public async Task CanUpdateAndGet(int id, int result)
        {
            await InsertOrUpdate(id, id);
            await InsertOrUpdate(id, result);
            var saved = await _sut.Get(id);
            Assert.That(saved.Results.First(), Is.EqualTo(someValue.Results.First()));
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public async Task CanDelete(int i)
        {
            await InsertOrUpdate(i, i);
            await _sut.Delete(i);
            var saved = await _sut.Get(i);
            Assert.IsNull(saved);
        }
    }
}
