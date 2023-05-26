using HotelAPI.Interfaces;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class HotelRepo : IRepo<Hotel, int>
    {
        private readonly HotelContext _context;
        public HotelRepo(HotelContext context)
        {
            _context = context;
        }
        public Hotel Add(Hotel item)
        { 
            _context.Hotels.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Hotel Delete(Hotel item)
        {
            var hotel = _context.Hotels.SingleOrDefault(h=>h.Id == item.Id);
            if(hotel != null)
            {
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
            }
            return hotel;
        }

        public Hotel Get(int id)
        {
            return _context.Hotels.SingleOrDefault(h=>h.Id == id);
        }

        public ICollection<Hotel> GetAll()
        {
            return _context.Hotels.ToList();
        }

        public Hotel Update(Hotel item)
        {
            var hotel = _context.Hotels.SingleOrDefault(h=>h.Id == item.Id);
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
