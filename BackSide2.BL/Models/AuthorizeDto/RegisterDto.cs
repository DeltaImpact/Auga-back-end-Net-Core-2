using System.ComponentModel.DataAnnotations;

namespace Auga.BL.Models.AuthorizeDto
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Email { get; set; }
    }
}