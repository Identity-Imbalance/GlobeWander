﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GlobeWander.Models.DTO
{
    public class HotelRoomDTO
    {
        public int RoomNumber { get; set; }

        public int HotelID { get; set; }

        public int RoomID { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public RoomDTO? Rooms { get; set; }

        public BookingRoomDTO? BookingRoom { get; set; }
    }
}
