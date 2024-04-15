using ChallengeCacibNY.Api.Responses;
using ChallengeCacibNY.Core;
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
                    return new StackResponse { IsSuccess = false, Message = Constants.NotFound };
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

        [HttpPut]
        public async Task<StackResponse> Update(int id, [FromBody] string item)
        {
            try
            {
                var existing = await _dataManager.Get(id);
                if (existing != null)
                {
                    var update = _stackAdder.Add(existing, item);
                    update.Updated = DateTime.UtcNow;
                    await _dataManager.UpdateOrInsert(id, update);
                    return new StackResponse { IsSuccess = true, Content = update };
                }
                return new StackResponse { IsSuccess = false, Message = Constants.NotFound };
            }
            catch (Exception ex)
            {
                return new StackResponse { IsSuccess = false, Message = ex.Message };
            }
        }

        [HttpDelete]
        public async Task<Response> Delete(int id)
        {
            try
            {
                var existing = await _dataManager.Get(id);
                if (existing != null)
                {
                    await _dataManager.Delete(id);
                    return new Response { IsSuccess = true };
                }
                return new Response { IsSuccess = false, Message = Constants.NotFound };
            }
            catch (Exception ex)
            {
                return new Response { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
