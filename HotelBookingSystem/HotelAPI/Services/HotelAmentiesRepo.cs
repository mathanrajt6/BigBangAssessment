using HotelAPI.Interfaces;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class HotelAmentiesRepo : IRepo<HotelAmenities, int>
    {
        private readonly HotelContext _context;
        public HotelAmentiesRepo(HotelContext context)
        {
            _context = context;
        }

        public HotelAmenities Add(HotelAmenities item)
        {
            var hotelAmenity = _context.HotelAmenities.SingleOrDefault(ha=>ha.HotelId == item.HotelId && ha.AmenitiesId == item.AmenitiesId);
            if(hotelAmenity == null)
            {
                _context.HotelAmenities.Add(item);
                _context.SaveChanges();
            }
            return item;
        }

        public HotelAmenities Delete(HotelAmenities item)
        {
            var hotelAmenity = _context.HotelAmenities.SingleOrDefault(ha=>ha.HotelId == item.HotelId && ha.AmenitiesId == item.AmenitiesId);
            if(hotelAmenity != null)
            {
                _context.HotelAmenities.Remove(hotelAmenity);
                _context.SaveChanges();
            }
            return hotelAmenity;
        }

        public HotelAmenities Get(int id)
        {
            return _context.HotelAmenities.SingleOrDefault(ha=>ha.HotelId == id);
        }

        public ICollection<HotelAmenities> GetAll()
        {
            return _context.HotelAmenities.ToList();
        }

        public HotelAmenities Update(HotelAmenities item)
        {
            var hotelAmenity = _context.HotelAmenities.SingleOrDefault(ha=>ha.HotelId == item.HotelId && ha.AmenitiesId == item.AmenitiesId);
            if(hotelAmenity != null)
            {
                hotelAmenity.HotelId = item.HotelId;
                hotelAmenity.AmenitiesId = item.AmenitiesId;
                _context.SaveChanges();
            }
            return hotelAmenity;
        }
    }
}
