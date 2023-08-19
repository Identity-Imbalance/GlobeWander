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

        public async Task<BookingRoomDTO> CreateBookingRoom(NewBookingRoomDTO bookingRoomDTO)
        {
            var getHotelRoom = await _context.HotelRooms.FindAsync(bookingRoomDTO.HotelID,bookingRoomDTO.RoomNumber);

            if (getHotelRoom.IsAvailable)
            {
                var bookingRoom = new BookingRoom
                {
                    HotelID = bookingRoomDTO.HotelID,
                    RoomNumber = bookingRoomDTO.RoomNumber,
                    Cost = getHotelRoom.PricePerDay,
                    Duration = bookingRoomDTO.Duration,
                    TotalPrice = getHotelRoom.PricePerDay * bookingRoomDTO.Duration

                };
                getHotelRoom.IsAvailable = false;
                _context.BookingRooms.Add(bookingRoom);
                await _context.SaveChangesAsync();

                var newBookingRoom = await GetBookingRoomById(bookingRoom.ID);

                return newBookingRoom;
            }
            return null;
           
        }

        public async Task DeleteBookingRoom(int id)
        {
            var deleteBookingRoom = await _context.BookingRooms.FindAsync(id);

            var hotelRoom = await _context.HotelRooms.FindAsync(deleteBookingRoom.HotelID, deleteBookingRoom.RoomNumber);
            
            if (deleteBookingRoom != null)
            {
                hotelRoom.IsAvailable = true;
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
                Duration = bookingRoom.Duration,
                TotalPrice = bookingRoom.TotalPrice
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
                Duration = bookingRoom.Duration,
                TotalPrice = bookingRoom.TotalPrice
            };

            return bookingRoomDTO;
        }
        // user cannot update the booking room 
        public async Task<BookingRoomDTO> UpdateBookingRoom(int id, DurationBookingRoomDTO updatedBookingRoomDTO)
        {
            var bookingRoom = await _context.BookingRooms.FindAsync(id);
            if (bookingRoom != null)
            {
                bookingRoom.Duration = updatedBookingRoomDTO.Duration;
                bookingRoom.TotalPrice = updatedBookingRoomDTO.Duration * bookingRoom.Cost;
                _context.Entry(bookingRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            var newBookingRoomUpdate = await GetBookingRoomById(id);
            return newBookingRoomUpdate;
        }
    }
}
