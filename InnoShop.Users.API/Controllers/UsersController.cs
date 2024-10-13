using InnoShop.Users.API.Models;
using InnoShop.Users.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InnoShop.Users.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await userService.CreateUserAsync(request.Name, request.Email, request.Password);
            var response = new UserResponse(user.Id, user.Name, user.Email, user.Role, user.IsEmailConfirmed, user.CreatedAt);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await userService.LoginAsync(request.Email, request.Password);
            return Ok(new { Token = token });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
        {
            await userService.UpdateUserAsync(id, request.Name, request.Email, request.Role);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            var response = new UserResponse(user.Id, user.Name, user.Email, user.Role, user.IsEmailConfirmed, user.CreatedAt);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            await userService.ResetPasswordAsync(request.Email, request.NewPassword);
            return Ok();
        }
    }
}
