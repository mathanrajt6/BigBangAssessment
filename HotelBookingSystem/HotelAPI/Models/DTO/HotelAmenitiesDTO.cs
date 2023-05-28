using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models.DTO
{
    public class HotelAmenitiesDTO
    {
        public int HotelAmentityId { get; set; }
        public int HotelId { get; set; }
        public int AmentityId { get; set; }
    }
}
