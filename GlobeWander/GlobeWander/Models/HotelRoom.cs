using System.ComponentModel.DataAnnotations.Schema;

namespace GlobeWander.Models
{
    public class HotelRoom
    {
        public int RoomNumber { get; set; }

        public int HotelID { get; set; }

        public int RoomID { get; set; }

        public decimal PricePerDay { get; set; }

        public bool IsAvailable { get; set; }

        [ForeignKey("HotelID")]
        public Hotel? Hotel { get; set; }

        [ForeignKey("RoomID")]
        public Room? Rooms { get; set; }

        public BookingRoom? BookingRoom { get; set; }
    }
}
