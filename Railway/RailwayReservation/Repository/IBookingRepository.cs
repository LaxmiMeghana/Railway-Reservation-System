using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RailwayReservation.Models;


namespace RailwayReservation.Repository
{
    public interface IBookingRepository
    {
        public IEnumerable<Booking> GetById(int Bookingid);

       // public Task<int> book(Booking entity);
        Booking book(Booking entity);
    }
}
