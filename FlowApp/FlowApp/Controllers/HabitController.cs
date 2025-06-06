using FlowApp.Domain;
using FlowApp.Service;
using FlowApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FlowApp.Controllers
{
    [Route("habit")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly ILogger<HabitController> _logger;
        private readonly IHabitService _habitService;

        public HabitController(ILogger<HabitController> logger, IHabitService habitService)
        {
            _logger = logger;
            _habitService = habitService;
        }

        [HttpPost("create", Name = "createHabit")]
        public async Task<IActionResult> CreateAsync([FromBody] HabitDomain habit)
        {
            try
            {
                var result = await _habitService.CreateAsync(habit);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 075, "create", ex);
            }
        }

        [HttpGet("all", Name = "getHabits")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _habitService.GetAsync();
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 085, "habits", ex);
            }
        }

        [HttpPut("change", Name = "changeHabit")]
        public async Task<IActionResult> ChangeAsync([FromBody] HabitParams habit)
        {
            try
            {
                var result = await _habitService.ChangeAsync(habit);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 095, "change", ex);
            }
        }

        [HttpDelete("delete", Name = "deleteHabit")]
        public async Task<IActionResult> DeleteAsync(string prefix)
        {
            try
            {
                var result = await _habitService.DeleteAsync(prefix);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 105, "delete", ex);
            }
        }
    }
}
