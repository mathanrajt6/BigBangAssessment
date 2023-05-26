using System.ComponentModel.DataAnnotations;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? HashKey { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
