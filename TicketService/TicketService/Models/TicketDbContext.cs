using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.Models
{
    public class TicketDbContext :DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Ticket> Tickets { get; set; }
    }
}
