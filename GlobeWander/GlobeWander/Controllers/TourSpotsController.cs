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
{
    /// <summary>
    /// API controller for managing tour spots.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TourSpotsController : ControllerBase
    {
        private readonly ITourSpot _context;

        public TourSpotsController(ITourSpot context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of all tour spots.
        /// </summary>
        // GET: api/TourSpots
        [HttpGet]
        [Authorize(Roles = "Admin Manager")]
        public async Task<ActionResult<IEnumerable<TourSpotDTO>>> GetTourSpots()
        {
            var tourSpots = await _context.GetAllTourSpots();
            return Ok(tourSpots);
        }

        /// <summary>
        /// get trend tours
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpGet("Trends")]
        public async Task<ActionResult<IEnumerable<TrendTourSpotDTO>>> GetTrendToursSpots()
        {
            var tourSpots = await _context.GetMostVisitedTourSpots();
            return Ok(tourSpots);
        }

        /// <summary>
        /// Get a specific tour spot by its ID.
        /// </summary>
        /// <param name="id">The ID of the tour spot.</param>
        // GET: api/TourSpots/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin Manager")]

        public async Task<ActionResult<TourSpotDTO>> GetTourSpot(int id)
        {
          if (_context == null)
          {
              return NotFound();
          }
            var tourSpot = await _context.GetSpotById(id);

            if (tourSpot == null)
            {
                return NotFound();
            }

            return tourSpot;
        }

        /// <summary>
        /// Update a tour spot.
        /// </summary>
        /// <param name="id">The ID of the tour spot.</param>
        /// <param name="tourSpot">The updated tour spot data.</param>
        // PUT: api/TourSpots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin Manager")]

        public async Task<IActionResult> PutTourSpot(int id, newTourSpotDTO tourSpot)
        {
            //if (id != tourSpot.ID)
            //{
            //    return BadRequest();
            //}

            var updatedTourSpot = await _context.UpdateTourSpot(tourSpot, id);

            return Ok(updatedTourSpot);
        }

        /// <summary>
        /// Create a new tour spot.
        /// </summary>
        /// <param name="tourSpot">The tour spot data to create.</param>
        // POST: api/TourSpots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin Manager")]

        public async Task<ActionResult<TourSpot>> PostTourSpot(newTourSpotDTO tourSpot)
        {
          if (_context == null)
          {
              return Problem("Entity set 'GlobeWanderDbContext.TourSpots'  is null.");
          }
            var newTourSpot = await _context.CreateTourSpot(tourSpot);

            return CreatedAtAction("GetTourSpot", new { id = newTourSpot.ID }, tourSpot);
        }

        /// <summary>
        /// Delete a tour spot by its ID.
        /// </summary>
        /// <param name="id">The ID of the tour spot to delete.</param>
        // DELETE: api/TourSpots/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin Manager")]

        public async Task<IActionResult> DeleteTourSpot(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            await _context.DeleteTourSpot(id);
            

            return NoContent();
        }

    }
}
