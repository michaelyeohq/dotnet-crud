using System.ComponentModel.DataAnnotations;

namespace Store.Dtos.Users
{
    public class UserAuthenticateDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}