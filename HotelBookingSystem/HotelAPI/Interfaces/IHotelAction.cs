using HotelAPI.Models;
using HotelAPI.Models.DTO;

namespace HotelAPI.Interfaces
{
    public interface IHotelAction : IFilter, ICount
    {
        Hotel AddHotel(Hotel hotel);
        Hotel UpdateHotel(Hotel hotel);
        Hotel DeleteHotel(HotelDTO hotelDTO);
        List<Hotel> GetAllHotels();

        Room AddRoom(Room room);
        Room UpdateRoom(Room room);
        Room DeleteRoom(RoomDTO roomDTO);
        List<Room> GetAllRoomsByHotel(HotelDTO hotelDTO);
        Room GetRoombyHotel(HotelRoomDTO hotelRoomDTO);

        Amenity AddAmenities(Amenity amenities);
        Amenity UpdateAmenities(Amenity amenities);
        Amenity DeleteAmenities(AmenityDTO amenityDTO);
        List<Amenity> GetAllAmenities();
        HotelAmenity AddAmentitiesToHotel(HotelAmenity hotelAmenities);
        HotelAmenity RemoveAmentitiesToHotel(HotelAmenityDTO hotelAmenityDTO);
        List<Amenity> GetAllAmenitiesbyHotel(HotelDTO hotelDTO);
        List<Room> GetAvailableRoomByHotel(List<RoomDTO> roomDTOs);


    }
}
