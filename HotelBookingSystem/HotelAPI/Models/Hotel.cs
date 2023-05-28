using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HotelAPI.Models
{
    public class Hotel : IEquatable<Hotel>
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Name should be maximum of 50 character")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Address should be maximum of 100 character")]
        public string Address { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "City should be maximum of 30 character")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "City should contain only alphabets or characters")]
        public string City { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Country should be maximum of 30 character")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "Country should contain only alphabets or characters")]
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone should be a 10-digit number")]
        public string Phone { get; set; }

        [MaxLength(50, ErrorMessage = "Email should be maximum of 50 character")]
        public string Email { get; set; }


        public bool Equals(Hotel? other)
        {
            return this.HotelId==other.HotelId;
        }

        
    }

   

}
