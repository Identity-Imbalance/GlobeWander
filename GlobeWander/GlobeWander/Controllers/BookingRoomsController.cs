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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GlobeWander.Controllers
{
/// <summary>
/// API controller for managing booking rooms.
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRoomsController : ControllerBase
    {
        private readonly IBookingRoom _context;

  

        public BookingRoomsController(IBookingRoom context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all booking rooms.
        /// </summary>
        // GET: api/BookingRooms
        [HttpGet]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<IEnumerable<BookingRoomDTO>>> GetAllBookingRooms()
        {
            var bookingRoomDTOs = await _context.GetAllBookingRooms();
            return bookingRoomDTOs;
        }

        /// <summary>
        /// Get a specific booking room by its ID.
        /// </summary>
        /// <param name="id">The ID of the booking room.</param>
        // GET: api/BookingRooms/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]

        public async Task<ActionResult<BookingRoomDTO>> GetBookingRoom(int id)
        {

            var bookingRoomDTO = await _context.GetBookingRoomById(id);


            if (bookingRoomDTO == null)
            {
                return NotFound();
            }

            return bookingRoomDTO;
        }

        /// <summary>
        /// Update a booking room.
        /// </summary>
        /// <param name="id">The ID of the booking room.</param>
        /// <param name="bookingRoomDTO">The updated booking room data.</param>
        // PUT: api/BookingRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager,User")]
      
        public async Task<IActionResult> PutBookingRoom(int id, DurationBookingRoomDTO bookingRoomDTO)
        {

            await _context.UpdateBookingRoom(id, bookingRoomDTO);

            return NoContent();
        }


        /// <summary>
        /// Create a new booking room.
        /// </summary>
        /// <param name="bookingRoomDTO">The booking room data to create.</param>

        // POST: api/BookingRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<BookingRoomDTO>> PostBookingRoom(NewBookingRoomDTO bookingRoomDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var x = await _context.CreateBookingRoom(bookingRoomDTO,userId);

            if (x != null)

                return CreatedAtAction(nameof(GetBookingRoom), new { id = x.ID, tripId = x.HotelID, RoomNumber = x.RoomNumber, userName = x.Username }, x);

            return NoContent();

        }


        /// <summary>
        /// Delete a booking room by its ID.
        /// </summary>
        /// <param name="id">The ID of the booking room to delete.</param>

        // DELETE: api/BookingRooms/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager,User")]
        public async Task<IActionResult> DeleteBookingRoom(int id)
        {
            await _context.DeleteBookingRoom(id);

            return NoContent();
        }


    }
}
