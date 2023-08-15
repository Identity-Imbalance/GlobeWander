using GlobeWander.Data;
using Microsoft.EntityFrameworkCore;
using GlobeWander.Models.Interfaces;

namespace GlobeWander.Models.Services
{
    public class RoomService : IRoom
    {
        private readonly GlobeWanderDbContext _context;

        public RoomService(GlobeWanderDbContext context)
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
            Room? room = await _context.Rooms.FindAsync(roomId);

            _context.Entry<Room>(room).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

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
