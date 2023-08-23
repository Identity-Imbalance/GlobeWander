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
/// API controller for managing hotels.
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {

        private readonly IHotel _hotel;
        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        /// <summary>
        /// Get a list of all hotels.
        /// </summary>
        // GET: api/Hotels
        [AllowAnonymous]
        [HttpGet]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> Get()
        {
                return await _hotel.GetAllHotels();
        }

        [AllowAnonymous]
        [HttpGet("AvailableHotels")]
        public async Task<ActionResult<IEnumerable<AnonymousHotelDTO>>> GetHotelsForAnonymous()
        {
            return await _hotel.AnonymousHotelDTOs();
        }

        /// <summary>
        /// Get a specific hotel by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel.</param>
        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            return await _hotel.GetHotelId(id);
        }


        /// <summary>
        /// Create a new hotel.
        /// </summary>
        /// <param name="hotel">The hotel data to create.</param>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // POST: api/Hotels
        [HttpPost]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<NewHotelDTO>> PostHotel(NewHotelDTO hotel)
        {
            return await _hotel.CreateHotel(hotel);
        }

        /// <summary>
        /// Update a hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel.</param>
        /// <param name="hotel">The updated hotel data.</param>
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


        /// <summary>
        /// Delete a hotel by its ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
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
