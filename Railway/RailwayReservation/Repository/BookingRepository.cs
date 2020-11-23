using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RailwayReservation.Models;
using RailwayReservation.Controllers;

namespace RailwayReservation.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext dbContext;
        public  BookingRepository(BookingDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        public Booking book(Booking model)
        {
            var result = dbContext.Bookings.Add(model);
            dbContext.SaveChanges();
            return model;
        }
            


        public IEnumerable<Booking> GetById(int user_id)
        {
            return dbContext.Bookings.Where(b => b.User_Id == user_id).ToList();
        }
    }
}
