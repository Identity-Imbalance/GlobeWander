namespace GlobeWander.Models.Interfaces
{
    public interface IBookingRoom
    {
        public Task<BookingRoom> CreateBookingRoom(BookingRoom bookingRoom);

        public Task<List<BookingRoom>> GetAllBookingRooms();

        public Task<BookingRoom> GetBookingRoomById(int Id);

        public Task<BookingRoom> UpdateBookingRoom(int id, BookingRoom bookingRoom);

        public Task DeleteBookingRoom(int id);
    }
}
