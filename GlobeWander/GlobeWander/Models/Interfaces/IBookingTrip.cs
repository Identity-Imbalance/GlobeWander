using GlobeWander.Models.DTO;

namespace GlobeWander.Models.Interfaces
{
    public interface IBookingTrip
    {
        // Create Booking Trip
        Task<BookingTripDTO> Create(NewBookingTripDTO bookingTrip, string userId);

        // GET All Booking Trips
        Task<List<BookingTripDTO>> GetAllBookingTrips();

        // GET by ID
        Task<BookingTripDTO> GetBookingTripById(int id, int tripId);

        // UPDATE
        Task<BookingTripDTO> UpdateBookingTrip(int id, UpdateBookingTripDTO updateBookingTrip, int tripId);

        // DELET by ID
        Task Delete(int id, int tripId);
    }
}
