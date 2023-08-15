namespace GlobeWander.Models.Interface
{
    public interface IRoom
    {
        Task<Room> CreateRoom(Room room);
        Task<List<Room>> GetRooms();
        Task<Room> GetRoomId(int roomId);
        Task<Room> UpdateRoom(int roomId, Room room);
        Task <Room> DeleteRoom(int roomId);

        
    }
}
