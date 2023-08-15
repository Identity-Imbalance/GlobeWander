namespace GlobeWander.Models.Interfaces
{
    public interface ITrip
    {
        public Task<List<Trip>> GetAllTrips();

        public Task<Trip> GetTripByID(int id);

        public Task<Trip> CreateTrip(Trip trip);

        public Task<Trip> UpdateTrip(Trip trip, int id);

        public Task DeleteTrip(int id);

    }
}
