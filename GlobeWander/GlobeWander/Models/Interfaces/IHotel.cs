namespace GlobeWander.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> CreateHotel(Hotel hotel);
        Task<List<Hotel>> GetAllHotels();
        Task<Hotel> GetHotelId(int hotelId);

        Task<Hotel> UpdateHotel(int id, Hotel updatedHotel);
        Task<Hotel> DeleteHotel(int id);

    }
}
