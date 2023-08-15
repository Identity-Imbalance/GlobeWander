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
    public class RatesController : ControllerBase
    {
        private readonly IRate _rate;

        public RatesController(IRate rate)
        {
            _rate = rate;
        }

        // GET: api/Rates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> GetRates()
        {
            return await _rate.GetAllRate();
        }

        // GET: api/Rates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rate>> GetRate(int id)
        {
         var rate = await _rate.GetRateById(id);
            return rate;
        }

        // PUT: api/Rates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRate(int id, Rate rate)
        {
            if (id != rate.ID)
            {
                return BadRequest();
            }

          var updateRate =await _rate.UpdateRate(id, rate);
            return Ok(updateRate);
        }

        // POST: api/Rates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rate>> PostRate(Rate rate)
        {
          return await _rate.Create(rate);
        }

        // DELETE: api/Rates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRate(int id)
        {
           await _rate.DeleteRate(id);
            return NoContent();
        }

      
    }
}
