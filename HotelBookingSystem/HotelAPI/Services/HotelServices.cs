using HotelAPI.Exceptions;
using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Models.DTO;

namespace HotelAPI.Services
{
    public class HotelServices : IHotelAction
    {
        private readonly IRepo<Hotel,int> _hotelrepo;
        private readonly IRepo<Amenity,int> _amenitiesrepo;
        private readonly IRepo<Room,int> _roomrepo;
        private readonly IRepo<HotelAmenity,int> _hotelamenitiesrepo;

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="hotelrepo"></param>
        /// <param name="amenitiesrepo"></param>
        /// <param name="roomrepo"></param>
        /// <param name="hotelamenitiesrepo"></param>
        public HotelServices(IRepo<Hotel,int> hotelrepo, IRepo<Amenity, int> amenitiesrepo, IRepo<Room, int> roomrepo, IRepo<HotelAmenity, int> hotelamenitiesrepo)
        {
            _amenitiesrepo = amenitiesrepo;
            _hotelrepo = hotelrepo;
            _roomrepo = roomrepo;
            _hotelamenitiesrepo = hotelamenitiesrepo;
        }

        /// <summary>
        /// This method is used to add amenities to the database.
        /// </summary>
        /// <param name="amenities"></param>
        /// <returns>Amenity</returns>
        public Amenity AddAmenities(Amenity amenities)
        {   
            var amenity = _amenitiesrepo.GetAll().SingleOrDefault(a=>a.Name == amenities.Name);
            if (amenity == null)
            {
                var newAmenity = _amenitiesrepo.Add(amenities);
                return newAmenity;
            }
            return null;
        }

        /// <summary>
        /// this method is used to add amenities to a hotel
        /// </summary>
        /// <param name="hotelAmenities"></param>
        /// <returns>HotelAmenity</returns>
        /// <exception cref="HotelException"></exception>
        /// <exception cref="AmentitiesException"></exception>
        public HotelAmenity AddAmentitiesToHotel(HotelAmenity hotelAmenities)
        {
            var hotel = _hotelrepo.Get(hotelAmenities.HotelId);
            if (hotel == null)
                throw new HotelException("Hotel not Exists");
            var amentity = _amenitiesrepo.Get(hotelAmenities.AmentityId);
            if (amentity == null)
                throw new AmentitiesException("Amentity not Exists");
            var hotelamenity = _hotelamenitiesrepo.GetAll().SingleOrDefault(ha=>ha.AmentityId == hotelAmenities.AmentityId && ha.HotelId == hotelAmenities.HotelId);
            if(hotelamenity == null)
            {
                _hotelamenitiesrepo.Add(hotelAmenities);
                return hotelAmenities;
            }
            return null;
        }

        /// <summary>
        /// This method is used to add a hotel to the database
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Hotel</returns>
        public Hotel AddHotel(Hotel hotel)
        {
            var hotels = _hotelrepo.GetAll();
            foreach(var h in hotels)
            {
                if(exists(hotel,h))
                    return null;
            }
            return _hotelrepo.Add(hotel);

        }

