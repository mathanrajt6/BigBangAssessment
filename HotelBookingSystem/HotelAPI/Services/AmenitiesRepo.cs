using HotelAPI.Interfaces;
using HotelAPI.Models;

namespace HotelAPI.Services
{
    public class AmenitiesRepo : IRepo<Amenities, int>
    {
        private readonly HotelContext _context;
        public AmenitiesRepo(HotelContext context)
        {
            _context = context;
        }
        public Amenities Add(Amenities item)
        {
            var amenity = _context.Amenities.SingleOrDefault(a=>a.Name==item.Name);
            if(amenity == null)
            {
                _context.Amenities.Add(item);
                _context.SaveChanges();
            }
            return item;
        }

        public Amenities Delete(Amenities item)
        {
            var amenity = _context.Amenities.SingleOrDefault(a=>a.Id == item.Id);
            if(amenity != null)
            {
                _context.Amenities.Remove(amenity);
                _context.SaveChanges();
            }
            return amenity;
        }

        public Amenities Get(int id)
        {
            return _context.Amenities.SingleOrDefault(a=>a.Id == id);
        }

        public ICollection<Amenities> GetAll()
        {
            return _context.Amenities.ToList();
        }

        public Amenities Update(Amenities item)
        {
            var amenity = _context.Amenities.SingleOrDefault(a=>a.Id == item.Id);
            if(amenity != null)
            {
                amenity.Name = item.Name;
                amenity.Description = item.Description;
                _context.SaveChanges();
            }
            return amenity;
        }
    }
}
