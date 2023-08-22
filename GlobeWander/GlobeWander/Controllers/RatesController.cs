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
/// API controller for managing rates.
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRate _rate;

        public RatesController(IRate rate)
        {
            _rate = rate;
        }

        /// <summary>
        /// Get a list of all rates.
        /// </summary>
        // GET: api/Rates
        [HttpGet]
        [Authorize(Roles = "Admin Manager,Trip Manager")]
        public async Task<ActionResult<IEnumerable<RateDTO>>> GetRates()
        {
            var rates = await _rate.GetAllRate();
            return Ok(rates);
        }

        /// <summary>
        /// Get a specific rate by its ID.
        /// </summary>
        /// <param name="id">The ID of the rate.</param>
        // GET: api/Rates/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin Manager,Trip Manager")]
        public async Task<ActionResult<RateDTO>> GetRate(int id)
        {
         var rate = await _rate.GetRateById(id);
           if(rate == null)
            {
                return NotFound();
            }
           return Ok(rate);
        }

        /// <summary>
        /// Get a rate by its associated trip ID.
        /// </summary>
        /// <param name="tripId">The ID of the associated trip.</param>
        [HttpGet]
        [Route("/api/trips/{tripId}/Rates")]
        [Authorize(Roles = "Admin Manager,Trip Manager,User")]
        public async Task<ActionResult<RateDTO>> GetRateByTripId(int tripId)
        {
            var rate = await _rate.GetRateByTripID(tripId);
            if (rate == null)
            {
                return NotFound();
            }
            return Ok(rate);
        }


        /// <summary>
        /// Update a rate.
        /// </summary>
        /// <param name="id">The ID of the rate.</param>
        /// <param name="TripID">The ID of the associated trip.</param>
        /// <param name="rateDTO">The updated rate data.</param>
        // PUT: api/Rates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{TripID}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PutRate(int id,int TripID, UpdateRateDTO rateDTO)
        {
          var updateRate =await _rate.UpdateRate(id,TripID, rateDTO);
          return Ok(updateRate);
        }

        /// <summary>
        /// Create a new rate.
        /// </summary>
        /// <param name="rateDTO">The rate data to create.</param>
        // POST: api/Rates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<RateDTO>> PostRate(NewRateDTO rateDTO)
        {
         var createRate = await _rate.Create(rateDTO,User);
            return Ok(createRate);
        }

        /// <summary>
        /// Delete a rate by its ID and associated trip ID.
        /// </summary>
        /// <param name="id">The ID of the rate to delete.</param>
        /// <param name="TripID">The ID of the associated trip.</param>
        // DELETE: api/Rates/5
        [HttpDelete("{id}/{TripID}")]
        [Authorize(Roles = "Admin Manager,Trip Manager,User")]
        public async Task<IActionResult> DeleteRate(int id ,int TripID)
        {
            var deleteRate = await _rate.DeleteRate(id, TripID);
            return NoContent();
        }

      
    }
}
