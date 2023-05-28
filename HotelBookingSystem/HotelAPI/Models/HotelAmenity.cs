using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class HotelAmenity
    {
        [Key]
        public int HotelAmentityId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "HotelId shoule be positive")]
        [DefaultValue(0)]

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AmenityId shoule be positive")]
        [DefaultValue(0)]
        public int AmentityId { get; set; }
        [ForeignKey("AmentityId")]
        public Amenity? Amenities { get; set; }

    }
}
