using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketClientApplication.Models
{
    public class Booking
    {
        [Key]
        public int Booking_Id { get; set; }
        public int User_Id { get; set; }
        public int Ticket_Id { get; set; }
        public string Destination { get; set; }

        public double Cost { get; set; }
    }
}
