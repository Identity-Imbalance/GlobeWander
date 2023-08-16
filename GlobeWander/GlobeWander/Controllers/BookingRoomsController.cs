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
    public class BookingRoomsController : ControllerBase
    {
        private readonly IBookingRoom _context;

        public BookingRoomsController(IBookingRoom context)
        {
            _context = context;
        }

        // GET: api/BookingRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingRoom>>> GetBookingRooms()
        {
          if (_context == null)
          {
              return NotFound();
          }
            return await _context.GetAllBookingRooms();
        }

        // GET: api/BookingRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingRoom>> GetBookingRoom(int id)
        {
          if (_context == null)
          {
              return NotFound();
          }
            var bookingRoom = await _context.GetBookingRoomById(id);

            if (bookingRoom == null)
            {
                return NotFound();
            }

            return bookingRoom;
        }

        // PUT: api/BookingRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("/api/hotelroom/")]
        public async Task<IActionResult> PutBookingRoom(int id, BookingRoom bookingRoom)
        {
            if (id != bookingRoom.ID)
            {
                return BadRequest();
            }

            await _context.UpdateBookingRoom(id, bookingRoom);

            return NoContent();
        }

        // POST: api/BookingRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingRoom>> PostBookingRoom(BookingRoom bookingRoom)
        {
          if (_context == null)
          {
              return Problem("Entity set 'GlobeWanderDbContext.BookingRooms'  is null.");
          }
            await _context.CreateBookingRoom(bookingRoom);

            return CreatedAtAction("GetBookingRoom", new { id = bookingRoom.ID }, bookingRoom);
        }

        // DELETE: api/BookingRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingRoom(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var bookingRoom =  _context.DeleteBookingRoom(id);
            if (bookingRoom == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