        /// <summary>
        ///This method is used to check whether the hotel already exists in the database
        /// </summary>
        /// <param name="current"></param>
        /// <param name="existing"></param>
        /// <returns>Boolean</returns>
        private bool exists(Hotel current,Hotel existing)
        {
            if (current.Name == existing.Name && current.Address == existing.Address && current.City == existing.City && current.Country == existing.Country)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method is used to add a room to the database
        /// </summary>
        /// <param name="room"></param>
        /// <returns>Room</returns>
        public Room AddRoom(Room room)
        {
            var hotel = _hotelrepo.Get(room.HotelId);
            if(hotel != null)
            {
                return _roomrepo.Add(room);
            }
            return null;
        }

        /// <summary>
        /// This method is used to delete amentities from a hotel
        /// </summary>
        /// <param name="amenityDTO"></param>
        /// <returns>Amenity</returns>
        /// <exception cref="AmentitiesException"></exception>
        public Amenity DeleteAmenities(AmenityDTO amenityDTO)
        {
            var amenity = _amenitiesrepo.GetAll().SingleOrDefault(a=>a.AmentityId==amenityDTO.Id);
            if(amenity == null)
                return null;
            var hotelamenity = _hotelamenitiesrepo.GetAll().Where(ha => ha.AmentityId == amenity.AmentityId).ToList();
            if(hotelamenity.Count == 0)
            {    
                _amenitiesrepo.Delete(amenity.AmentityId);
                return amenity;
            }
            throw new AmentitiesException("Amenities is associated with hotel");
        }

        /// <summary>
        /// This method is used to delete Hotel from a database
        /// </summary>
        /// <param name="hotelDTO"></param>
        /// <returns>Hotel</returns>
        public Hotel DeleteHotel(HotelDTO hotelDTO)
        {
            var rooms = _roomrepo.GetAll().Where(r=>r.HotelId == hotelDTO.Id).ToList();
            foreach(var room in rooms)
            {
                _roomrepo.Delete(room.RoomId);
            }
            var hotelamentities =  _hotelamenitiesrepo.GetAll().Where(ha=>ha.HotelId== hotelDTO.Id).ToList();
            foreach (var hotelamentity in hotelamentities)
            {
                _hotelamenitiesrepo.Delete(hotelamentity.HotelAmentityId);
            }
            return _hotelrepo.Delete(hotelDTO.Id);
        }

        /// <summary>
        /// This method is used to delete a room from a database
        /// </summary>
        /// <param name="roomDTO"></param>
        /// <returns>Room</returns>
        public Room DeleteRoom(RoomDTO roomDTO)
        {
            return _roomrepo.Delete(roomDTO.Id);
        }


        /// <summary>
        /// This method is used to get all the amenities from the database
        /// </summary>
        /// <returns>List of Amenity</returns>
        public List<Amenity> GetAllAmenities()
        {
            return _amenitiesrepo.GetAll().ToList();
        }

        /// <summary>
        /// This method is used to get all the amenities of a hotel
        /// </summary>
        /// <param name="hotelDTO"></param>
        /// <returns>List of Amenity</returns>
        public List<Amenity> GetAllAmenitiesbyHotel(HotelDTO hotelDTO)
        {
            var hotelamenities = _hotelamenitiesrepo.GetAll().Where(ha=>ha.HotelId == hotelDTO.Id).ToList();
            List<Amenity> amenities = new List<Amenity>();
            foreach(var hotelamenity in hotelamenities)
            {
                amenities.Add(_amenitiesrepo.Get(hotelamenity.AmentityId));
            }
            return amenities;
        }

        /// <summary>
        /// This method is used to get all the hotels from the database
        /// </summary>
        /// <returns>List of Hotel</returns>
        public List<Hotel> GetAllHotels()
        {
            return _hotelrepo.GetAll().ToList();
        }

        /// <summary>
        /// this method is used to get all the rooms from the database
        /// </summary>
        /// <param name="hotelDTO"></param>
        /// <returns>List of rooms</returns>
        /// <exception cref="HotelException"></exception>
        public List<Room> GetAllRoomsByHotel(HotelDTO hotelDTO)
        {
            var hotel = _hotelrepo.Get(hotelDTO.Id);
            if(hotel == null)              
                throw new HotelException("Hotel not Exists");
            return _roomrepo.GetAll().Where(r=>r.HotelId == hotelDTO.Id).ToList();
        }

        /// <summary>
        /// This method is used to get a hotel from the database
        /// </summary>
        /// <param name="roomDTOs"></param>
        /// <returns>List of Room</returns>
        public List<Room> GetAvailableRoomByHotel(List<RoomDTO> roomDTOs)
        {
            var rooms = _roomrepo.GetAll().ToList();
            var availableRooms = new List<Room>();
            foreach(var room in roomDTOs)
            {
                var Available = rooms.SingleOrDefault(r=>r.RoomId == room.Id);
                if(Available != null)
                {
                    availableRooms.Add(Available);
               
                }
            }
            return availableRooms;
        }


        /// <summary>
        /// This method is used to get a hotel from the database
        /// </summary>
        /// <param name="hotelFilterDTO"></param>
        /// <returns>List of Hotels</returns>
        public List<Hotel> GetHotelbyFilter(HotelFilterDTO hotelFilterDTO)
        {
            List<Hotel> hotels = _hotelrepo.GetAll().ToList();
            if (hotelFilterDTO.City != null)
            {
                List<Hotel> city = GetHotelByCity(hotelFilterDTO);
                hotels= hotels.Intersect(city).ToList();
            }
            if(hotelFilterDTO.Country != null)
            {
                var country = GetHotelByCountry(hotelFilterDTO);
                hotels = hotels.Intersect(country).ToList();
            }
            if (hotelFilterDTO.AmenityId != null && hotelFilterDTO.AmenityId>=0)
            {
                var hotelamenities = GetHotelByAmentities(hotelFilterDTO);
                hotels = hotels.Intersect(hotelamenities).ToList();
            }
            if (hotelFilterDTO.MaxPrice != null && hotelFilterDTO.MinPrice != null)
            {
                var price = GetHotelByPriceRange(hotelFilterDTO);
                hotels = hotels.Intersect(price).ToList();
            }
            return hotels;

       }

        /// <summary>
        /// This method is used to get a hotel from the database
        /// </summary>
        /// <param name="hotelFilterDTO"></param>
        /// <returns>List of Hotel</returns>
        public List<Hotel> GetHotelByAmentities(HotelFilterDTO hotelFilterDTO)
        {
            var hotelamenities = _hotelamenitiesrepo.GetAll().Where(ha => ha.AmentityId == hotelFilterDTO.AmenityId).ToList();
            List<Hotel> hotels = new List<Hotel>();
            foreach (var hotelamenity in hotelamenities)
            {
                hotels.Add(_hotelrepo.Get(hotelamenity.HotelId));
            }
            return hotels;
        }


        /// <summary>
        /// this method is used to get a hotel from the database
        /// </summary>
        /// <param name="hotelFilterDTO"></param>
        /// <returns>List of Hotel</returns>
        public List<Hotel> GetHotelByCity(HotelFilterDTO hotelFilterDTO)
        {
            return _hotelrepo.GetAll().Where(h => h.City == hotelFilterDTO.City).ToList();
        }

        /// <summary>
        /// This method is used to get a hotel from the database
        /// </summary>
        /// <param name="hotelFilterDTO"></param>
        /// <returns>List of Hotel</returns>
        public List<Hotel> GetHotelByCountry(HotelFilterDTO hotelFilterDTO)
        {
            return _hotelrepo.GetAll().Where(h => h.Country == hotelFilterDTO.Country).ToList();
        }

        /// <summary>
        /// This method is used to get a hotel from the database
        /// </summary>
        /// <param name="hotelFilterDTO"></param>
        /// <returns>List of Hotels</returns>
        public List<Hotel> GetHotelByPriceRange(HotelFilterDTO hotelFilterDTO)
        {
            var hotels = _hotelrepo.GetAll().ToList();
            ISet<Hotel> hotelset = new HashSet<Hotel>();
            foreach (var item in hotels)
            {
                var rooms = GetRoomsByPriceRange(
                    new RoomFilterDTO
                    {
                        HotelId = item.HotelId,
                        MinPrice = hotelFilterDTO.MinPrice,
                        MaxPrice = hotelFilterDTO.MaxPrice
                    });
                if (rooms.Count > 0)
                {
                    hotelset.Add(item);
                }
            }
            return hotelset.ToList();
        }

        /// <summary>
        /// This method is used to get a hotel from the database
        /// </summary>
        /// <param name="hotelRoomDTO"></param>
        /// <returns>List of hotel</returns>
        /// <exception cref="HotelException"></exception>
        public Room GetRoombyHotel(HotelRoomDTO hotelRoomDTO)
        {
            var hotel = _hotelrepo.Get(hotelRoomDTO.HotelId);
            if (hotel == null)
                throw new HotelException("Hotel not Exists");
            return _roomrepo.GetAll().SingleOrDefault(r=>r.HotelId == hotelRoomDTO.HotelId && r.RoomId == hotelRoomDTO.RoomId);
        }


        /// <summary>
        /// This method is used to get a room from the database
        /// </summary>
        /// <param name="roomFilterDTO"></param>
        /// <returns>List of room</returns>
        /// <exception cref="HotelException"></exception>
        public List<Room> GetRoomsByFilter(RoomFilterDTO roomFilterDTO)
        {
            if(roomFilterDTO.HotelId == null)
            {
                throw new HotelException("HotelId is required");
            }
            var rooms = GetAllRoomsByHotel(new HotelDTO { Id = roomFilterDTO.HotelId ?? 0 });
            if (roomFilterDTO.AC != null)
            {
                List<Room> ac = GetRoomsByAC(roomFilterDTO);
                rooms= rooms.Intersect(ac).ToList();
            }
            if (roomFilterDTO.Capacity != null)
            {
                var capacity = GetRoomsByCapacity(roomFilterDTO);
                rooms= rooms.Intersect(capacity).ToList();
            }
            if (roomFilterDTO.MaxPrice != null && roomFilterDTO.MinPrice != null)
            {
                var price = GetRoomsByPriceRange(roomFilterDTO);
                rooms= rooms.Intersect(price).ToList();
            }
            return rooms;
        }

        /// <summary>
        /// This method is used to get a room from the database
        /// </summary>
        /// <param name="roomFilterDTO"></param>
        /// <returns>List of Rooms</returns>
        public List<Room> GetRoomsByAC(RoomFilterDTO roomFilterDTO)
        {
            return _roomrepo.GetAll().Where(r => r.HotelId == roomFilterDTO.HotelId && r.AC == true).ToList();
        }


        /// <summary>
        /// this method is used to get a room from the database
        /// </summary>
        /// <param name="roomFilterDTO"></param>
        /// <returns>List of Rooms</returns>
        public List<Room> GetRoomsByCapacity(RoomFilterDTO roomFilterDTO)
        {
            return _roomrepo.GetAll().Where(r => r.HotelId == roomFilterDTO.HotelId && r.Capacity == roomFilterDTO.Capacity).ToList();
        }

        /// <summary>
        /// This method is used to get a room from the database
        /// </summary>
        /// <param name="roomFilterDTO"></param>
        /// <returns>List of Rooms</returns>
        public List<Room> GetRoomsByPriceRange(RoomFilterDTO roomFilterDTO)
        {
            return _roomrepo.GetAll().Where(r => r.HotelId == roomFilterDTO.HotelId && r.Price >= roomFilterDTO.MinPrice && r.Price <= roomFilterDTO.MaxPrice).ToList();
        }


        /// <summary>
        /// This method is used to remove a amenity from the hotel
        /// </summary>
        /// <param name="hotelAmenityDTO"></param>
        /// <returns>HotelAmenity</returns>
        public HotelAmenity RemoveAmentitiesToHotel(HotelAmenityDTO hotelAmenityDTO)
        {
            var hotelamenity = _hotelamenitiesrepo.GetAll().SingleOrDefault(ha=>ha.HotelAmentityId==hotelAmenityDTO.Id);
            if(hotelamenity != null)
            {
                _hotelamenitiesrepo.Delete(hotelamenity.HotelAmentityId);
                return hotelamenity;
            }
            return null;
        }

        /// <summary>
        /// This method is used to get a Amenity from the database
        /// </summary>
        /// <param name="amenities"></param>
        /// <returns>Amenity</returns>
        public Amenity UpdateAmenities(Amenity amenities)
        {
            var amenity = _amenitiesrepo.GetAll().SingleOrDefault(a=>a.Name == amenities.Name);
            if(amenity != null)
            {
                amenity.Description = amenities.Description;
                return _amenitiesrepo.Update(amenity);
            }
            return null;
        }


        /// <summary>
        /// This method is used to update a hotel in the database
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Hotel</returns>
        /// <exception cref="HotelException"></exception>
        public Hotel UpdateHotel(Hotel hotel)
        {
            var existingHotel = _hotelrepo.Get(hotel.HotelId);
            if(existingHotel==null)
            {
                throw new HotelException("Hotel Not Exists");
            }
            if (exists(existingHotel,hotel))
            {
                throw new HotelException("Hotel Can't Change");
            }
            return _hotelrepo.Update(hotel);
        }

        /// <summary>
        /// This method is used to update room in the database
        /// </summary>
        /// <param name="room"></param>
        /// <returns>Room</returns>
        /// <exception cref="HotelException"></exception>
        public Room UpdateRoom(Room room)
        {
            var hotel = _hotelrepo.Get(room.HotelId);
            if (hotel == null)
            {
                throw new HotelException("Hotel Not Exists");
            }
            return _roomrepo.Update(room);
        }

        /// <summary>
        /// This method is used to get a hotel and count of Room and amenity of a hotel from the database
        /// </summary>
        /// <param name="hotelDTO"></param>
        /// <returns>HotelCountDTO</returns>
        public HotelCountDTO GetRoomAndAmenityForHotel(HotelDTO hotelDTO)
        {
            if (_hotelrepo.Get(hotelDTO.Id) == null)
                return null;
            HotelCountDTO HotelCountDTO = new HotelCountDTO();
            HotelCountDTO.HotelId = hotelDTO.Id;
            HotelCountDTO.Roomcount = _roomrepo.GetAll().Where(r => r.HotelId == hotelDTO.Id).Count();
            HotelCountDTO.AmentiesCount = _hotelamenitiesrepo.GetAll().Where(ha => ha.HotelId == hotelDTO.Id).Count();
            return HotelCountDTO;

        }

        /// <summary>
        /// This method is used to get a hotel and count of Room and amenity of all hotel from the database
        /// </summary>
        /// <returns>List of HotelCountDTO</returns>
        public List<HotelCountDTO> GetRoomAndAmenityForAllHotel()
        {
            List<HotelCountDTO> HotelCountDTOs = new List<HotelCountDTO>();
            List<Hotel> hotels = _hotelrepo.GetAll().ToList();
            foreach (var hotel in hotels)
            {
                HotelCountDTOs.Add(GetRoomAndAmenityForHotel(new HotelDTO { Id = hotel.HotelId }));
            }
            return HotelCountDTOs;
        }
    }
}
