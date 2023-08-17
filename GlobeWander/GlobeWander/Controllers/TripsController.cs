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
    public class TripsController : ControllerBase
    {
        private readonly ITrip _context;

        public TripsController(ITrip context)
        {
            _context = context;
        }

        // GET: api/Trips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripDTO>>> GetTrips()
        {
          if (_context == null)
          {
              return NotFound();
          }
            return await _context.GetAllTrips();
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
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

        // PUT: api/Trips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(int id, NewTripDTO trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

           var updatedTrip =  await _context.UpdateTrip(trip, id);

            return Ok(updatedTrip);
        }

        // POST: api/Trips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TripDTO>> PostTrip(NewTripDTO trip)
        {
          if (_context == null)
          {
              return Problem("Entity set 'GlobeWanderDbContext.Trips'  is null.");
          }
           var newTripDTO =  await _context.CreateTrip(trip);

            return CreatedAtAction("GetTrip", new { id = newTripDTO.Id }, trip);
        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
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
