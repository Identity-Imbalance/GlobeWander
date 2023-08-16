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

        public async Task DeleteBookingRoom(int id)
        {
            var deleteBookingRoom = await _context.BookingRooms.FindAsync(id);
            if (deleteBookingRoom != null) 
            {
                _context.Entry(deleteBookingRoom).State = EntityState.Deleted;
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

        public async Task<BookingRoom> UpdateBookingRoom(int id ,BookingRoom updatedBookingRoom )
        {
            if (id == updatedBookingRoom.ID)
            {
                _context.Entry(updatedBookingRoom).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            return updatedBookingRoom;
            
        }
    }
}
