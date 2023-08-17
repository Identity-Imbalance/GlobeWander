﻿using System;
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
    public class HotelRoomsController : ControllerBase
    {

        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom Hotelroom)
        {
            _hotelRoom = Hotelroom;

        }

        // GET: api/HotelRooms
        [HttpGet]

        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms()
        {
            return await _hotelRoom.GetHotelRooms();
        }

        // GET: api/HotelRooms/5
        [HttpGet("Hotels/{hotelID}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom(int hotelID, int roomNumber)
        {
            return await _hotelRoom.GetHotelRoomId(hotelID, roomNumber);

        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<hotelroomDTOcreate>> PostHotelRoom(hotelroomDTOcreate hotelRoom)
        {
            return await _hotelRoom.CreateHotelRoom(hotelRoom);

        }


        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Hotel/{hotelId}/Room/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId,int roomNumber, hotelroomDTOcreate hotelRoom)
        {
            var ret = await _hotelRoom.UpdateHotelRoom(hotelId, roomNumber, hotelRoom);
            return Ok(ret);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("Hotels/{hotelID}/Rooms/{roomNumber}")]
        public async Task<HotelRoomDTO> DeleteHotelRoom(int hotelID,int roomNumber)
        {
            // return Ok(await _hoteRoom.Delete(hotelId, idRoom));

            return await _hotelRoom.DeleteHotelRoom(hotelID, roomNumber);
        }

    }
}
