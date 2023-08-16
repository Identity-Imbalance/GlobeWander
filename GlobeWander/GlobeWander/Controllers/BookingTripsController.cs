using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobeWander.Data;
using GlobeWander.Models;
using GlobeWander.Models.Interfaces;

namespace GlobeWander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingTripsController : ControllerBase
    {
        private readonly IBookingTrip _bookTrip;

        public BookingTripsController(IBookingTrip booktrip)
        {
            _bookTrip = booktrip;
        }

        // GET: api/BookingTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingTrip>>> GetbookingTrips()
        {
            return await _bookTrip.GetAllBookingTrips();
        }

        // GET: api/BookingTrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingTrip>> GetBookingTrip(int id)
        {
            return await _bookTrip.GetBookingTripById(id);
        }

        // PUT: api/BookingTrips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingTrip(int id, BookingTrip bookingTrip)
        {
            if (id != bookingTrip.ID)
            {
                return BadRequest();
            }

            return Ok(await _bookTrip.UpdateBookingTrip(id, bookingTrip));
        }

        // POST: api/BookingTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingTrip>> PostBookingTrip(BookingTrip bookingTrip)
        {
            return await _bookTrip.Create(bookingTrip);
        }

        // DELETE: api/BookingTrips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingTrip(int id)
        {
            var bookingTrip = await _bookTrip.GetBookingTripById(id);

            if (id != bookingTrip.ID)
            {
                return BadRequest();
            }

            return Ok(await _bookTrip.Delete(id));
        }
    }
}
