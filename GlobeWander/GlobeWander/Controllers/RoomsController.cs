﻿using System;
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
using Microsoft.AspNetCore.Authorization;

namespace GlobeWander.Controllers
{/// <summary>
/// API controller for managing rooms.
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _context;

        public RoomsController(IRoom context)
        {
            _context = context;
        }


        /// <summary>
        /// Get a list of all rooms.
        /// </summary>
        // GET: api/Rooms
        [HttpGet]
        [Authorize(Roles = "Admin Manager,Hotel Manager, User")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            if (_context == null)
            {
                return NotFound();
            }
            return await _context.GetRooms();
        }

        /// <summary>
        /// Get a specific room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room.</param>
        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
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

        /// <summary>
        /// Update a room.
        /// </summary>
        /// <param name="id">The ID of the room.</param>
        /// <param name="room">The updated room data.</param>
        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager, User")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO room)
        {
           var updateRoom = await _context.UpdateRoom(id, room);    
          
            await _context.UpdateRoom(id, room);
            return Ok(updateRoom);

        }

        /// <summary>
        /// Create a new room.
        /// </summary>
        /// <param name="room">The room data to create.</param>
        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO room)
        {
            
            if (_context == null)
            {
                return Problem("Entity set 'GlobeWanderDbContext.Rooms'  is null.");
            }
            RoomDTO room1 = await _context.CreateRoom(room);


            return CreatedAtAction("GetRoom", new { id = room1.ID }, room);
        }

        /// <summary>
        /// Delete a room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>
        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
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
