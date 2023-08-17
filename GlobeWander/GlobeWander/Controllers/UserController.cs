using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobeWander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _user;
        public UserController(IUser user) {
        _user = user;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>>Register(RegisterUserDTO Data)
        {
            var user = await _user.Register(Data, this.ModelState);
            if (ModelState.IsValid) {
                return user;
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>>Login(LogInDTO loginDto)
        {
            var user = await _user.Authenticate(loginDto.UserName, loginDto.Password);
            if(user == null)
            {
                return Unauthorized();
            }
            return user;
        }
    }
}
