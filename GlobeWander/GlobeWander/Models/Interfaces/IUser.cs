using GlobeWander.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace GlobeWander.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUserDTO registerUser, ModelStateDictionary modelState, ClaimsPrincipal claimsPrincipal);

        public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal principal);

        public Task<ApplicationUser> GetUserByIdAsync(string userId);

        public Task<UserDTO> UpdateProfile(UserUpdateDTO updateDTO, ClaimsPrincipal claimsPrincipal);

    }
}

