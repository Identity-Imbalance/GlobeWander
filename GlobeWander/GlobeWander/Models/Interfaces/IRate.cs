using GlobeWander.Models.DTO;
using System.Security.Claims;

namespace GlobeWander.Models.Interfaces
{
    public interface IRate
    {
        Task<RateDTO> Create (NewRateDTO rateDTO, ClaimsPrincipal user);

        Task <RateDTO> GetRateById(int id);

        Task<List<RateDTO>> GetRateByTripID(int tripId);

        Task <List<RateDTO>> GetAllRate();

        Task<RateDTO> UpdateRate(int id,int TripID, UpdateRateDTO rateDTO);

        Task <Rate> DeleteRate(int id, int TripID);

        Task DeleteAllRatesByTripID(int tripId);
      
    }
}
