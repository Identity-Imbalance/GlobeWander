namespace GlobeWander.Models.Interfaces
{
    public interface IBookingRoom
    {
        public Task<BookingRoom> CreateBookingRoom(BookingRoom bookingRoom);

        public Task<List<BookingRoom>> GetAllBookingRooms();

        public Task<BookingRoom> GetBookingRoomById(int Id);

        public Task<BookingRoom> UpdateBookingRoom(BookingRoom bookingRoom,int hotelId, int roomNumber);

        public Task DeleteBookingRoom(int hotelId, int roomNumber);
    }
}
