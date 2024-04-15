using ChallengeCacibNY.Api.Controllers;
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
    }
}
