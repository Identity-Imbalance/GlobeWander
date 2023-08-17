using GlobeWander.Data;
using GlobeWander.Models.DTO;
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

        public async Task<BookingRoomDTO> CreateBookingRoom(BookingRoomDTO bookingRoomDTO)
        {
            var bookingRoom = new BookingRoom
            {
                HotelID = bookingRoomDTO.HotelID,
                RoomNumber = bookingRoomDTO.RoomNumber,
                Cost = bookingRoomDTO.Cost,
                Duration = bookingRoomDTO.Duration
            };

            _context.BookingRooms.Add(bookingRoom);
            await _context.SaveChangesAsync();

            bookingRoomDTO.ID = bookingRoom.ID;
            return bookingRoomDTO;
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

        public async Task<List<BookingRoomDTO>> GetAllBookingRooms()
        {
            var bookingRooms = await _context.BookingRooms.ToListAsync();
            var bookingRoomDTOs = bookingRooms.Select(bookingRoom => new BookingRoomDTO
            {
                ID = bookingRoom.ID,
                HotelID = bookingRoom.HotelID,
                RoomNumber = bookingRoom.RoomNumber,
                Cost = bookingRoom.Cost,
                Duration = bookingRoom.Duration
            }).ToList();

            return bookingRoomDTOs;
        }

        public async Task<BookingRoomDTO> GetBookingRoomById(int id)
        {
            var bookingRoom = await _context.BookingRooms.FindAsync(id);
            if (bookingRoom == null)
            {
                return null;
            }

            var bookingRoomDTO = new BookingRoomDTO
            {
                ID = bookingRoom.ID,
                HotelID = bookingRoom.HotelID,
                RoomNumber = bookingRoom.RoomNumber,
                Cost = bookingRoom.Cost,
                Duration = bookingRoom.Duration
            };

            return bookingRoomDTO;
        }

        public async Task<BookingRoomDTO> UpdateBookingRoom(int id, BookingRoomDTO updatedBookingRoomDTO)
        {
            var bookingRoom = await _context.BookingRooms.FindAsync(id);
            if (bookingRoom == null)
            {
                return null;
            }

            bookingRoom.HotelID = updatedBookingRoomDTO.HotelID;
            bookingRoom.RoomNumber = updatedBookingRoomDTO.RoomNumber;
            bookingRoom.Cost = updatedBookingRoomDTO.Cost;
            bookingRoom.Duration = updatedBookingRoomDTO.Duration;

            _context.Entry(bookingRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return updatedBookingRoomDTO;
        }
    }
}
