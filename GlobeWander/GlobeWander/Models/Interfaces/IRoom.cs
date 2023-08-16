using GlobeWander.Models.DTO;
namespace GlobeWander.Models.Interfaces
    
{
    public interface IRoom
    {
        Task<RoomDTO> CreateRoom(newRoomDTo room);
        Task<List<RoomDTO>> GetRooms();
        Task<RoomDTO> GetRoomId(int roomId);
        Task<Room> UpdateRoom(int roomId, Room room);
        Task<Room> DeleteRoom(int roomId);
    }
}
