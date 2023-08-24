using GlobeWander.Models.DTO;

namespace GlobeWander.Models.Interfaces
{
    public interface ITourSpot
    {
        public Task<List<TourSpotDTO>> GetAllTourSpots();

        public Task<TourSpotDTO> GetSpotById(int id);

        public Task<TourSpotDTO> CreateTourSpot(newTourSpotDTO tourSpot);

        // create a method to add a hotel and trip from the post by the id 

        public Task DeleteTourSpot(int id);

        public Task<TourSpotDTO> UpdateTourSpot(newTourSpotDTO tourSpot, int id);

        // create a method to update the hotel and trip and add them by the id 

        public Task<List<TrendTourSpotDTO>> GetMostVisitedTourSpots();
    }
}
