using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobeWander.Data;
using GlobeWander.Models;

namespace GlobeWander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingTripsController : ControllerBase
    {
        private readonly GlobeWanderDbContext _context;

        public BookingTripsController(GlobeWanderDbContext context)
        {
            _context = context;
        }

        // GET: api/BookingTrips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingTrip>>> GetbookingTrips()
        {
          if (_context.bookingTrips == null)
          {
              return NotFound();
          }
            return await _context.bookingTrips.ToListAsync();
        }

        // GET: api/BookingTrips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingTrip>> GetBookingTrip(int id)
        {
          if (_context.bookingTrips == null)
          {
              return NotFound();
          }
            var bookingTrip = await _context.bookingTrips.FindAsync(id);

            if (bookingTrip == null)
            {
                return NotFound();
            }

            return bookingTrip;
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

            _context.Entry(bookingTrip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingTripExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookingTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingTrip>> PostBookingTrip(BookingTrip bookingTrip)
        {
          if (_context.bookingTrips == null)
          {
              return Problem("Entity set 'GlobeWanderDbContext.bookingTrips'  is null.");
          }
            _context.bookingTrips.Add(bookingTrip);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingTripExists(bookingTrip.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookingTrip", new { id = bookingTrip.ID }, bookingTrip);
        }

        // DELETE: api/BookingTrips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingTrip(int id)
        {
            if (_context.bookingTrips == null)
            {
                return NotFound();
            }
            var bookingTrip = await _context.bookingTrips.FindAsync(id);
            if (bookingTrip == null)
            {
                return NotFound();
            }

            _context.bookingTrips.Remove(bookingTrip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingTripExists(int id)
        {
            return (_context.bookingTrips?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
