using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public class HotelDBContext : DbContext
    {
        public HotelDBContext(): base("HotelConnectionString")
        {

        }

        public DbSet<Room> Rooms { get; set; }        
        public DbSet<Booking> Bookings { get; set; }
        
    }
}
