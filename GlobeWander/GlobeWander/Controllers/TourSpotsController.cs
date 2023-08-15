using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobeWander.Data;
using GlobeWander.Models;

namespace GlobeWander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourSpotsController : ControllerBase
    {
        private readonly GlobeWanderDbContext _context;

        public TourSpotsController(GlobeWanderDbContext context)
        {
            _context = context;
        }

        // GET: api/TourSpots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourSpot>>> GetTourSpots()
        {
          if (_context.TourSpots == null)
          {
              return NotFound();
          }
            return await _context.TourSpots.ToListAsync();
        }

        // GET: api/TourSpots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourSpot>> GetTourSpot(int id)
        {
          if (_context.TourSpots == null)
          {
              return NotFound();
          }
            var tourSpot = await _context.TourSpots.FindAsync(id);

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

            _context.Entry(tourSpot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourSpotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TourSpots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TourSpot>> PostTourSpot(TourSpot tourSpot)
        {
          if (_context.TourSpots == null)
          {
              return Problem("Entity set 'GlobeWanderDbContext.TourSpots'  is null.");
          }
            _context.TourSpots.Add(tourSpot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTourSpot", new { id = tourSpot.ID }, tourSpot);
        }

        // DELETE: api/TourSpots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTourSpot(int id)
        {
            if (_context.TourSpots == null)
            {
                return NotFound();
            }
            var tourSpot = await _context.TourSpots.FindAsync(id);
            if (tourSpot == null)
            {
                return NotFound();
            }

            _context.TourSpots.Remove(tourSpot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourSpotExists(int id)
        {
            return (_context.TourSpots?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
