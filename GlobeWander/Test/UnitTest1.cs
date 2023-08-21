using GlobeWander.Models;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Services;

namespace Test
{
    public class UnitTest1 : Mock
    {
        // Hotel Tests //

        //Get
        [Fact]
        public async Task CanGetHotel()
        {
            // Arrange
            var hotel = await CreateAndSaveTestHotel();

            var service = new HotelService(_db);

            // Act
            var actulalHotel = await service.GetHotelId(hotel.Id);

            // Assert
            Assert.NotNull(actulalHotel);
            Assert.Equal(hotel.Id, actulalHotel.Id);
            Assert.Equal(hotel.Name, actulalHotel.Name);
            Assert.Equal(hotel.Description, actulalHotel.Description);
            Assert.Equal(hotel.TourSpotID, actulalHotel.TourSpotID);
            
        }
        //Create
        [Fact]
        public async Task CreatHotelTest()
        {
            var hotel = await CreateAndSaveTestHotel();
            var service = new HotelService(_db);

            var hotelDTO = new HotelDTO()
            {
                
                Name = "Test",
                Description = "Test",
                TourSpotID = 1
            };

            var actualHotel = await service.CreateHotel(hotelDTO);

            Assert.NotNull(actualHotel);
            Assert.Equal(actualHotel.Name, hotelDTO.Name);
        }

        //update

        [Fact]
        public async void UpdateHotel()
        {
            var hotel = await CreateAndSaveTestHotel();
            var service = new HotelService(_db);

            var hotelDTO = new HotelDTO()
            {
                Id = hotel.Id,
                Name = "Test",
                Description = "Test",
                TourSpotID = 1

            };

            var UpdatedHotel = new HotelDTO()
            {
                Name = hotelDTO.Name,
                Description = hotelDTO.Description,
                TourSpotID = hotelDTO.TourSpotID
            };

            var actualHotel = await service.UpdateHotel(hotelDTO.Id, UpdatedHotel);

            Assert.Equal(UpdatedHotel.Name, actualHotel.Name);
            Assert.Equal(UpdatedHotel.Description, actualHotel.Description);
            Assert.Equal(UpdatedHotel.TourSpotID, actualHotel.TourSpotID);
            
        }

        //Delete
        [Fact]
        public async Task DeleteHotelTest()
        {
            var hotel = await CreateAndSaveTestHotel();
            var service = new HotelService(_db);


            var deleteHotel = await service.DeleteHotel(hotel.Id);
            
            Assert.NotNull(deleteHotel);

        }

        // Trip Tests //

        //Get
        [Fact]
        public async Task CanGetTrip()
        {
            // Arrange
            var trip = await CreateAndSaveTestTrip();

            var service = new TripService(_db);

            // Act
            var actulalTrip = await service.GetTripByID(trip.Id);

            // Assert
            Assert.NotNull(actulalTrip);
            Assert.Equal(trip.Id, actulalTrip.Id);
            Assert.Equal(trip.Name, actulalTrip.Name);
            Assert.Equal(trip.Description, actulalTrip.Description);
            Assert.Equal(trip.Cost, actulalTrip.Cost);
            Assert.Equal(trip.Activity, actulalTrip.Activity);
            Assert.Equal(trip.StartDate, actulalTrip.StartDate);
            Assert.Equal(trip.EndDate, actulalTrip.EndDate);
            Assert.Equal(trip.Theme, actulalTrip.Theme);
            Assert.Equal(trip.TourSpotID, actulalTrip.TourSpotID);
        }

        //Create
        [Fact]
        public async Task CreatTripTest()
        {
            var trip = await CreateAndSaveTestTrip();
            var service = new TripService(_db);

            var newTripDTO = new NewTripDTO()
            {
                Name = "Test",
                Description = "Test",
                Cost = 1.0,
                Activity = "Test",
                StartDate = DateTime.Parse("2020-01-01"),
                EndDate = DateTime.Parse("2020-02-02"),
                Theme = "Test",
                TourSpotID = 1
            };

            var actualTrip = await service.CreateTrip(newTripDTO);

            Assert.NotNull(actualTrip);
            Assert.Equal(actualTrip.Name, newTripDTO.Name);
        }

        //update

        [Fact]
        public async void UpdateTrip()
        {
            var trip = await CreateAndSaveTestTrip();
            var service = new TripService(_db);

            var newTripDTO = new NewTripDTO()
            {
                Id = trip.Id,
                Name = "Test",
                Description = "Test",
                Cost = 1.0,
                Activity = "Test",
                StartDate = DateTime.Parse("2020-01-01"),
                EndDate = DateTime.Parse("2020-02-02"),
                Theme = "Test",
                TourSpotID = 1
            };

            var x = await service.CreateTrip(newTripDTO);

            var UpdatedTrip = new NewTripDTO()
            {
                Name = newTripDTO.Name,
                Description = newTripDTO.Description,
                Cost = newTripDTO.Cost,
                Activity = newTripDTO.Activity,
                StartDate = newTripDTO.StartDate,
                EndDate = newTripDTO.EndDate,
                Theme = newTripDTO.Theme,
                TourSpotID = newTripDTO.TourSpotID
            };

            var actualTrip = await service.UpdateTrip(UpdatedTrip, x.Id);
            var y = await service.GetTripByID(actualTrip.Id);

            Assert.Equal(UpdatedTrip.Name, y.Name);
            Assert.Equal(UpdatedTrip.Description, y.Description);
            Assert.Equal(UpdatedTrip.Cost, y.Cost);
            Assert.Equal(UpdatedTrip.Activity, y.Activity);
            Assert.Equal(UpdatedTrip.StartDate, y.StartDate);
            Assert.Equal(UpdatedTrip.EndDate, y.EndDate);
            Assert.Equal(UpdatedTrip.Theme, y.Theme);
            Assert.Equal(UpdatedTrip.TourSpotID, y.TourSpotID);

        }

        //Delete
        [Fact]
        public async Task DeleteTripTest()
        {
            var trip = await CreateAndSaveTestTrip();
            var service = new TripService(_db);
            

            await service.DeleteTrip(trip.Id);
            var deletedTrip = await service.GetTripByID(trip.Id);
            Assert.Null(deletedTrip);

        }
    }
}