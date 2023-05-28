using HotelAPI.Models;
using HotelAPI.Models.DTO;

namespace HotelAPI.Interfaces
{
    public interface IFilter
    {
        List<Hotel> GetHotelbyFilter(HotelFilterDTO hotelFilterDTO);
        List<Room> GetRoomsByFilter(RoomFilterDTO roomFilterDTO);
    }
}
