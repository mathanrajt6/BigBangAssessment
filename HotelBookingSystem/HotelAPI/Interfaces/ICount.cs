using HotelAPI.Models;
using HotelAPI.Models.DTO;

namespace HotelAPI.Interfaces
{
    public interface ICount
    {
        HotelCountDTO GetRoomAndAmenityForHotel(HotelDTO hotelDTO);
        List<HotelCountDTO> GetRoomAndAmenityForAllHotel();

    }
}
