using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TicketService.Models;
using TicketService.Repository;

namespace TicketServiceTest
{
    public class Tests
    {
        List<Ticket> pg = new List<Ticket>();
        IQueryable<Ticket> pgdata;
        Mock<DbSet<Ticket>> mockSet;
        Mock<TicketDbContext> pgcontextmock;
        [SetUp]
        public void Setup()
        {
            pg = new List<Ticket>()
            {
                new Ticket{Ticket_Id = 1, Cost=1,Status=1},
                  new Ticket{Ticket_Id = 2, Cost=1,Status=1},
                    new Ticket{Ticket_Id = 3, Cost=1,Status=1},
                      new Ticket{Ticket_Id = 4, Cost=1,Status=1},

            };
            pgdata = pg.AsQueryable();
            mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(pgdata.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(pgdata.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(pgdata.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(pgdata.GetEnumerator());
            var p = new DbContextOptions<TicketDbContext>();
            pgcontextmock = new Mock<TicketDbContext>(p);
            pgcontextmock.Setup(x => x.Tickets).Returns(mockSet.Object);



        }


        [Test]
        public void GetAllTest()
        {
            var pgrepo = new TicketRepository(pgcontextmock.Object);
            var pglist = pgrepo.GetAll();
            Assert.AreEqual(4, pglist.Count());




        }
        [Test]
        public void GetByIdTest()
        {
            var pgrepo = new TicketRepository(pgcontextmock.Object);
            var pgobj = pgrepo.GetById(2);
            Assert.IsNotNull(pgobj);
        }
        [Test]
        public void GetByIdTestFail()
        {
            var pgrepo = new TicketRepository(pgcontextmock.Object);
            var pgobj = pgrepo.GetById(88);
            Assert.IsNull(pgobj);
        }

    }

}

