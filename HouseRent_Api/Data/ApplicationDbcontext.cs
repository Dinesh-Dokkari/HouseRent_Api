using HouseRent_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRent_Api.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options)
            : base(options)
        {
            
        }

        public DbSet<LocalUser> LocalUsers { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<HouseNumber> HouseNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<House>().HasData(
            //    new House
            //    {
            //        Id = 1,
            //        Amenity = "",
            //        CreatedDate = new DateTime(2022, 7, 14, 11, 1, 43, 354, DateTimeKind.Local).AddTicks(4886),
            //        Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
            //        ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
            //        Name = "Royal Villa",
            //        Occupancy = 4,
            //        Rate = 200.0,
            //        Sqft = 550,
            //        UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            //    },
            //    new House
            //            {
            //                Id = 2,
            //                Amenity = "",
            //                CreatedDate = new DateTime(2022, 7, 14, 11, 1, 43, 354, DateTimeKind.Local).AddTicks(4934),
            //                Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
            //                ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
            //                Name = "Premium Pool Villa",
            //                Occupancy = 4,
            //                Rate = 300.0,
            //                Sqft = 550,
            //                UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            //            },
            //    new House
            //            {
            //                Id = 3,
            //                Amenity = "",
            //                CreatedDate = new DateTime(2022, 7, 14, 11, 1, 43, 354, DateTimeKind.Local).AddTicks(4936),
            //                Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
            //                ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa4.jpg",
            //                Name = "Luxury Pool Villa",
            //                Occupancy = 4,
            //                Rate = 400.0,
            //                Sqft = 750,
            //                UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            //            },
            //    new House
            //            {
            //                Id = 4,
            //                Amenity = "",
            //                CreatedDate = new DateTime(2022, 7, 14, 11, 1, 43, 354, DateTimeKind.Local).AddTicks(4939),
            //                Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
            //                ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa5.jpg",
            //                Name = "Diamond Villa",
            //                Occupancy = 4,
            //                Rate = 550.0,
            //                Sqft = 900,
            //                UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            //            },
            //    new House
            //            {
            //                Id = 5,
            //                Amenity = "",
            //                CreatedDate = new DateTime(2022, 7, 14, 11, 1, 43, 354, DateTimeKind.Local).AddTicks(4941),
            //                Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
            //                ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa2.jpg",
            //                Name = "Diamond Pool Villa",
            //                Occupancy = 4,
            //                Rate = 600.0,
            //                Sqft = 1100,
            //                UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            //            }
              
        }

    }
}
