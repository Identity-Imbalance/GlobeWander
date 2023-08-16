namespace GlobeWander.Models.Interfaces
{
    public interface IHotelRoom
    {

        Task<HotelRoom> CreateHotelRoom(HotelRoom hotelRoom);
        Task<List<HotelRoom>> GetHotelRooms();
        Task<HotelRoom> GetHotelRoomId(int roomNumber,int hotelId);
        Task<HotelRoom> UpdateHotelRoom(int roomNumber,int hotelId, HotelRoom hotelRoom);
        Task<HotelRoom> DeleteHotelRoom(int roomNumber, int hotelID);

    }
}
