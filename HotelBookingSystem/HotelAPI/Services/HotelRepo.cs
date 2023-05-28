using HotelAPI.Interfaces;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class HotelRepo : IRepo<Hotel, int>
    {
        private readonly HotelContext _context;

        /// <summary>
        /// This method is used to inject the dependencies
        /// </summary>
        /// <param name="context"></param>
        public HotelRepo(HotelContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method adds a hotel to the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Hotel</returns>
        public Hotel Add(Hotel item)
        { 
            _context.Hotels.Add(item);
            _context.SaveChanges();
            return item;
        }

        /// <summary>
        /// This method deletes a hotel from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Hotel</returns>
        public Hotel Delete(int id)
        {
            var hotel = _context.Hotels.SingleOrDefault(h=>h.HotelId == id);
            if(hotel != null)
            {
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
            }
            return hotel;
        }

        /// <summary>
        /// This method gets a hotel from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Hotel</returns>
        public Hotel Get(int id)
        {
            return _context.Hotels.SingleOrDefault(h=>h.HotelId == id);
        }


        /// <summary>
        /// This method gets all hotels from the database.
        /// </summary>
        /// <returns>List of Hotel</returns>
        public ICollection<Hotel> GetAll()
        {
            return _context.Hotels.ToList();
        }

        /// <summary>
        /// This method updates a hotel in the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Hotel<returns>
        public Hotel Update(Hotel item)
        {
            var hotel = _context.Hotels.SingleOrDefault(h=>h.HotelId == item.HotelId);
            if(hotel != null)
            {
                hotel.Name = item.Name;
                hotel.Address = item.Address;
                hotel.City = item.City;
                hotel.Country = item.Country;
                hotel.Phone = item.Phone;
                hotel.Email = item.Email;
                _context.SaveChanges();
            }
            return hotel;
        }
    }
}
