namespace GlobeWander.Models.DTO
{
    public class BookingRoomDTO
    {
        public int ID { get; set; }

        public int HotelID { get; set; }

        public int RoomNumber { get; set; }

        public decimal Cost { get; set; }

        public int Duration { get; set; }

        public decimal TotalPrice { get; set; }

        public string Username { get; set; }
    }
}
