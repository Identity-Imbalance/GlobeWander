using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GlobeWander.Models.Services
{
    public class IdentityUserService : IUser
    {
        private UserManager<ApplicationUser> _UserManager;
        public IdentityUserService(UserManager<ApplicationUser> manager)
        {
            _UserManager = manager;
        }
        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await _UserManager.FindByNameAsync(username);
            bool isValidPass = await _UserManager.CheckPasswordAsync(user, password);
            if(isValidPass)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }
            return null;
        
        }

        public async Task<UserDTO> Register(RegisterUserDTO registerUserDto, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
                PhoneNumber = registerUserDto.PhoneNumber,
            };
            var result = await _UserManager.CreateAsync(user, registerUserDto.Password);
            if(result.Succeeded)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                };
            }
            foreach (var error in result.Errors)
            {
                var errorMessage = error.Code.Contains("Password") ? nameof(registerUserDto.Password) :
                                   error.Code.Contains("Email") ? nameof(registerUserDto.Email) :
                                   error.Code.Contains("Username") ? nameof(registerUserDto.UserName) :
                                 //  error.Code.Contains("Phone") ? nameof(registerUserDto.Phone) :
                                   "";
                modelState.AddModelError(errorMessage, error.Description);

            };
            return null;

        }
    }
}
