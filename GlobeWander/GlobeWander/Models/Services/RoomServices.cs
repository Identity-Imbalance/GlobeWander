using GlobeWander.Models.Interface;
using GlobeWander.Data;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{
    public class RoomServices : IRoom
    {
        private readonly GlobeWanderDbContext _context;

        public RoomServices(GlobeWanderDbContext context)
        {
            _context = context;
        }
       

        public async Task<Room> CreateRoom(Room room)
        {
          _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> DeleteRoom(int roomId)
        {
           Room room = await _context.Rooms.FindAsync(roomId);
            return room;
        }

       
        

       

        public async Task<Room> GetRoomId(int roomId)
        {
           Room room = await _context.Rooms.FindAsync(roomId);
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        

        public async Task<Room> UpdateRoom(int roomId, Room room)
        {
            var Temproom = await GetRoomId(roomId);
            Temproom.Name = room.Name;
            Temproom.Layout = room.Layout;
           
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
    }
}
