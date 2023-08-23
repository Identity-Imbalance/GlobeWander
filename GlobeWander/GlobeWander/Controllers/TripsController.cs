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
using Microsoft.AspNetCore.Authorization;

namespace GlobeWander.Controllers
{/// <summary>
/// API controller for managing trips.
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITrip _context;

        public TripsController(ITrip context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all trips.
        /// </summary>
        // GET: api/Trips
        [HttpGet]
        [Authorize(Roles = "Admin Manager,Trip Manager")]
        public async Task<ActionResult<IEnumerable<TripDTO>>> GetTrips()
        {
          if (_context == null)
          {
              return NotFound();
          }
            return await _context.GetAllTrips();
        }

        /// <summary>
        /// Get a specific trip by its ID.
        /// </summary>
        /// <param name="id">The ID of the trip.</param>
        // GET: api/Trips/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin Manager,Trip Manager")]
        public async Task<ActionResult<TripDTO>> GetTrip(int id)
        {
          if (_context == null)
          {
              return NotFound();
          }
            var trip = await _context.GetTripByID(id);

            if (trip == null)
            {
                return NotFound();
            }

            return trip;
        }

        /// <summary>
        /// Update a trip.
        /// </summary>
        /// <param name="id">The ID of the trip.</param>
        /// <param name="trip">The updated trip data.</param>
        // PUT: api/Trips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin Manager,Trip Manager")]
        public async Task<IActionResult> PutTrip(int id, UpdateTripDTO trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

           var updatedTrip =  await _context.UpdateTrip(trip, id);

            return Ok(updatedTrip);
        }

        /// <summary>
        /// Create a new trip.
        /// </summary>
        /// <param name="trip">The trip data to create.</param>
        // POST: api/Trips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin Manager,Trip Manager")]
        public async Task<ActionResult<TripDTO>> PostTrip(NewTripDTO trip)
        {
          if (_context == null)
          {
              return Problem("Entity set 'GlobeWanderDbContext.Trips'  is null.");
          }
           var newTripDTO =  await _context.CreateTrip(trip);

            return CreatedAtAction("GetTrip", new { id = newTripDTO.Id }, trip);
        }

        /// <summary>
        /// Delete a trip by its ID.
        /// </summary>
        /// <param name="id">The ID of the trip to delete.</param>
        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin Manager,Trip Manager")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            await _context.DeleteTrip(id);

            return NoContent();
        }

        
    }
}
