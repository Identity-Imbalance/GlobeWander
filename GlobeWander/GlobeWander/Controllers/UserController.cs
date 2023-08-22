using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Controllers
{/// <summary>
/// API controller for managing user-related operations.
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="Data">Registration data for the new user.</param>
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO Data)
        {
            var user = await _user.Register(Data, this.ModelState);
            if (ModelState.IsValid)
            {
                if (user != null)
                    return user;

                else
                    return NotFound();
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        /// <summary>
        /// Authenticate a user and perform login.
        /// </summary>
        /// <param name="loginDto">Login credentials.</param>
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LogInDTO loginDto)
        {
            var user = await _user.Authenticate(loginDto.UserName, loginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return user;
        }

        /// <summary>
        /// Get the profile information of the currently authenticated user.
        /// </summary>
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDTO>> Profile()
        {
            var profile = await _user.GetUser(this.User);

            return Ok(profile);
        }
    }
}
