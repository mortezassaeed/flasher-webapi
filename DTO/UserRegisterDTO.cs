using System.ComponentModel.DataAnnotations;

namespace FlasherWebApi.DTO
{
    public class UserRegisterDTO
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
