using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class HotelAmenities
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        [ForeignKey("Hotels")]

        public Hotel? Hotel { get; set; }
        public int AmenitiesId { get; set; }
        [ForeignKey("Amennities")]
        public Amenities? Amenities { get; set; }

    }
}
