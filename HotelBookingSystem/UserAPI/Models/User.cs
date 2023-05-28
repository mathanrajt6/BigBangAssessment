using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        [MaxLength(50, ErrorMessage = "Username should be maximum of 50 character")]
        public string Username { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? HashKey { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone should be a 10-digit number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name should be maximum of 50 character")]
        public string Name { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Age should be between 1 and 100")]
        [DefaultValue(0)]
        public int Age { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Email should be maximum of 50 character")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Role should be maximum of 50 character")]
        public string Role { get; set; }
    }
}
