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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GlobeWander.Controllers
{/// <summary>
/// API controller for managing booking trips.
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookingTripsController : ControllerBase
    {
        private readonly IBookingTrip _bookTrip;

        public BookingTripsController(IBookingTrip booktrip)
        {
            _bookTrip = booktrip;
        }

        /// <summary>
        /// Get a list of all booking trips.
        /// </summary>
        // GET: api/BookingTrips

        [HttpGet]
        [Authorize(Roles = "Admin Manager,Trip Manager")]
        public async Task<ActionResult<IEnumerable<BookingTripDTO>>> GetbookingTrips()
        {
            var bookingTrip = await _bookTrip.GetAllBookingTrips();
            return Ok(bookingTrip);
        }

        /// <summary>
        /// Get a specific booking trip by its ID and trip ID.
        /// </summary>
        /// <param name="id">The ID of the booking trip.</param>
        /// <param name="tripId">The ID of the trip associated with the booking.</param>
        // GET: api/BookingTrips/5
        [HttpGet("{id}")]
        [HttpGet("{id}/{tripId}")]
        [Authorize(Roles = "Admin Manager,Trip Manager")]

        public async Task<ActionResult<BookingTripDTO>> GetBookingTrip(int id)
        {
            return await _bookTrip.GetBookingTripById(id);
        }

        /// <summary>
        /// Update a booking trip.
        /// </summary>
        /// <param name="id">The ID of the booking trip.</param>
        /// <param name="bookingTrip">The updated booking trip data.</param>
        /// <param name="tripId">The ID of the trip associated with the booking.</param>
        // PUT: api/BookingTrips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{tripId}")]
        [Authorize(Roles = "Admin Manager,Trip Manager,User")]
        public async Task<IActionResult> PutBookingTrip(int id, UpdateBookingTripDTO bookingTrip, int tripId)
        {

            var updatebookingTrip = await _bookTrip.UpdateBookingTrip(id, bookingTrip, tripId);

            return Ok(updatebookingTrip);
        }

        /// <summary>
        /// Create a new booking trip.
        /// </summary>
        /// <param name="bookingTrip">The booking trip data to create.</param>
        // POST: api/BookingTrips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<BookingTripDTO>> PostBookingTrip(NewBookingTripDTO bookingTrip)
        {


           var createdBookingTrip =  await _bookTrip.Create(bookingTrip,User);
           
           return CreatedAtAction(nameof(GetBookingTrip), new { id = createdBookingTrip.ID, tripId = createdBookingTrip.TripID, TotalPrice = createdBookingTrip.TotalPrice, CostPerPerson = createdBookingTrip.CostPerPerson ,createdBookingTrip.Duration, userName = createdBookingTrip.Username }, createdBookingTrip);

        }

        /// <summary>
        /// Delete a booking trip by its ID and trip ID.
        /// </summary>
        /// <param name="id">The ID of the booking trip to delete.</param>
        /// <param name="tripId">The ID of the trip associated with the booking.</param>
        // DELETE: api/BookingTrips/5
        [HttpDelete("{id}/{tripId}")]
        [Authorize(Roles = "Admin Manager,Trip Manager,User")]
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
