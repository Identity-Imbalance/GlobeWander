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
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {

        private readonly IHotel _hotel;
        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> Get()
        {
            return await _hotel.GetAllHotels();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            return await _hotel.GetHotelId(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<HotelDTO>> PostHotel(HotelDTO hotel)
        {
            return await _hotel.CreateHotel(hotel);
        }


        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            return Ok(await _hotel.UpdateHotel(id, hotel));
        }

        // POST: api/Hotels


        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotel.GetHotelId(id);

            if (id != hotel.Id)
            {
                return BadRequest();
            }

            return Ok(await _hotel.DeleteHotel(id));
        }



    }
}
