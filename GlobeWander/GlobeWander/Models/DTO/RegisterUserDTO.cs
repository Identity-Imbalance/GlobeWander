using System.ComponentModel.DataAnnotations;

namespace GlobeWander.Models.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }
    }
}
