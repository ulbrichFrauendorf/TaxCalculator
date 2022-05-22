using System.ComponentModel.DataAnnotations;

namespace DataServices.Authorization.Models
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
