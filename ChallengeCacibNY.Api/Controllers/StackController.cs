using ChallengeCacibNY.Api.Responses;
using ChallengeCacibNY.Core.Data;
using ChallengeCacibNY.Core.Logic;
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
    }
}
