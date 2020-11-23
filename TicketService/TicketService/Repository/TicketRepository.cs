using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TicketService.Models;
using TicketService.Controllers;

namespace TicketService.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext dbContext;
        
        public TicketRepository(TicketDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public IEnumerable<Ticket> GetAll()
        {
            var booklist = dbContext.Tickets.ToList();
            return booklist;
        }
        public Ticket GetById(int ticket_id)
        {
            return dbContext.Tickets.FirstOrDefault(t => t.Ticket_Id == ticket_id);
           // dbContext.Tickets.Where(x => x.Status == 1).ToListAsync();
        }
    }
}
