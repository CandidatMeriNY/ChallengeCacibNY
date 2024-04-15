using ChallengeCacibNY.Api.Responses;
using ChallengeCacibNY.Core.Data;
using ChallengeCacibNY.Core.Logic;
using ChallengeCacibNY.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCacibNY.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StackController : ControllerBase
    {
        private readonly IStackDataManager _dataManager;
        private readonly IStackAdder _stackAdder;

        public StackController(IStackAdder stackAdder, IStackDataManager dataManager)
        {
            _stackAdder = stackAdder;
            _dataManager = dataManager;
        }

        [HttpGet]
        public async Task<StackResponse> Get(int id)
        {
            try
            {
                var v = await _dataManager.Get(id);
                if (v == null)
                {
                    return new StackResponse { IsSuccess = false, Message = "Not found" };
                }

                return new StackResponse { IsSuccess = true, Content = v };
            }
            catch (Exception ex)
            {
                return new StackResponse { IsSuccess = false, Message = ex.Message };
            }
        }

        [HttpPost]
        public async Task<StackResponse> Create([FromBody] string item)
        {
            try
            {
                var id = StackDataManager.MaxId + 1;
                var stack = _stackAdder.Add(new StackValue()
                {
                    Id = id,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                }, item);
                await _dataManager.UpdateOrInsert(id, stack);
                return new StackResponse { IsSuccess = true, Content = stack };
            }
            catch (Exception ex)
            {
                return new StackResponse { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
