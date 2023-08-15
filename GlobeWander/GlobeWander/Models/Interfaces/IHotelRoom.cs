namespace GlobeWander.Models.Interfaces
{
    public interface IHotelRoom
    {

        Task<HotelRoom> CreateHotelRoom(HotelRoom hotelRoom);
        Task<List<HotelRoom>> GetHotelRooms();
        Task<HotelRoom> GetHotelRoomId(int roomNumber);
        Task<HotelRoom> UpdateHotelRoom(int roomNumber, HotelRoom hotelRoom);
        Task<HotelRoom> DeleteHotelRoom(int roomNumber);

    }
}
