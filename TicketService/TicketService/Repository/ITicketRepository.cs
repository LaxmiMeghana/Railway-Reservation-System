using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Models;

namespace TicketService.Repository
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();

        Ticket GetById(int ticket_id);
    }
}
