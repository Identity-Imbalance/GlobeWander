using GlobeWander.Models.DTO;
namespace GlobeWander.Models.Interfaces
    
{
    public interface IRoom
    {
        Task<RoomDTO> CreateRoom(RoomDTO room);
        Task<List<RoomDTO>> GetRooms();
        Task<RoomDTO> GetRoomId(int roomId);
        Task<RoomDTO> UpdateRoom(int roomId, RoomDTO room);
        Task<RoomDTO> DeleteRoom(int roomId);
    }
}
