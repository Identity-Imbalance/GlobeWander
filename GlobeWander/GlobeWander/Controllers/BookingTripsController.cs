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
        [Authorize(Roles = "Admin Manager")]
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingTripDTO>>> GetbookingTrips()
        {
            var bookingTrip = await _bookTrip.GetAllBookingTrips();
            return Ok(bookingTrip);
        }

        // GET: api/BookingTrips/5
        [HttpGet("{id}/{tripId}")]
        public async Task<ActionResult<BookingTripDTO>> GetBookingTrip(int id, int tripId)
        {
            return await _bookTrip.GetBookingTripById(id, tripId);
        }

        // PUT: api/BookingTrips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{tripId}")]
        public async Task<IActionResult> PutBookingTrip(int id, UpdateBookingTripDTO bookingTrip, int tripId)
        {

            var updatebookingTrip = await _bookTrip.UpdateBookingTrip(id, bookingTrip, tripId);

            return Ok(updatebookingTrip);
        }

        // POST: api/BookingTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<BookingTripDTO>> PostBookingTrip(NewBookingTripDTO bookingTrip)
        //{

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    return await _bookTrip.Create(bookingTrip,User);

        //}
        [HttpPost]
        public async Task<ActionResult<BookingTripDTO>> PostBookingTrip(NewBookingTripDTO bookingTrip)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createdBookingTrip = await _bookTrip.Create(bookingTrip, User);

            return CreatedAtAction(nameof(GetBookingTrip), new { id = createdBookingTrip.ID, tripId = createdBookingTrip.TripID }, createdBookingTrip);
        }

        // DELETE: api/BookingTrips/5
        [HttpDelete("{id}/{tripId}")]
        public async Task<IActionResult> DeleteBookingTrip(int id, int tripId)
        {
            if (_bookTrip == null)
            {
                return NotFound();
            }

            await _bookTrip.Delete(id, tripId);
            return NoContent();
        }
    }
}
