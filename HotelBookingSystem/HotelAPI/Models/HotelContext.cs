using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Models
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<HotelAmenities> HotelAmenities { get; set; }

    }
}
