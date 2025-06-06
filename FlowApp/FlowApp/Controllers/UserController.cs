using FlowApp.Domain;
using FlowApp.Service;
using FlowApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FlowApp.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("create", Name = "createUser")]
        public async Task<IActionResult> CreateAsync([FromBody] UserDomain user)
        {
            try
            {
                var result = await _userService.CreateAsync(user);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 005, "create", ex);
            }
        }

        [HttpGet("all", Name = "getUsers")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _userService.GetAsync();
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 015, "users", ex);
            }
        }

        [HttpDelete("delete", Name = "deleteUser")]
        public async Task<IActionResult> DeleteAsync(string prefix)
        {
            try
            {
                var result = await _userService.DeleteAsync(prefix);
                return await Task.FromResult(this.Ok(result));
            }
            catch (Exception ex)
            {
                return ErrorHandler.HandleError(this._logger, BadRequest, 025, "delete", ex);
            }
        }
    }
}
