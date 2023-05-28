using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Models
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>().HasData(
                                new Hotel
                                {
                                    HotelId = 1,
                                    Name = "Hotel A",
                                    Address = "123 Main Street",
                                    City = "City A",
                                    Country = "Country A",
                                    Phone = "1234567890",
                                    Email = "hotelA@example.com"
                                },
                                new Hotel
                                {
                                    HotelId = 2,
                                    Name = "Hotel B",
                                    Address = "456 Elm Street",
                                    City = "City B",
                                    Country = "Country B",
                                    Phone = "9876543210",
                                    Email = "hotelB@example.com"
                                },
                                new Hotel
                                {
                                    HotelId = 3,
                                    Name = "Hotel C",
                                    Address = "789 Oak Avenue",
                                    City = "City C",
                                    Country = "Country C",
                                    Phone = "4561237890",
                                    Email = "hotelC@example.com"
                                },
                                new Hotel
                                {
                                    HotelId = 4,
                                    Name = "Hotel D",
                                    Address = "321 Pine Road",
                                    City = "City D",
                                    Country = "Country D",
                                    Phone = "7896541230",
                                    Email = "hotelD@example.com"
                                }
                                );
            modelBuilder.Entity<Room>().HasData(
                                new Room
                                {
                                    RoomId = 1,
                                    HotelId = 1,
                                    RoomNumber = 101,
                                    Capacity = 2,
                                    Price = 100.0,
                                    AC = true
                                },
                                new Room
                                {
                                    RoomId = 2,
                                    HotelId = 1,
                                    RoomNumber = 102,
                                    Capacity = 4,
                                    Price = 150.0,
                                    AC = true
                                },
                                new Room
                                {
                                    RoomId = 3,
                                    HotelId = 2,
                                    RoomNumber = 201,
                                    Capacity = 3,
                                    Price = 120.0,
                                    AC = false
                                },
                                new Room
                                {
                                    RoomId = 4,
                                    HotelId = 2,
                                    RoomNumber = 202,
                                    Capacity = 2,
                                    Price = 90.0,
                                    AC = true
                                },
                                new Room
                                {
                                    RoomId = 5,
                                    HotelId = 3,
                                    RoomNumber = 301,
                                    Capacity = 5,
                                    Price = 200.0,
                                    AC = true
                                }
                                );
            modelBuilder.Entity<Amenity>().HasData(
                               new Amenity
                               {
                                   AmentityId = 1,
                                   Name = "Swimming Pool",
                                   Description = "A large swimming pool for guests to enjoy."
                               },
                                new Amenity
                                {
                                    AmentityId = 2,
                                    Name = "Fitness Center",
                                    Description = "A fully equipped fitness center with modern equipment."
                                },
                                new Amenity
                                {
                                    AmentityId = 3,
                                    Name = "Restaurant",
                                    Description = "An on-site restaurant offering delicious meals."
                                },
                                new Amenity
                                {
                                    AmentityId = 4,
                                    Name = "Spa",
                                    Description = "A relaxing spa offering various treatments."
                                }
                                );
                                      
                                                                                                                                        
            modelBuilder.Entity<HotelAmenity>().HasData(
                                 new HotelAmenity
                                 {
                                     HotelAmentityId = 1,
                                     HotelId = 1,
                                     AmentityId = 1
                                 },
                                new HotelAmenity
                                {
                                    HotelAmentityId = 2,
                                    HotelId = 1,
                                    AmentityId = 2
                                },
                                new HotelAmenity
                                {
                                    HotelAmentityId = 3,
                                    HotelId = 2,
                                    AmentityId = 3
                                },
                                new HotelAmenity
                                {
                                    HotelAmentityId = 4,
                                    HotelId = 2,
                                    AmentityId = 1
                                },
                                new HotelAmenity
                                {
                                    HotelAmentityId = 5,
                                    HotelId = 3,
                                    AmentityId = 2
                                }
                );
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<HotelAmenity> HotelAmenities { get; set; }

    }
}
