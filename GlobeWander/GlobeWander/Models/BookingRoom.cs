namespace GlobeWander.Models
{
    public class BookingRoom
    {
        public int ID { get; set; }

        public int HotelID { get; set; }

        public int RoomNumber { get; set; }
         
        public decimal Cost { get; set; }

        public string Duration { get; set; }

        public HotelRoom? HotelRooms { get; set; }
    }
}
