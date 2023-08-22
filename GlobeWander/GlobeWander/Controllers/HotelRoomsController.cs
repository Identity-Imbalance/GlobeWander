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
/// API controller for managing hotel rooms.
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {

        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom Hotelroom)
        {
            _hotelRoom = Hotelroom;

        }

        /// <summary>
        /// Get a list of all hotel rooms.
        /// </summary>
        // GET: api/HotelRooms
        [HttpGet]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
            return await _hotelRoom.GetHotelRooms();
        }

        /// <summary>
        /// Get a specific hotel room by hotel ID and room number.
        /// </summary>
        /// <param name="hotelID">The ID of the hotel.</param>
        /// <param name="roomNumber">The room number.</param>
        // GET: api/HotelRooms/5
        [HttpGet("Hotels/{hotelID}/Rooms/{roomNumber}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelID, int roomNumber)
        {
            return await _hotelRoom.GetHotelRoomId(hotelID, roomNumber);

        }

        /// <summary>
        /// Create a new hotel room.
        /// </summary>
        /// <param name="hotelRoom">The hotel room data to create.</param>
        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(hotelroomDTOcreate hotelRoom)
        {
            return await _hotelRoom.CreateHotelRoom(hotelRoom);

        }

        /// <summary>
        /// Update a hotel room.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="roomNumber">The room number.</param>
        /// <param name="hotelRoom">The updated hotel room data.</param>
        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Hotel/{hotelId}/Room/{roomNumber}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<IActionResult> PutHotelRoom(int hotelId,int roomNumber, hotelroomDTOcreate hotelRoom)
        {
            var ret = await _hotelRoom.UpdateHotelRoom(hotelId, roomNumber, hotelRoom);
            return Ok(ret);
        }

        /// <summary>
        /// Delete a hotel room by hotel ID and room number.
        /// </summary>
        /// <param name="hotelID">The ID of the hotel.</param>
        /// <param name="roomNumber">The room number.</param>
        // DELETE: api/HotelRooms/5
        [HttpDelete("Hotels/{hotelID}/Rooms/{roomNumber}")]
        [Authorize(Roles = "Admin Manager,Hotel Manager")]
        public async Task<HotelRoom> DeleteHotelRoom(int hotelID,int roomNumber)
        {

            if (_hotelRoom == null)
            {
                return null;
            }
             return await _hotelRoom.DeleteHotelRoom(hotelID, roomNumber);
            

        }

    }
}
