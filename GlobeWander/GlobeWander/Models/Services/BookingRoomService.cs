using GlobeWander.Data;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{
    public class BookingRoomService : IBookingRoom
    {
        private readonly GlobeWanderDbContext _context;

        public BookingRoomService(GlobeWanderDbContext context)
        {
            _context = context;
        }

        public async Task<BookingRoom> CreateBookingRoom(BookingRoom bookingRoom)
        {
            _context.BookingRooms.Add(bookingRoom);
            await _context.SaveChangesAsync();
            return bookingRoom;                    
        }

        public async Task DeleteBookingRoom(int hotelId, int roomNumber)
        {
            var delete = await _context.BookingRooms
                .Where(r => r.HotelID == hotelId && r.RoomNumber == roomNumber)
                .FirstOrDefaultAsync();
            if (delete != null)
            {
                _context.Entry<BookingRoom>(delete).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BookingRoom>> GetAllBookingRooms()
        {
            var BookingRooms = await _context.BookingRooms.ToListAsync();

            return BookingRooms;
        }

        public async Task<BookingRoom> GetBookingRoomById(int Id)
        {
            BookingRoom bookingRoom = await _context.BookingRooms.FindAsync(Id);

            return bookingRoom;
        }

        public async Task<BookingRoom> UpdateBookingRoom(BookingRoom updatedBookingRoom, int hotelId, int roomNumber)
        {
            BookingRoom bookingRoom = new BookingRoom();
            bookingRoom.Cost = updatedBookingRoom.Cost;
            bookingRoom.Duration= updatedBookingRoom.Duration;

            _context.Entry(bookingRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return bookingRoom;
        }
    }
}
