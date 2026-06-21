using Microsoft.AspNetCore.Mvc;
using server.Interfaces;
using static server.DTOs.UserDTOs;

namespace server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("usersList")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsersList()
        {
            _logger.LogInformation("Fetching all users list");
            try
            {
                var users = await _userService.GetUsersList();
                _logger.LogInformation("Successfully retrieved users list");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users list");
                return StatusCode(500, new { message = "An error occurred while fetching users" });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(string id)
        {
            _logger.LogInformation("Fetching user - UserId: {UserId}", id);
            try
            {
                var user = await _userService.GetUserById(id);
                if(user == null)
                {
                    _logger.LogWarning("User not found - UserId: {UserId}", id);
                    return NotFound(new { message = $"User with ID {id} does not exist!" });
                }
                _logger.LogInformation("Successfully retrieved user - UserId: {UserId}", id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user - UserId: {UserId}", id);
                return StatusCode(500, new { message = "An error occurred while fetching user" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] UserCreateDto user)
        {
            _logger.LogInformation("Creating new user - UserId: {UserId}, Email: {Email}", user.Id, user.Email);
            try
            {
                var createdUser = await _userService.CreateUser(user);
                if (createdUser == null)
                {
                    _logger.LogWarning("Cannot create user - User already exists - UserId: {UserId}", user.Id);
                    return BadRequest(new { message = $"User with ID {user.Id} already exists!" });
                }
                _logger.LogInformation("User created successfully - UserId: {UserId}", createdUser.Id);
                return Ok(user);
            }
            catch(Exception ex)
            {
            _logger.LogInformation("Deleting user - UserId: {UserId}", id);
            try
            {
                var user = await _userService.DeleteUser(id);
                if(!user)
                {
                    _logger.LogWarning("Cannot delete user - User not found - UserId: {UserId}", id);
                    return NotFound(new { message = $"User with ID {id} does not exist!" });
                }
                _logger.LogInformation("User deleted successfully - UserId: {UserId}", id);
                return Ok("Deleted!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user - UserId: {UserId}", id);
                return StatusCode(500, new { message = "An error occurred while deleting user" });
            }
_logger.LogInformation("Updating user - UserId: {UserId}", id);
            try
            {
                var updatedUser = await _userService.UpdateUser(id, user);
                if(updatedUser == null)
                {
                    _logger.LogWarning("Cannot update user - User not found - UserId: {UserId}", id);
                    return NotFound(new { message = $"User with ID {id} does not exist!" });
                }
                _logger.LogInformation("User updated successfully - UserId: {UserId}", id);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user - UserId: {UserId}", id);
                return StatusCode(500, new { message = "An error occurred while updating user" });
            }
            var user = await _userService.DeleteUser(id);
            if(!user)
                return NotFound(new { message = $"User with ID {id} does not exist!" });
            return Ok("Deleted!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(string id, [FromBody] UserUpdateDto user)
        {
            var updatedUser = await _userService.UpdateUser(id,user);
            if(updatedUser == null)
                return NotFound(new { message = $"User with ID {id} does not exist!" });
            return Ok(updatedUser);
        }
    }
}