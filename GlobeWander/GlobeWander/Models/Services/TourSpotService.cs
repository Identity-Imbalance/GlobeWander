using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace GlobeWander.Models.Services
{
    public class TourSpotService : ITourSpot
    {
        private readonly GlobeWanderDbContext _context;

        public TourSpotService(GlobeWanderDbContext context)
        {
            _context = context;
        }
        public async Task<TourSpotDTO> CreateTourSpot(newTourSpotDTO tourSpot)
        {
            var newTourSpot = new TourSpot()
            {
                ID = tourSpot.ID,
                Name = tourSpot.Name,
                Country = tourSpot.Country,
                City = tourSpot.City,
                Description = tourSpot.Description,
                Category = tourSpot.Category,
                PhoneNumber = tourSpot.PhoneNumber
            };
            _context.Entry<TourSpot>(newTourSpot).State = EntityState.Added;
            await _context.SaveChangesAsync();
            var tourSpotDTO = await GetSpotById(newTourSpot.ID);
            tourSpot.ID = newTourSpot.ID;

            return tourSpotDTO;
        }

        public async Task DeleteTourSpot(int id)
        {
            TourSpot tourSpotToDelete = await _context.TourSpots.FindAsync(id);

                _context.Entry<TourSpot>(tourSpotToDelete).State = EntityState.Deleted;

                await _context.SaveChangesAsync();
            
        }

        public async Task<List<TourSpotDTO>> GetAllTourSpots()
        {
            return await _context.TourSpots.Select(
                tours=> new TourSpotDTO
                {
                    ID = tours.ID,
                    Name = tours.Name,
                    Country = tours.Country,
                    City = tours.City,
                    Description = tours.Description,
                    Category = tours.Category,
                    PhoneNumber = tours.PhoneNumber,
                    Hotels = tours.Hotels.Select(hotels => new HotelDTO
                    {
                        TourSpotID = hotels.TourSpotID,
                        Id = hotels.Id,
                        Name = hotels.Name,
                        Description = hotels.Description,
                        HotelRoom = hotels.HotelRoom.Select(hrooms => new HotelRoomDTO
                        {
                            RoomNumber = hrooms.RoomNumber,
                            HotelID = hrooms.HotelID,
                            RoomID = hrooms.RoomID,
                            PricePerDay = hrooms.PricePerDay,
                            IsAvailable = hrooms.IsAvailable,
                            Rooms = new RoomDTO
                            {
                                ID = hrooms.Rooms.ID,
                                Name = hrooms.Rooms.Name,
                                Layout = hrooms.Rooms.Layout
                                
                            },
                            BookingRoom = new BookingRoomDTO
                            {
                                ID = hrooms.BookingRoom.ID,
                                RoomNumber = hrooms.BookingRoom.RoomNumber,
                                HotelID = hrooms.BookingRoom.HotelID,
                                Cost = hrooms.BookingRoom.Cost,
                                Duration = hrooms.BookingRoom.Duration,
                                TotalPrice = hrooms.BookingRoom.TotalPrice,
                                Username = hrooms.BookingRoom.Username
                            }
                        }).ToList(),
                    }).ToList(),
                    Trips = tours.Trips
                    .Where(x=> x.TourSpotID == tours.ID)
                    .Select(trips => new TripDTO
                    {
                        Id = trips.Id,
                        Name = trips.Name,
                        Description = trips.Description,
                        Cost = trips.Cost,
                        Activity = trips.Activity,
                        StartDate = trips.StartDate,
                        EndDate = trips.EndDate,
                        Capacity = trips.Capacity,
                        Count = trips.Count,
                        TourSpotID = trips.TourSpotID,
                        BookingTrips = trips.BookingTrips.Select(bt => new BookingTripDTO
                        {
                            ID = bt.ID,
                            TripID = bt.TripID,
                            NumberOfPersons = bt.NumberOfPersons,
                            CostPerPerson = bt.CostPerPerson,
                            TotalPrice = bt.TotalPrice,
                            Duration = bt.Duration,
                            Username = bt.Username
                        }).ToList(),
                        Rates = trips.Rates.Select(r => new RateDTO
                        {
                            ID = r.ID,
                            TripID = r.TripID,
                            Comments = r.Comments,
                            Rating = r.Rating,
                            Username = r.Username
                        }).ToList()
                    }).ToList()

                }
                ).ToListAsync();
            
        }

        public async Task<TourSpotDTO> GetSpotById(int id)
        {
            TourSpotDTO? tourSpot = await _context.TourSpots
                .Where(x=> x.ID == id)
                .Select(
                tours => new TourSpotDTO
                {
                    ID = tours.ID,
                    Name = tours.Name,
                    Country = tours.Country,
                    City = tours.City,
                    Description = tours.Description,
                    Category = tours.Category,
                    PhoneNumber = tours.PhoneNumber,
                    Hotels = tours.Hotels.Select(hotels => new HotelDTO
                    {
                        TourSpotID = hotels.TourSpotID,
                        Id = hotels.Id,
                        Name = hotels.Name,
                        Description = hotels.Description,
                        HotelRoom = hotels.HotelRoom.Select(hrooms => new HotelRoomDTO
                        {
                            RoomNumber = hrooms.RoomNumber,
                            HotelID = hrooms.HotelID,
                            RoomID = hrooms.RoomID,
                            PricePerDay = hrooms.PricePerDay,
                            IsAvailable = hrooms.IsAvailable,
                            Rooms = new RoomDTO
                            {
                                ID = hrooms.Rooms.ID,
                                Name = hrooms.Rooms.Name,
                                Layout = hrooms.Rooms.Layout

                            },
                            BookingRoom = new BookingRoomDTO
                            {
                                ID = hrooms.BookingRoom.ID,
                                RoomNumber = hrooms.BookingRoom.RoomNumber,
                                HotelID = hrooms.BookingRoom.HotelID,
                                Cost = hrooms.BookingRoom.Cost,
                                Duration = hrooms.BookingRoom.Duration,
                                TotalPrice = hrooms.BookingRoom.TotalPrice,
                                Username = hrooms.BookingRoom.Username

                            }
                        }).ToList(),
                    }).ToList(),
                    Trips = tours.Trips
                    .Where(x => x.TourSpotID == tours.ID)
                    .Select(trips => new TripDTO
                    {
                        Id = trips.Id,
                        Name = trips.Name,
                        Description = trips.Description,
                        Cost = trips.Cost,
                        Activity = trips.Activity,
                        StartDate = trips.StartDate,
                        EndDate = trips.EndDate,
                        Capacity = trips.Capacity,
                        Count = trips.Count,
                        TourSpotID = trips.TourSpotID,
                        BookingTrips = trips.BookingTrips.Select(bt => new BookingTripDTO
                        {
                            ID = bt.ID,
                            TripID = bt.TripID,
                            NumberOfPersons = bt.NumberOfPersons,
                            CostPerPerson = bt.CostPerPerson,
                            TotalPrice = bt.TotalPrice,
                            Duration = bt.Duration,
                            Username = bt.Username
                        }).ToList(),
                        Rates = trips.Rates.Select(r => new RateDTO
                        {
                            ID = r.ID,
                            TripID = r.TripID,
                            Comments = r.Comments,
                            Rating = r.Rating,
                            Username = r.Username
                        }).ToList()
                    }).ToList()

                }
                ).FirstOrDefaultAsync();
                
                

            return tourSpot;
        }

        public async Task<TourSpotDTO> UpdateTourSpot(newTourSpotDTO tourSpot, int id)
        {
            var tourSpotRecord = await _context.TourSpots.FindAsync(id);

            if (tourSpotRecord != null)
            {
                    tourSpotRecord.ID = id;
                    tourSpotRecord.Name = tourSpot.Name;
                    tourSpotRecord.Country = tourSpot.Country;
                    tourSpotRecord.City = tourSpot.City;
                    tourSpotRecord.Description = tourSpot.Description;
                    tourSpotRecord.Category = tourSpot.Category;
                    tourSpotRecord.PhoneNumber = tourSpot.PhoneNumber;
                               
                _context.Entry(tourSpotRecord).State = EntityState.Modified;

                await _context.SaveChangesAsync();

               
            }
            tourSpot.ID = id;
            var updateTour = await GetSpotById(tourSpot.ID);
            return updateTour;

        }
    }
}
