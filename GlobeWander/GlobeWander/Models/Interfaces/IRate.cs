using GlobeWander.Models.DTO;

namespace GlobeWander.Models.Interfaces
{
    public interface IRate
    {
        Task<RateDTO> Create (RateDTO rateDTO);
        Task <RateDTO> GetRateById(int id);
        Task <List<RateDTO>> GetAllRate();
        Task<RateDTO> UpdateRate(int id,RateDTO rateDTO);
        Task<RateDTO> DeleteRate(int id);
      

    }
}
