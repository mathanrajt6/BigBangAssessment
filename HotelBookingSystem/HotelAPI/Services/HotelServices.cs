using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;

namespace HotelAPI.Services
{
    public class HotelServices : IHotelAction
    {
        private readonly IRepo<Hotel,int> _hotelrepo;
        private readonly IRepo<Amenities,int> _amenitiesrepo;
        private readonly IRepo<Room,int> _roomrepo;
        private readonly IRepo<HotelAmenities,int> _hotelamenitiesrepo;
        public HotelServices(IRepo<Hotel,int> hotelrepo, IRepo<Amenities, int> amenitiesrepo, IRepo<Room, int> roomrepo, IRepo<HotelAmenities, int> hotelamenitiesrepo)
        {
            _amenitiesrepo = amenitiesrepo;
            _hotelrepo = hotelrepo;
            _roomrepo = roomrepo;
            _hotelamenitiesrepo = hotelamenitiesrepo;
        }
        public AmentitiesDTO AddAmenities(AmentitiesDTO amenities)
        {   
            var hotel = _hotelrepo.Get(amenities.HotelId);
            if(hotel == null)
                return null;
            var amnity = _amenitiesrepo.GetAll().SingleOrDefault(a=>a.Name == amenities.Name);
            if (amnity == null)
            {
                var amenity = new Amenities
                {
                    Name = amenities.Name,
                    Description = amenities.Description
                };
                var newamentity =_amenitiesrepo.Add(amenity);
                var hotelamnity = _hotelamenitiesrepo.Add(new HotelAmenities
                {
                    HotelId = amenities.HotelId,
                    AmenitiesId = newamentity.Id
                });
                return amenities;
            }
            else
            {
                var hotelamnity = _hotelamenitiesrepo.Add(new HotelAmenities
                {
                    HotelId = amenities.HotelId,
                    AmenitiesId = amnity.Id
                });
                return amenities;
            }
        }

        public Hotel AddHotel(Hotel hotel)
        {
            var hotels = _hotelrepo.GetAll();
            foreach(var h in hotels)
            {
                if(hotel.Equals(h))
                    return null;
            }
            return _hotelrepo.Add(hotel);

        }

        public Room AddRoom(Room room)
        {
            var hotel = _hotelrepo.Get(room.HotelId);
            if(hotel != null)
            {
                return _roomrepo.Add(room);
            }
            return null;
        }

        public AmentitiesDTO DeleteAmenities(AmentitiesDTO amenities)
        {
            var amenity = _amenitiesrepo.GetAll().SingleOrDefault(a=>a.Name == amenities.Name || a.Id == amenities.AmenitiesId);
            if(amenity == null)
                return null;
            HotelAmenities hotelamenity = _hotelamenitiesrepo.GetAll().SingleOrDefault(ha => ha.AmenitiesId == amenity.Id && ha.HotelId == amenities.HotelId);
            if(hotelamenity != null)
            {
                _hotelamenitiesrepo.Delete(hotelamenity);
                return amenities;
            }
            return null;
        }

        public Hotel DeleteHotel(Hotel hotel)
        {
            var rooms = _roomrepo.GetAll().Where(r=>r.HotelId == hotel.Id).ToList();
            foreach(var room in rooms)
            {
                _roomrepo.Delete(room);
            }
            return _hotelrepo.Delete(hotel);
        }

        public Room DeleteRoom(Room room)
        {
            return _roomrepo.Delete(room);
        }

        public List<Amenities> GetAllAmenitiesbyHotel(Hotel hotel)
        {
            var hotelamenities = _hotelamenitiesrepo.GetAll().Where(ha=>ha.HotelId == hotel.Id).ToList();
            List<Amenities> amenities = new List<Amenities>();
            foreach(var hotelamenity in hotelamenities)
            {
                amenities.Add(_amenitiesrepo.Get(hotelamenity.AmenitiesId));
            }
            return amenities;
        }

        public List<Hotel> GetAllHotels()
        {
            return _hotelrepo.GetAll().ToList();
        }

        public List<Room> GetAllRoomsByHotel(Hotel hotel)
        {
            return _roomrepo.GetAll().Where(r=>r.HotelId == hotel.Id).ToList();
        }

        public List<Hotel> GetHotelByAmentities(Amenities amenities)
        {
            var hotelamenities = _hotelamenitiesrepo.GetAll().Where(ha=>ha.AmenitiesId == amenities.Id).ToList();
            List<Hotel> hotels = new List<Hotel>();
            foreach(var hotelamenity in hotelamenities)
            {
                hotels.Add(_hotelrepo.Get(hotelamenity.HotelId));
            }
            return hotels;
        }

        public List<Hotel> GetHotelByCity(HotelFilterDTO hotelFilterDTO)
        {
            return _hotelrepo.GetAll().Where(h=>h.City == hotelFilterDTO.City).ToList();
        }

        public List<Hotel> GetHotelByCountry(HotelFilterDTO hotelFilterDTO)
        {
            return _hotelrepo.GetAll().Where(h=>h.Country == hotelFilterDTO.Country).ToList();
        }

        public List<Hotel> GetHotelByPriceRange(HotelFilterDTO hotelFilterDTO)
        {
            var hotels = _hotelrepo.GetAll().ToList();
            ISet<Hotel> hotelset = new HashSet<Hotel>();
            foreach (var item in hotels)
            {
                var rooms = GetRoomsByPriceRange(
                    new RoomFilterDTO
                    {
                        HotelId = item.Id,
                        MinPrice = hotelFilterDTO.MinPrice,
                        MaxPrice = hotelFilterDTO.MaxPrice
                    });
                if(rooms.Count > 0)
                {
                    hotelset.Add(item);
                }
            }
            return hotelset.ToList();
        }

        public List<Room> GetRoomsByAC(Hotel hotel)
        {
            return _roomrepo.GetAll().Where(r=>r.HotelId == hotel.Id && r.AC == 1).ToList();
        }

        public List<Room> GetRoomsByCapacity(RoomFilterDTO roomFilterDTO)
        {
            return _roomrepo.GetAll().Where(r=>r.HotelId == roomFilterDTO.HotelId && r.Capacity == roomFilterDTO.Capacity).ToList();
        }

        public List<Room> GetRoomsByPriceRange(RoomFilterDTO roomFilterDTO)
        {
            return _roomrepo.GetAll().Where(r=>r.HotelId == roomFilterDTO.HotelId && r.Price >= roomFilterDTO.MinPrice && r.Price <= roomFilterDTO.MaxPrice).ToList();
        }

        public Amenities UpdateAmenities(AmentitiesDTO amenities)
        {
            var amenity = _amenitiesrepo.GetAll().SingleOrDefault(a=>a.Name == amenities.Name);
            if(amenity != null)
            {
                amenity.Description = amenities.Description;
                return _amenitiesrepo.Update(amenity);
            }
            return null;
        }

        public Hotel UpdateHotel(Hotel hotel)
        {
            return _hotelrepo.Update(hotel);
        }

        public Room UpdateRoom(Room room)
        {
            return _roomrepo.Update(room);
        }
    }
}
