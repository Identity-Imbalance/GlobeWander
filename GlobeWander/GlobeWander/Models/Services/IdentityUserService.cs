using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace GlobeWander.Models.Services
{/// <summary>
/// Service implementation for user authentication and registration.
/// </summary>
    public class IdentityUserService : IUser
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly JWTTokenService tokenService;
       
        public IdentityUserService(UserManager<ApplicationUser> manager, JWTTokenService tokenService)
        {
            _UserManager = manager;
            this.tokenService = tokenService;
        }


        /// <summary>
        /// Authenticate a user based on username and password.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await _UserManager.FindByNameAsync(username);
            bool isValidPass = await _UserManager.CheckPasswordAsync(user, password);
            if(isValidPass)
            {
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(100)),
                    Roles = await _UserManager.GetRolesAsync(user)
                };
            }
            return null;
        
        }

        /// <summary>
        /// Get user information based on the ClaimsPrincipal.
        /// </summary>
        /// <param name="principal">ClaimsPrincipal representing the user.</param>
        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _UserManager.GetUserAsync(principal);

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(100)),
                Roles = await _UserManager.GetRolesAsync(user)
            };
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
           return  await _UserManager.FindByIdAsync(userId);
        }

        
        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="registerUserDto">Registration information for the user.</param>
        /// <param name="modelState">ModelStateDictionary to store validation errors.</param>
        public async Task<UserDTO> Register(RegisterUserDTO registerUserDto, ModelStateDictionary modelState,ClaimsPrincipal User)
        {
            bool IsAdminManager = User.IsInRole("Admin Manager");

            if (IsAdminManager || registerUserDto.Roles.Contains("User"))
            {
                var user = new ApplicationUser()
                {
                    UserName = registerUserDto.UserName,
                    Email = registerUserDto.Email,
                    PhoneNumber = registerUserDto.PhoneNumber,
                };
                var result = await _UserManager.CreateAsync(user, registerUserDto.Password);
                if (result.Succeeded)
                {
                    await _UserManager.AddToRolesAsync(user, registerUserDto.Roles);
                    return new UserDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(100)),
                        Roles = await _UserManager.GetRolesAsync(user)

                    };
                }
                else
                {
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
             else
            {
                modelState.AddModelError("", "You don't have permission to create this type of account.");
                return null;
            }
            
        }

        public async Task<UserDTO> UpdateProfile(UserUpdateDTO updateDTO, ClaimsPrincipal claimsPrincipal)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var getUserId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _UserManager.FindByIdAsync(getUserId);

            if (user == null)
            {
                return null;
            }
            var userByUserNameExist = await _UserManager.FindByNameAsync(updateDTO.UserName);
            var userByEmailExist = await _UserManager.FindByEmailAsync(updateDTO.Email);
            if ((userByUserNameExist != null && userByUserNameExist != user) || (userByEmailExist != null && userByEmailExist !=user))
            {
                return null;
            }
            user.UserName = updateDTO.UserName;
            user.Email = updateDTO.Email;
            user.PhoneNumber = updateDTO.PhoneNumber;

            var update = new UserDTO
            {
                Id = user.Id,
                UserName = updateDTO.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(100)),
                Roles = await _UserManager.GetRolesAsync(user)
            };
            user.PasswordHash = hasher.HashPassword(user,updateDTO.Password);
            await _UserManager.UpdateAsync(user);

            return update;
        }
    }
}