using GlobeWander.Models.DTO;

namespace GlobeWander.Models.Interfaces
{
    public interface IRate
    {
        Task<RateDTO> Create (RateDTO rateDTO);
        Task <RateDTO> GetRateById(int id,int TripID);
        Task <List<RateDTO>> GetAllRate();
        Task<RateDTO> UpdateRate(int id,int TripID,RateDTO rateDTO);
        Task <Rate> DeleteRate(int id, int TripID);
      

    }
}
