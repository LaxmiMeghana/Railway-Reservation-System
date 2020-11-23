using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.Models
{
    public class Ticket
    {
        [Key]

        public int Ticket_Id { get; set; }

        public double Cost { get; set; }

        public string Destination { get; set; }

        public int Status { get; set; }
    }
}
