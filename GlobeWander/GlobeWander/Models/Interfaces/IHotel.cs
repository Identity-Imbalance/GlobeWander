using GlobeWander.Models.DTO;

namespace GlobeWander.Models.Interfaces
{
    public interface IHotel
    {
        Task<HotelDTO> CreateHotel(HotelDTO hotel);
        Task<List<HotelDTO>> GetAllHotels();
        Task<HotelDTO> GetHotelId(int hotelId);

        Task<HotelDTO> UpdateHotel(int id, HotelDTO updatedHotel);
        Task<HotelDTO> DeleteHotel(int id);

    }
}
