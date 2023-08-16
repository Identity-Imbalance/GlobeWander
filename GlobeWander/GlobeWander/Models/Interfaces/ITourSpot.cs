namespace GlobeWander.Models.Interfaces
{
    public interface ITourSpot
    {
        public Task<List<TourSpot>> GetAllTourSpots();

        public Task<TourSpot> GetSpotById(int id);

        public Task<TourSpot> CreateTourSpot(TourSpot tourSpot);

        public Task DeleteTourSpot(int id);

        public Task<TourSpot> UpdateTourSpot(TourSpot tourSpot, int id);

    }
}
