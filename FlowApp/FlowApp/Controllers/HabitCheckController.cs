using FlowApp.Domain;
using FlowApp.Service;
using FlowApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FlowApp.Controllers
{
    [Route("habitCheck")]
    [ApiController]
    public class HabitCheckController : ControllerBase
    {
        private readonly ILogger<HabitCheckController> _logger;
        private readonly IHabitCheckService _habitCheckService;

        public HabitCheckController(ILogger<HabitCheckController> logger, IHabitCheckService habitCheckService)
        {
            _logger = logger;
            _habitCheckService = habitCheckService;
        }

        [HttpPost("create", Name = "createHabitCheck")]
        public async Task<IActionResult> CreateAsync([FromBody] HabitCheckDomain habitCheck)
        {
            try
            {
                var result = await _habitCheckService.CreateAsync(habitCheck);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 115, "create", ex);
            }
        }

        [HttpGet("all", Name = "getHabitChecks")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _habitCheckService.GetAsync();
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 125, "habitChecks", ex);
            }
        }

        [HttpPut("change", Name = "changeHabitCheck")]
        public async Task<IActionResult> ChangeAsync([FromBody] HabitCheckParams habitCheck)
        {
            try
            {
                var result = await _habitCheckService.ChangeAsync(habitCheck);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 135, "change", ex);
            }
        }
    }
}
