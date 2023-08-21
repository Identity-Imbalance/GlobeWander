using GlobeWander.Models.DTO;

namespace GlobeWander.Models.Interfaces
{
    public interface IHotelRoom
    {

        Task<hotelroomDTOcreate> CreateHotelRoom(hotelroomDTOcreate hotelRoom);

        Task<List<HotelRoomDTO>> GetHotelRooms();

        Task<HotelRoomDTO> GetHotelRoomId(int hotelID,int roomNumber);

        Task<hotelroomDTOcreate> UpdateHotelRoom(int hotelId, int roomNumber, hotelroomDTOcreate hotelRoom);

        Task<HotelRoom>  DeleteHotelRoom(int hotelID,int roomNumber);

    }
}
