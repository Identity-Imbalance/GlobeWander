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
    public class RatesController : ControllerBase
    {
        private readonly IRate _rate;

        public RatesController(IRate rate)
        {
            _rate = rate;
        }

        // GET: api/Rates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateDTO>>> GetRates()
        {
            var rates = await _rate.GetAllRate();
            return Ok(rates);
        }

        // GET: api/Rates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RateDTO>> GetRate(int id)
        {
         var rate = await _rate.GetRateById(id);
           if(rate == null)
            {
                return NotFound();
            }
           return Ok(rate);
        }

        [HttpGet]
        [Route("/api/trips/{tripId}/Rates")]
        public async Task<ActionResult<RateDTO>> GetRateByTripId(int tripId)
        {
            var rate = await _rate.GetRateByTripID(tripId);
            if (rate == null)
            {
                return NotFound();
            }
            return Ok(rate);
        }

        // PUT: api/Rates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{TripID}")]
        public async Task<IActionResult> PutRate(int id,int TripID, UpdateRateDTO rateDTO)
        {
          var updateRate =await _rate.UpdateRate(id,TripID, rateDTO);
          return Ok(updateRate);
        }

        // POST: api/Rates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RateDTO>> PostRate(NewRateDTO rateDTO)
        {
         var createRate = await _rate.Create(rateDTO,User);
            return Ok(createRate);
        }

        // DELETE: api/Rates/5
        [HttpDelete("{id}/{TripID}")]
        public async Task<IActionResult> DeleteRate(int id ,int TripID)
        {
            var deleteRate = await _rate.DeleteRate(id, TripID);
            return NoContent();
        }

      
    }
}
