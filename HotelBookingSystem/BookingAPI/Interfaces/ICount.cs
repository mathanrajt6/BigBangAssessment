using BookingAPI.Models.DTO;

namespace BookingAPI.Interfaces
{
    public interface ICount
    {
        HotelCountDTO GetCountOfBookedRoomForHotel(HotelDTO hotelDTO);
        List<HotelCountDTO> GetCountOfBookedRoomForAllHotel(DateDTO date);
    }
}
