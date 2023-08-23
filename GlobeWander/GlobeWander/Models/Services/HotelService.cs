using GlobeWander.Data;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlobeWander.Models.Services
{/// <summary>
/// Service implementation for managing hotels.
/// </summary>
    public class HotelService : IHotel
    {


        private readonly GlobeWanderDbContext _context;

        public HotelService(GlobeWanderDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create a new hotel.
        /// </summary>
        /// <param name="hotelDTO">Data for the new hotel.</param>
        public async Task<NewHotelDTO> CreateHotel(NewHotelDTO hotelDTO)
        {
            Hotel hotel =new Hotel() {    
                Name= hotelDTO.Name,
                Description= hotelDTO.Description,
                TourSpotID= hotelDTO.TourSpotID
            };

             _context.Entry(hotel).State = EntityState.Added;
            await _context.SaveChangesAsync();

            hotelDTO.Id = hotel.Id;

            return hotelDTO;
        }

        /// <summary>
        /// Delete a hotel by its ID.
        /// </summary>
        /// <param name="id">ID of the hotel.</param>
        public async Task<Hotel> DeleteHotel(int id)

        {
            // Hotel hoteldto = await GetHotelId(id);
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Entry(hotel).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            


            return hotel;
        }

        /// <summary>
        /// Get a hotel by its ID.
        /// </summary>
        /// <param name="hotelId">ID of the hotel.</param>

        public async Task<HotelDTO> GetHotelId(int hotelId)
        {
            var hotelRooms = await _context.HotelRooms.Where(x => x.HotelID == hotelId).Select(v => new HotelRoomDTO
            {
                HotelID = v.HotelID,
                RoomID = v.RoomID,
                IsAvailable = v.IsAvailable,
                RoomNumber = v.RoomNumber,
                PricePerDay = v.PricePerDay,
                Rooms=_context.Rooms.Select(r => new RoomDTO
                {
                    ID = r.ID,
                    Layout = r.Layout,
                    Name = r.Name
                }).Where(r=>r.ID== v.RoomID).FirstOrDefault(),
                BookingRoom=_context.BookingRooms.Select(bk=>new BookingRoomDTO
                {
                    ID = bk.ID,
                    HotelID= bk.HotelID,
                    Cost = bk.Cost,
                    RoomNumber= bk.RoomNumber,
                    TotalPrice= bk.TotalPrice,
                    Duration= bk.Duration,
                    Username=bk.Username
                }).Where(bk=>bk.RoomNumber==v.RoomNumber).FirstOrDefault()

            }).ToListAsync();

            HotelDTO? hotel = await _context.Hotels.Where(x => x.Id == hotelId).Select(b => new HotelDTO
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                TourSpotID = b.TourSpotID,
                HotelRoom = hotelRooms
            }).FirstOrDefaultAsync();
            ;
            return hotel;

        }

        /// <summary>
        /// Get a list of all hotels.
        /// </summary>
        public async Task<List<HotelDTO>> GetAllHotels()
        {
            

            List<HotelDTO> hotel = await _context.Hotels.Select(b => new HotelDTO
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                TourSpotID = b.TourSpotID,
                HotelRoom = _context.HotelRooms.Where(x=>x.HotelID==b.Id).Select(v => new HotelRoomDTO
                {
                    HotelID = v.HotelID,
                    RoomID = v.RoomID,
                    IsAvailable = v.IsAvailable,
                    RoomNumber = v.RoomNumber,
                    PricePerDay = v.PricePerDay,
                    Rooms = _context.Rooms.Select(r => new RoomDTO
                    {
                        ID = r.ID,
                        Layout = r.Layout,
                        Name = r.Name
                    }).Where(r => r.ID == v.RoomID).FirstOrDefault(),
                    BookingRoom = _context.BookingRooms.Select(bk => new BookingRoomDTO
                    {
                        ID = bk.ID,
                        HotelID = bk.HotelID,
                        Cost = bk.Cost,
                        RoomNumber = bk.RoomNumber,
                        TotalPrice = bk.TotalPrice,
                        Duration = bk.Duration,
                        Username = bk.Username
                    }).Where(bk => bk.RoomNumber == v.RoomNumber).FirstOrDefault()


                }).ToList()
        }).ToListAsync();
          

            return hotel;
        }

        /// <summary>
        /// Update a hotel's information.
        /// </summary>
        /// <param name="id">ID of the hotel.</param>
        /// <param name="updatedHotel">Updated hotel data.</param>
        public async Task<HotelDTO> UpdateHotel(int id, HotelDTO updatedHotel)
        {
            Hotel? hotel = await _context.Hotels.FindAsync(id);

            if (hotel != null)
            {

                hotel.Name = updatedHotel.Name;
                hotel.Description = updatedHotel.Description;

                _context.Entry(hotel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                updatedHotel.Id = id;
                updatedHotel.TourSpotID = hotel.TourSpotID;

                return updatedHotel;
            }
            return null;
        }

        public async Task<List<AnonymousHotelDTO>> AnonymousHotelDTOs()
        {
            return await _context.Hotels.Select(x => new AnonymousHotelDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
        }
    }
}

