using FlowApp.Domain;
using FlowApp.Service;
using FlowApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FlowApp.Controllers
{
    [Route("goal")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly ILogger<GoalController> _logger;
        private readonly IGoalService _goalService;

        public GoalController(ILogger<GoalController> logger, IGoalService goalService)
        {
            _logger = logger;
            _goalService = goalService;
        }

        [HttpPost("create", Name = "createGoal")]
        public async Task<IActionResult> CreateAsync([FromBody] GoalDomain goal)
        {
            try
            {
                var result = await _goalService.CreateAsync(goal);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 035, "create", ex);
            }
        }

        [HttpGet("all", Name = "getGoals")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _goalService.GetAsync();
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 045, "goals", ex);
            }
        }

        [HttpPut("change", Name = "changeGoal")]
        public async Task<IActionResult> ChangeAsync([FromBody] GoalParams goal)
        {
            try
            {
                var result = await _goalService.ChangeAsync(goal);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 055, "change", ex);
            }
        }

        [HttpDelete("delete", Name = "deleteGoal")]
        public async Task<IActionResult> DeleteAsync(string prefix)
        {
            try
            {
                var result = await _goalService.DeleteAsync(prefix);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 065, "delete", ex);
            }
        }
    }
}
