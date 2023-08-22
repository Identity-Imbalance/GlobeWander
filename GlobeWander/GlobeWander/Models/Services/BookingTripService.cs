using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Xunit.Sdk;

namespace GlobeWander.Models.Services
{/// <summary>
/// Service implementation for managing booking trips.
/// </summary>
    public class BookingTripService : IBookingTrip
    {

        private readonly GlobeWanderDbContext _context;

        private UserManager<ApplicationUser> _UserManager;

        public BookingTripService(GlobeWanderDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _UserManager = userManager;
        }

        public async Task<BookingTripDTO> Create(NewBookingTripDTO bookingTrip, ClaimsPrincipal userPrincipal)
        {
            var getUserId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _UserManager.FindByIdAsync(getUserId);

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


                    var BookingTripDTO = await GetBookingTripById(newBookingTrip.ID);
                    BookingTripDTO.ID = newBookingTrip.ID;

                    return BookingTripDTO;
                }
                return null;
            }
            return null;
        }

        public async Task<BookingTripDTO> GetBookingTripById(int id)
        {
            BookingTripDTO? bookingTrip = await _context.bookingTrips

                .Where(x => x.ID == id )
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

        /// <summary>
        /// Get a list of all booking trips.
        /// </summary>
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


        /// <summary>
        /// Update a booking trip's information.
        /// </summary>
        /// <param name="id">ID of the booking trip to update.</param>
        /// <param name="updateBookingTrip">Updated booking trip data.</param>
        /// <param name="tripId">ID of the associated trip.</param>
        public async Task<BookingTripDTO> UpdateBookingTrip(int id, UpdateBookingTripDTO updateBookingTrip, int tripId)
        {
            var newbookingTrip = await _context.bookingTrips.FindAsync(id);

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
            var returnBookingTrip = await GetBookingTripById(newbookingTrip.ID);

            return returnBookingTrip;
        }

        /// <summary>
        /// Delete a booking trip by its ID and associated trip ID.
        /// </summary>
        /// <param name="id">ID of the booking trip.</param>
        /// <param name="tripId">ID of the associated trip.</param>
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
