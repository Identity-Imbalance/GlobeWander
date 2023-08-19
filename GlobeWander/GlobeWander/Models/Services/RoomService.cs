using GlobeWander.Data;
using Microsoft.EntityFrameworkCore;
using GlobeWander.Models.Interfaces;
using GlobeWander.Models.DTO;

namespace GlobeWander.Models.Services
{
    public class RoomService : IRoom
    {
        private readonly GlobeWanderDbContext _context;

        public RoomService(GlobeWanderDbContext context)
        {
            _context = context;
        }


        public async Task<RoomDTO> CreateRoom(RoomDTO room)
        {
            Room room1 = new Room()
            {
                Name = room.Name,
                Layout = room.Layout,
            };
            _context.Rooms.Add(room1);
            await _context.SaveChangesAsync();
            RoomDTO addroom = await GetRoomId(room1.ID);
            return addroom;
        }

        public async Task<RoomDTO> DeleteRoom(int roomId)
        {
            RoomDTO room1 = await GetRoomId(roomId);
            Room room = await _context.Rooms.FindAsync(roomId);

            _context.Entry<Room>(room).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return room1;

            
        }

        public async Task<RoomDTO> GetRoomId(int roomId)
        {
            // Room room = await _context.Rooms.FindAsync(roomId);

            // return room;

            var RoomDTO = _context.Rooms.Select(s => new RoomDTO()
            {
                ID = s.ID,
                Name = s.Name,
                Layout = s.Layout,
            }).FirstOrDefault(x => x.ID == roomId);

            return RoomDTO;
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            //return await _context.Rooms.ToListAsync();
            var RoomDTO = await _context.Rooms.Select(s => new RoomDTO()
            {
                ID = s.ID,
                Name = s.Name,
                Layout = s.Layout,
            }).ToListAsync();

            return RoomDTO;

        }

        public async Task<RoomDTO> UpdateRoom(int roomId, RoomDTO room)
        {
            var room1 = await _context.Rooms.FindAsync(roomId);
            if (room1 != null)
            {
                room1.Name = room.Name;
                room1.Layout = room.Layout;
                _context.Entry(room1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            room1.ID=roomId;
            var updateroom = await GetRoomId(room.ID);
            return updateroom;
           
            

        }
    }
}
