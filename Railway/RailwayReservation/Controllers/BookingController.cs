using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailwayReservation.Repository;

namespace RailwayReservation.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingController));
        private readonly IBookingRepository bookRepo;
        public BookingController(IBookingRepository bookingRepository)
        {
            this.bookRepo = bookingRepository;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                _log4net.Info("Get BookingDetails by Id accessed");
                var bookinglist = bookRepo.GetById(id);
                return new OkObjectResult(bookinglist);

            }
            catch
            {
                _log4net.Error("Error in getting Booking Details");
                return new NoContentResult();
            }
        }

        [HttpPost]
        public IActionResult PostBookVehicle(Booking model)
        {
            try
            {
                _log4net.Info("Book Details Getting Added");
                if (ModelState.IsValid)
                {
                    bookRepo.book(model);
                    return CreatedAtAction(nameof(PostBookVehicle), new { id = model.Booking_Id }, model);

                }
                return BadRequest();
            }
            catch
            {
                _log4net.Error("Error in Adding Booking Details");
                return new NoContentResult();

            }
        }
    }
}
