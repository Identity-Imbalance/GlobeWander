using GlobeWander.Data;
using GlobeWander.Models.DTO;
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

        public async Task<BookingTripDTO> Create(BookingTripDTO bookingTrip)
        {
            var newBookingTrip = new BookingTrip()
            {
                ID = bookingTrip.ID,
                TripID = bookingTrip.TripID,
                NumberOfPersons = bookingTrip.NumberOfPersons,
                CostPerPerson = bookingTrip.CostPerPerson,
                Duration = bookingTrip.Duration
            };
            _context.Entry<BookingTrip>(newBookingTrip).State = EntityState.Added;
            await _context.SaveChangesAsync();
            var BookingTripDTO = await GetBookingTripById(newBookingTrip.ID, newBookingTrip.TripID);
            bookingTrip.ID = newBookingTrip.ID;

            return BookingTripDTO;
        }

        public async Task<BookingTripDTO> GetBookingTripById(int id, int tripId)
        {
            BookingTripDTO? bookingTrip = await _context.bookingTrips

                .Where(x => x.ID == id && x.TripID == tripId)
                .Select(bookingTrip => new BookingTripDTO
                {
                    ID = bookingTrip.ID,
                    TripID = bookingTrip.TripID,
                    NumberOfPersons = bookingTrip.NumberOfPersons,
                    CostPerPerson = bookingTrip.CostPerPerson,
                    Duration = bookingTrip.Duration

                }).FirstOrDefaultAsync();

            return bookingTrip;
        }

        public async Task<List<BookingTripDTO>> GetAllBookingTrips()
        {
            return await _context.bookingTrips
              .Select(bookTrip => new BookingTripDTO
              {
                  ID = bookTrip.ID,
                  TripID = bookTrip.TripID,
                  NumberOfPersons = bookTrip.NumberOfPersons,
                  CostPerPerson = bookTrip.CostPerPerson,
                  Duration = bookTrip.Duration,

              }).ToListAsync();
        }


        public async Task<BookingTripDTO> UpdateBookingTrip(int id, BookingTripDTO updateBookingTrip, int tripId)
        {
            var newbookingTrip = await _context.bookingTrips.FindAsync(id, tripId);

            if (newbookingTrip != null)
            {
                newbookingTrip.ID = updateBookingTrip.ID;
                newbookingTrip.TripID = updateBookingTrip.TripID;
                newbookingTrip.NumberOfPersons = updateBookingTrip.NumberOfPersons;
                newbookingTrip.CostPerPerson = updateBookingTrip.CostPerPerson;
                newbookingTrip.Duration = updateBookingTrip.Duration;



                _context.Entry(newbookingTrip).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            var returnBookingTrip = await GetBookingTripById(newbookingTrip.ID, newbookingTrip.TripID);

            return returnBookingTrip;
        }

        public async Task Delete(int id, int tripId)
        {
            var DeleteBookingTrip = await _context.bookingTrips.FindAsync(id, tripId);

            if (DeleteBookingTrip != null)
            {
                _context.Entry<BookingTrip>(DeleteBookingTrip).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }


        }
    }
}
