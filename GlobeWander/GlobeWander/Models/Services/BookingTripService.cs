using GlobeWander.Data;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{
    public class BookingTripService : IBookingTrip
    {

        private readonly GlobeWanderDbContext _context;

        public BookingTripService(GlobeWanderDbContext context)
        {
            _context = context;
        }

        public async Task<BookingTrip> Create(BookingTrip bookingTrip)
        {
            _context.Entry(bookingTrip).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return bookingTrip;
        }

        public async Task<BookingTrip> GetBookingTripById(int id)
        {
            BookingTrip BookingTrip = await _context.bookingTrips.FindAsync(id);

            return BookingTrip;
        }

        public async Task<List<BookingTrip>> GetAllBookingTrips()
        {
            var BookingTrips = await _context.bookingTrips.ToListAsync();

            return BookingTrips;
        } 
   

        public async Task<BookingTrip> UpdateBookingTrip(int id, BookingTrip updateBookingTrip)
        {
            BookingTrip bookingTrip = await GetBookingTripById(id);
            bookingTrip.NumberOfPersons = updateBookingTrip.NumberOfPersons;
            bookingTrip.CostPerPerson = updateBookingTrip.CostPerPerson;
            bookingTrip.Duration = updateBookingTrip.Duration;


            _context.Entry(bookingTrip).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return bookingTrip;
        }

        public async Task<BookingTrip> Delete(int id)
        {
            BookingTrip bookingTrip = await GetBookingTripById(id);
            _context.Entry(bookingTrip).State = EntityState.Deleted;
            await _context.SaveChangesAsync();


            return bookingTrip;
        }
    }
}
