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
    public class HotelRoomsController : ControllerBase
    {

        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom Hotelroom)
        {
            _hotelRoom = Hotelroom;

        }

        // GET: api/HotelRooms
        [HttpGet]

        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms()
        {
            return await _hotelRoom.GetHotelRooms();
        }

        // GET: api/HotelRooms/5
        [HttpGet("Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int roomNumber)
        {
            return await _hotelRoom.GetHotelRoomId(roomNumber);

        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
            return await _hotelRoom.CreateHotelRoom(hotelRoom);

        }


        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Hotel/{hotelId}/Room/{idRoom}")]
        public async Task<IActionResult> PutHotelRoom(int roomNumber, HotelRoom hotelRoom)
        {
            return Ok(await _hotelRoom.UpdateHotelRoom(roomNumber, hotelRoom));
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("Hotel/{hotelId}/Room/{idRoom}")]
        public async Task<HotelRoom> DeleteHotelRoom(int roomNumber)
        {
            // return Ok(await _hoteRoom.Delete(hotelId, idRoom));

            return await _hotelRoom.DeleteHotelRoom(roomNumber);
        }

    }
}
