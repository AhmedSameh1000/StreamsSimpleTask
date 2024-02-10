using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleTask.BAL.Services.Interfaces;

namespace SimpleTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityServices _PriorityServices;

        public PriorityController(IPriorityServices priorityServices)
        {
            _PriorityServices = priorityServices;
        }

        [HttpGet("GetPriorities")]
        public async Task<IActionResult> getPriorities()
        {
            var Result = await _PriorityServices.GetPrioritiesAsync();
            return Ok(Result);
        }
    }
}