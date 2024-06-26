﻿using ChallengeCacibNY.Api.Controllers;
using ChallengeCacibNY.Api.Responses;
using ChallengeCacibNY.Core.Data;
using ChallengeCacibNY.Core.Logic;
using ChallengeCacibNY.Core.Models;

namespace ChallengeCacibNY.Tests
{
    public class StackControllerTests
    {
        private Mock<IStackDataManager> _dataManagerMock;
        private Mock<IStackAdder> _stackAdderMock;

        private StackController _sut;
        private StackValue sampleStack;
        private StackValue nullStack;

        [SetUp]
        public void Setup()
        {
            _dataManagerMock = new Mock<IStackDataManager>();
            _stackAdderMock = new Mock<IStackAdder>();
            _sut = new StackController(_stackAdderMock.Object, _dataManagerMock.Object);
            sampleStack = new StackValue() { Id = 1, Results = new List<string> { "10" } };
            nullStack = null;
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetReturnsSuccessIfStackFound(int id)
        {
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(sampleStack);
            Response response = await _sut.Get(id);
            Assert.True(response.IsSuccess);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetReturnsErrorIfStackNotFound(int id)
        {
            StackValue nullValue = null;
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(nullValue);
            Response response = await _sut.Get(id);
            Assert.False(response.IsSuccess);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetReturnsErrorIfException(int id)
        {
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            Response response = await _sut.Get(id);
            Assert.False(response.IsSuccess);
        }

        [Test]
        [TestCase("10")]
        [TestCase("20")]
        [TestCase("+")]
        public async Task CreateReturnsSuccessIfNoError(string item)
        {
            _dataManagerMock.Setup(m => m.UpdateOrInsert(It.IsAny<int>(), It.IsAny<StackValue>()));
            _stackAdderMock.Setup(m => m.Add(It.IsAny<StackValue>(), It.IsAny<string>())).Returns(sampleStack);
            Response response = await _sut.Create(item);
            Assert.True(response.IsSuccess);
        }

        [Test]
        [TestCase("10")]
        [TestCase("20")]
        [TestCase("+")]
        public async Task CreateReturnsErrorIfException(string item)
        {
            _dataManagerMock.Setup(m => m.UpdateOrInsert(It.IsAny<int>(), It.IsAny<StackValue>())).Throws(new Exception());
            _stackAdderMock.Setup(m => m.Add(It.IsAny<StackValue>(), It.IsAny<string>())).Returns(sampleStack);
            Response response = await _sut.Create(item);
            Assert.False(response.IsSuccess);
        }

        [Test]
        [TestCase(1, "10")]
        [TestCase(2, "20")]
        [TestCase(3, "+")]
        public async Task UpdateReturnsSuccessIfFound(int id, string item)
        {
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(sampleStack);
            _dataManagerMock.Setup(m => m.UpdateOrInsert(It.IsAny<int>(), It.IsAny<StackValue>()));
            _stackAdderMock.Setup(m => m.Add(It.IsAny<StackValue>(), It.IsAny<string>())).Returns(sampleStack);
            Response response = await _sut.Update(id, item);
            Assert.True(response.IsSuccess);
        }

        [Test]
        [TestCase(1, "10")]
        [TestCase(2, "20")]
        [TestCase(3, "+")]
        public async Task UpdateReturnsErrorIfNotFound(int id, string item)
        {
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(nullStack);
            _dataManagerMock.Setup(m => m.UpdateOrInsert(It.IsAny<int>(), It.IsAny<StackValue>()));
            _stackAdderMock.Setup(m => m.Add(It.IsAny<StackValue>(), It.IsAny<string>())).Returns(sampleStack);
            Response response = await _sut.Update(id, item);
            Assert.False(response.IsSuccess);
        }

        [Test]
        [TestCase(1, "10")]
        [TestCase(2, "20")]
        [TestCase(3, "+")]
        public async Task UpdateReturnsErrorIfException(int id, string item)
        {
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            Response response = await _sut.Update(id, item);
            Assert.False(response.IsSuccess);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteReturnsSuccessIfStackFound(int id)
        {
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(sampleStack);
            Response response = await _sut.Delete(id);
            Assert.True(response.IsSuccess);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteReturnsErrorIfStackNotFound(int id)
        {
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(nullStack);
            Response response = await _sut.Delete(id);
            Assert.False(response.IsSuccess);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteReturnsErrorIfException(int id)
        {
            _dataManagerMock.Setup(m => m.Get(It.IsAny<int>())).ThrowsAsync(new Exception());
            Response response = await _sut.Delete(id);
            Assert.False(response.IsSuccess);
        }
    }
}
