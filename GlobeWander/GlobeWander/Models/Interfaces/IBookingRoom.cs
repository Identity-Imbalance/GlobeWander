using GlobeWander.Models.DTO;

namespace GlobeWander.Models.Interfaces
{
    public interface IBookingRoom
    {
        public Task<BookingRoomDTO> CreateBookingRoom(NewBookingRoomDTO bookingRoom,string userId);

        public Task<List<BookingRoomDTO>> GetAllBookingRooms();

        public Task<BookingRoomDTO> GetBookingRoomById(int Id);

        public Task<BookingRoomDTO> UpdateBookingRoom(int id, DurationBookingRoomDTO bookingRoom);

        public Task DeleteBookingRoom(int id);
    }
}
