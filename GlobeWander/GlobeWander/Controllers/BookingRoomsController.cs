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
    public class BookingRoomsController : ControllerBase
    {
        private readonly GlobeWanderDbContext _context;

        public BookingRoomsController(GlobeWanderDbContext context)
        {
            _context = context;
        }

        // GET: api/BookingRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingRoom>>> GetBookingRooms()
        {
          if (_context.BookingRooms == null)
          {
              return NotFound();
          }
            return await _context.BookingRooms.ToListAsync();
        }

        // GET: api/BookingRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingRoom>> GetBookingRoom(int id)
        {
          if (_context.BookingRooms == null)
          {
              return NotFound();
          }
            var bookingRoom = await _context.BookingRooms.FindAsync(id);

            if (bookingRoom == null)
            {
                return NotFound();
            }

            return bookingRoom;
        }

        // PUT: api/BookingRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingRoom(int id, BookingRoom bookingRoom)
        {
            if (id != bookingRoom.ID)
            {
                return BadRequest();
            }

            _context.Entry(bookingRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingRoomExists(id))
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

        // POST: api/BookingRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingRoom>> PostBookingRoom(BookingRoom bookingRoom)
        {
          if (_context.BookingRooms == null)
          {
              return Problem("Entity set 'GlobeWanderDbContext.BookingRooms'  is null.");
          }
            _context.BookingRooms.Add(bookingRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingRoom", new { id = bookingRoom.ID }, bookingRoom);
        }

        // DELETE: api/BookingRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingRoom(int id)
        {
            if (_context.BookingRooms == null)
            {
                return NotFound();
            }
            var bookingRoom = await _context.BookingRooms.FindAsync(id);
            if (bookingRoom == null)
            {
                return NotFound();
            }

            _context.BookingRooms.Remove(bookingRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingRoomExists(int id)
        {
            return (_context.BookingRooms?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
