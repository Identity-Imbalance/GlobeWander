using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobeWander.Models.Services
{
    /// <summary>
    /// Service implementation for managing hotel rooms.
    /// </summary>
    public class HotelRoomService : IHotelRoom
    {


        private readonly GlobeWanderDbContext _context;

        public HotelRoomService(GlobeWanderDbContext context)
        {
            _context = context;

        }

        /// <summary>
        /// Create a new hotel room.
        /// </summary>
        /// <param name="hotelRoomdto">Data for the new hotel room.</param>
        public async Task<HotelRoomDTO> CreateHotelRoom(hotelroomDTOcreate hotelRoomdto)
        {
            HotelRoom hotelRooms = new HotelRoom()
            {
                RoomNumber= hotelRoomdto.HotelID*100 + hotelRoomdto.RoomID,
                HotelID= hotelRoomdto.HotelID,
                RoomID= hotelRoomdto.RoomID,
                PricePerDay= hotelRoomdto.PricePerDay,
                IsAvailable=true,

            };


          
            //hotelRoomdto.RoomNumber = hotelRooms.RoomNumber;

            _context.HotelRooms.Add(hotelRooms);
            await _context.SaveChangesAsync();

            var hotelRoom = await GetHotelRoomId(hotelRooms.HotelID, hotelRooms.RoomNumber);

            return hotelRoom;
        }

        /// <summary>
        /// Get a hotel room by its hotel ID and room number.
        /// </summary>
        /// <param name="hotelID">ID of the hotel.</param>
        /// <param name="roomNumber">Number of the room.</param>
        public async Task<HotelRoomDTO> GetHotelRoomId(int hotelID, int roomNumber)
        {

            var hotelRoomDTO = await _context.HotelRooms
       .Where(b => b.HotelID == hotelID && b.RoomNumber == roomNumber)
       .Select(b => new HotelRoomDTO
       {
           HotelID = b.HotelID,
           RoomNumber = b.RoomNumber,
           RoomID = b.RoomID,
           PricePerDay = b.PricePerDay,
           IsAvailable = b.IsAvailable
       })
       .FirstOrDefaultAsync();

            if (hotelRoomDTO != null)
            {
                var room = await _context.Rooms
                    .Where(r => r.ID == hotelRoomDTO.RoomID)
                    .Select(r => new RoomDTO
                    {
                        ID = r.ID,
                        Name = r.Name,
                        Layout = r.Layout
                    })
                    .FirstOrDefaultAsync();

                hotelRoomDTO.Rooms = room;

                var bookingRoom = await _context.BookingRooms
                    .Where(x => x.HotelID == hotelID && x.RoomNumber == roomNumber)
                    .Select(x => new BookingRoomDTO
                    {
                        ID = x.ID,
                        HotelID = x.HotelID,
                        RoomNumber = x.RoomNumber,
                        Cost = x.Cost,
                        Duration = x.Duration,
                        TotalPrice = x.TotalPrice,
                        Username = x.Username
                    })
                    .FirstOrDefaultAsync();

                hotelRoomDTO.BookingRoom = bookingRoom;
            }

            return hotelRoomDTO;


        }

        /// <summary>
        /// Delete a hotel room by its hotel ID and room number.
        /// </summary>
        /// <param name="hotelID">ID of the hotel.</param>
        /// <param name="roomNumber">Number of the room.</param>
        public async Task<HotelRoom> DeleteHotelRoom(int hotelID, int roomNumber)
        {

            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelID, roomNumber);
            if (hotelRoom != null)
            {
                _context.Entry(hotelRoom).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            return hotelRoom;

        }

        /// <summary>
        /// Get a list of all hotel rooms.
        /// </summary>
        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {

            List<HotelRoomDTO> hotelRoomDTO = await _context.HotelRooms.Select(b => new HotelRoomDTO
            {
                HotelID = b.HotelID,
                RoomNumber = b.RoomNumber,
                RoomID = b.RoomID,
                PricePerDay = b.PricePerDay,
                IsAvailable = b.IsAvailable,
                Rooms = _context.Rooms.Select(
                    x => new RoomDTO
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Layout = x.Layout
                    })
                .Where(x => x.ID == b.RoomID)
                .FirstOrDefault(),
                BookingRoom = _context.BookingRooms.Select(x =>
                new BookingRoomDTO()
                {
                    ID = x.ID,
                    HotelID = x.HotelID,
                    RoomNumber = x.RoomNumber,
                    Cost = x.Cost,
                    Duration = x.Duration,
                    TotalPrice = x.TotalPrice,
                    Username = x.Username
                })
                .FirstOrDefault(i => i.RoomNumber == b.RoomNumber && i.HotelID == b.HotelID)
            }).ToListAsync();




            return hotelRoomDTO;
        }

        /// <summary>
        /// Update a hotel room's information.
        /// </summary>
        /// <param name="hotelId">ID of the hotel.</param>
        /// <param name="roomNumber">Number of the room.</param>
        /// <param name="updatedHotelRoom">Updated hotel room data.</param>
        public async Task<hotelroomDTOcreate> UpdateHotelRoom(int hotelId, int roomNumber, hotelroomDTOcreate updatedHotelRoom)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            
                hotelRoom.PricePerDay = updatedHotelRoom.PricePerDay;
                hotelRoom.IsAvailable = updatedHotelRoom.IsAvailable;
                _context.Entry(hotelRoom).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            updatedHotelRoom.HotelID = hotelId;
            updatedHotelRoom.RoomNumber = roomNumber;
            updatedHotelRoom.RoomID = hotelRoom.RoomID;
                return updatedHotelRoom;
          

            
        
        }

        public async Task<List<AnonymousHotelRoomDTO>> GetAnonymousHotelRoomDTO()
        {
            return await _context.HotelRooms.Select(hr => new AnonymousHotelRoomDTO
            {
                HotelID = hr.HotelID,
                RoomNumber = hr.RoomNumber,
                RoomID = hr.RoomID,
                IsAvailable = hr.IsAvailable,
                PricePerDay = hr.PricePerDay,
            }).ToListAsync();
        }
    }
}
