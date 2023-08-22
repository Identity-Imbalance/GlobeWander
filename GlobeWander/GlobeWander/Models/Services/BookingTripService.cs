using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace GlobeWander.Models.Services
{
    public class BookingTripService : IBookingTrip
    {

        private readonly GlobeWanderDbContext _context;

        private UserManager<ApplicationUser> _UserManager;

        public BookingTripService(GlobeWanderDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _UserManager = userManager;
        }

        public async Task<BookingTripDTO> Create(NewBookingTripDTO bookingTrip, string userId)
        {
            var user = await _UserManager.FindByIdAsync(userId);
            var trip = await _context.Trips.FindAsync(bookingTrip.TripID);

            var existBookingTrip = await _context.bookingTrips
                                    .Where(x=> x.TripID == bookingTrip.TripID)
                                    .FirstOrDefaultAsync(b => b.Username == user.UserName && trip.Id == bookingTrip.TripID);
           if (existBookingTrip == null)
            {

                if (trip.Capacity >= (trip.Count + bookingTrip.NumberOfPersons) )
                {
                trip.Count += bookingTrip.NumberOfPersons;

                    var newBookingTrip = new BookingTrip()
                    {

                        TripID = bookingTrip.TripID,
                        NumberOfPersons = bookingTrip.NumberOfPersons,
                        CostPerPerson = trip.Cost,
                        Duration = bookingTrip.Duration,
                        TotalPrice = bookingTrip.NumberOfPersons * trip.Cost,
                        Username = user.UserName
                    };

                    

                    _context.Entry<BookingTrip>(newBookingTrip).State = EntityState.Added;

                    await _context.SaveChangesAsync();


                    var BookingTripDTO = await GetBookingTripById(newBookingTrip.ID, newBookingTrip.TripID);
                    BookingTripDTO.ID = newBookingTrip.ID;

                    return BookingTripDTO;
                }
                return null;
            }
            return null;
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
                    Duration = bookingTrip.Duration,
                    TotalPrice = bookingTrip.TotalPrice,
                    Username = bookingTrip.Username

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
                  TotalPrice = bookTrip.TotalPrice, 
                  Username = bookTrip.Username

              }).ToListAsync();
        }


        public async Task<BookingTripDTO> UpdateBookingTrip(int id, UpdateBookingTripDTO updateBookingTrip, int tripId)
        {
            var newbookingTrip = await _context.bookingTrips.FindAsync(id, tripId);

            var trip = await _context.Trips.FindAsync(tripId);

            if (newbookingTrip != null)
            {
                
                
                newbookingTrip.NumberOfPersons = updateBookingTrip.NumberOfPersons;
                newbookingTrip.CostPerPerson = trip.Cost;
                newbookingTrip.TotalPrice = trip.Cost * updateBookingTrip.NumberOfPersons;
                newbookingTrip.Duration = updateBookingTrip.Duration;

                trip.Count += updateBookingTrip.NumberOfPersons;

                if (trip.Capacity >= trip.Count)
                {
                    _context.Entry(newbookingTrip).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                

            }
            var returnBookingTrip = await GetBookingTripById(newbookingTrip.ID, newbookingTrip.TripID);

            return returnBookingTrip;
        }

        public async Task Delete(int id, int tripId)
        {
            var DeleteBookingTrip = await _context.bookingTrips.FindAsync(id);
            var trip = await _context.Trips.FindAsync(tripId);

            if (DeleteBookingTrip != null)
            {
                trip.Count -= DeleteBookingTrip.NumberOfPersons;

                _context.Entry<BookingTrip>(DeleteBookingTrip).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            }


        }
    }
}
