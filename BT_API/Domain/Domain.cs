using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Customer
    {
        [Key]
        [StringLength(12)]
        public string CMND { get; set; }
        [Required]
        public string fullName { get; set; }
        public DateTime DOB { get; set; }        
    }

    public class Room
    {
        [Key]
        public string RoomID { get; set; }
        public bool isEnable { get; set; }
        [Required]
        public int capacity { get; set; }
    }

    public class Booking
    {
        [Key]
        public string BookingID { get; set; }
        public string CMND { get; set; }
        public string RoomID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
