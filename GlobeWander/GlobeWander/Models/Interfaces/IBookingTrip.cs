namespace GlobeWander.Models.Interfaces
{
    public interface IBookingTrip
    {
        // Create Booking Trip
        Task<BookingTrip> Create(BookingTrip bookingTrip);

        // GET All Booking Trips
        Task<List<BookingTrip>> GetAllBookingTrips();

        // GET by ID
        Task<BookingTrip> GetBookingTripById(int id);

        // UPDATE
        Task<BookingTrip> UpdateBookingTrip(int id, BookingTrip updateBookingTrip);

        // DELET by ID
        Task<BookingTrip> Delete(int id);
    }
}
