using HotelAPI.Models;
using HotelAPI.Models.DTO;

namespace HotelAPI.Interfaces
{
    public interface IFilter
    {
        List<Hotel> GetHotelByAmentities(Amenities amenities);
        List<Hotel> GetHotelByCity(HotelFilterDTO hotelFilterDTO);
        List<Hotel> GetHotelByCountry(HotelFilterDTO hotelFilterDTO);
        List<Hotel> GetHotelByPriceRange(HotelFilterDTO hotelFilterDTO);
        List<Room> GetRoomsByAC(Hotel hotel);
        List<Room> GetRoomsByCapacity(RoomFilterDTO roomFilterDTO);
        List<Room> GetRoomsByPriceRange(RoomFilterDTO roomFilterDTO);
    }
}
