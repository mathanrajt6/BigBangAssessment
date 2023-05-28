using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models
{
    public class Amenity
    {
        [Key]
        public int AmentityId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name should be maximum of 50 character")]
        public string Name { get; set; }


        [MaxLength(200, ErrorMessage = "Description should be maximum of 200 character")]
        public string Description { get; set; }

    }
}
