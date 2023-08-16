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
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _context;

        public RoomsController(IRoom context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]

        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            if (_context == null)
            {
                return NotFound();
            }
            return await _context.GetRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var room = await _context.GetRoomId(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.ID)
            {
                return BadRequest();
            }

            await _context.UpdateRoom(id, room);




            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(newRoomDTo room)
        {
            
            if (_context == null)
            {
                return Problem("Entity set 'GlobeWanderDbContext.Rooms'  is null.");
            }
            RoomDTO room1 = await _context.CreateRoom(room);


            return CreatedAtAction("GetRoom", new { id = room1.ID }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var room = await _context.DeleteRoom(id);
            if (room == null)
            {
                return NotFound();
            }



            return NoContent();
        }
    }
}
