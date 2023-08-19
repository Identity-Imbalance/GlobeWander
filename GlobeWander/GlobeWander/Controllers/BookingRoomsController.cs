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
using GlobeWander.Models.DTO;

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
        public async Task<ActionResult<IEnumerable<BookingRoomDTO>>> GetAllBookingRooms()
        {
            var bookingRoomDTOs = await _context.GetAllBookingRooms();
            return bookingRoomDTOs;
        }

        // GET: api/BookingRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingRoomDTO>> GetBookingRoom(int id)
        {
            var bookingRoomDTO = await _context.GetBookingRoomById(id);

            if (bookingRoomDTO == null)
            {
                return NotFound();
            }

            return bookingRoomDTO;
        }

        // PUT: api/BookingRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingRoom(int id, DurationBookingRoomDTO bookingRoomDTO)
        {

            await _context.UpdateBookingRoom(id, bookingRoomDTO);

            return NoContent();
        }

        // POST: api/BookingRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingRoomDTO>> PostBookingRoom(NewBookingRoomDTO bookingRoomDTO)
        {
            var x = await _context.CreateBookingRoom(bookingRoomDTO);

            return CreatedAtAction("GetBookingRoom", new { id = x.ID }, x);
        }

        // DELETE: api/BookingRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingRoom(int id)
        {
            await _context.DeleteBookingRoom(id);

            return NoContent();
        }


    }
}
