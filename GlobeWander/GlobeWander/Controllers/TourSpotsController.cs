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
    public class TourSpotsController : ControllerBase
    {
        private readonly ITourSpot _context;

        public TourSpotsController(ITourSpot context)
        {
            _context = context;
        }

        // GET: api/TourSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourSpot>>> GetTourSpots()
        {
          if (_context == null)
          {
              return NotFound();
          }
            return await _context.GetAllTourSpots();
        }

        // GET: api/TourSpots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourSpot>> GetTourSpot(int id)
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

        // PUT: api/TourSpots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTourSpot(int id, TourSpot tourSpot)
        {
            if (id != tourSpot.ID)
            {
                return BadRequest();
            }

            await _context.UpdateTourSpot(tourSpot, id);

            return NoContent();
        }

        // POST: api/TourSpots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TourSpot>> PostTourSpot(TourSpot tourSpot)
        {
          if (_context == null)
          {
              return Problem("Entity set 'GlobeWanderDbContext.TourSpots'  is null.");
          }
            await _context.CreateTourSpot(tourSpot);

            return CreatedAtAction("GetTourSpot", new { id = tourSpot.ID }, tourSpot);
        }

        // DELETE: api/TourSpots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourSpot(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var tourSpot = _context.DeleteTourSpot(id);
            if (tourSpot == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
