using Microsoft.AspNetCore.Mvc;
using SunTrack.API.Data.Models;
using SunTrack.API.Services.Users;
using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SunTrack.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(IEnumerable<UserVM>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<UserVM>>> GetAllUsers(SearchVM search)
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(search);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving users: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserVM user)
        {
            var result = await _userService.AddUserAsync(user);
            return result ? Ok("User added successfully.") : BadRequest("Failed to add user.");
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserVM user)
        {
            var result = await _userService.UpdateUserAsync(user);
            return result ? Ok("User updated successfully.") : NotFound("User not found.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return result ? Ok("User deleted successfully.") : NotFound("User not found.");
        }
    }
}
