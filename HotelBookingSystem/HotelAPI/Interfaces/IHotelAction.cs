using HotelAPI.Models;
using HotelAPI.Models.DTO;

namespace HotelAPI.Interfaces
{
    public interface IHotelAction : IFilter
    {
        Hotel AddHotel(Hotel hotel);
        Hotel UpdateHotel(Hotel hotel);
        Hotel DeleteHotel(Hotel hotel);
        List<Hotel> GetAllHotels();

        Room AddRoom(Room room);
        Room UpdateRoom(Room room);
        Room DeleteRoom(Room room);
        List<Room> GetAllRoomsByHotel(Hotel hotel);

        AmentitiesDTO AddAmenities(AmentitiesDTO amenities);
        Amenities UpdateAmenities(AmentitiesDTO amenities);
        AmentitiesDTO DeleteAmenities(AmentitiesDTO amenities);
        List<Amenities> GetAllAmenitiesbyHotel(Hotel hotel);


    }
}
