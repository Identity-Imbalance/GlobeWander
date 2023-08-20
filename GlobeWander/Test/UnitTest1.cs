using GlobeWander.Data;
using GlobeWander.Models;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using GlobeWander.Models.Interfaces;
using GlobeWander.Models.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using GlobeWander.Models;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Services;

namespace Test
{
    public class UnitTest1 : Mock
    {
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

            var rateService = new RateService(_db);
            var rateDTO = new RateDTO
            {
                TripID = 1,
                Comments = "Test Comment",
                Rating = "4"
            };


            var createdRate = await rateService.Create(rateDTO);


            Assert.NotNull(createdRate);
            Assert.Equal(1, createdRate.TripID);


            _db.Rates.RemoveRange(_db.Rates);
            await _db.SaveChangesAsync();
        }
        [Fact]
        public async Task GetRate_ReturnsRate()
        {

            var rate = await CreateRatesAndSave();
            var rateService = new RateService(_db);


            var retrievedRate = await rateService.GetRateById(rate.ID, rate.TripID);


            Assert.NotNull(retrievedRate);
            Assert.Equal(rate.ID, retrievedRate.ID);

        }
        [Fact]
        public async Task UpdateRate_ReturnsUpdatedRate()
        {
            // Arrange
            var rate = await CreateRatesAndSave();
            var rateService = new RateService(_db);
            var updatedRateDTO = new RateDTO
            {
                TripID = rate.TripID,
                Comments = "Updated Comment",
                Rating = "5"
            };

            // Act
            await rateService.UpdateRate(rate.ID, rate.TripID, updatedRateDTO);
            var newRateUpdate = await rateService.GetRateById(rate.ID, rate.TripID);

            // Assert
            Assert.NotNull(newRateUpdate);
            Assert.Equal(updatedRateDTO.TripID, newRateUpdate.TripID);
            Assert.Equal(updatedRateDTO.Comments, newRateUpdate.Comments);
            Assert.Equal(updatedRateDTO.Rating, newRateUpdate.Rating);
        }

        [Fact]
        public async Task DeleteRate_ReturnsDeletedRate()
        {

            var rate = await CreateRatesAndSave();
            var rateService = new RateService(_db);
            var deletedRate = await rateService.DeleteRate(rate.ID, rate.TripID);

            Assert.NotNull(deletedRate);
            Assert.Equal(rate.ID, deletedRate.ID);
        }

        [Fact]
        public async void Crate_HotelRoom()
        {
            var HotelRoomService = new HotelRoomService(_db);
            var hotelRoomDTO = new hotelroomDTOcreate
            {
                RoomNumber = 101,
                HotelID = 1,
                RoomID = 1,
                PricePerDay = 100,
                IsAvailable = true,
            };
            var createHotelRoom = await HotelRoomService.CreateHotelRoom(hotelRoomDTO);

            Assert.NotNull(createHotelRoom);
            Assert.Equal(101, createHotelRoom.RoomNumber);



            await _db.SaveChangesAsync();
        }

        [Fact]
        public async Task Get_HotelRoom()
        {
            var hotelRoom = await CreateHotelRoom();
            var hotelRoomService = new HotelRoomService(_db);
            var RetrievedHotelRoom = await hotelRoomService.GetHotelRoomId(hotelRoom.HotelID,hotelRoom.RoomNumber);
            Assert.NotNull(RetrievedHotelRoom);
        }

        [Fact]
        public async Task Update_HotelRoom()
        {

            // Arrange
            var hotelRoom = await CreateHotelRoom();
            var hotelRoomDTO = new hotelroomDTOcreate
            {
                RoomNumber = hotelRoom.RoomNumber,
                HotelID = hotelRoom.HotelID,
                RoomID = hotelRoom.RoomID,
                IsAvailable = hotelRoom.IsAvailable,
                PricePerDay = hotelRoom.PricePerDay
            };

            var service = new HotelRoomService(_db);

            // Act
            var actualResult = await service.CreateHotelRoom(hotelRoomDTO);

            var hotelRoomDtoToUpdate = new hotelroomDTOcreate()
            {
                RoomNumber = 101,
                HotelID = 1,
                RoomID = 1,
                IsAvailable = false,
                PricePerDay = 200
            };

            await service.UpdateHotelRoom(actualResult.HotelID, actualResult.RoomNumber, hotelRoomDtoToUpdate);
            var updatedHotelRoom = await service.GetHotelRoomId(actualResult.HotelID, actualResult.RoomNumber);

            // Assert
            //Assert.NotNull(updatedHotelRoom);
            Assert.Equal(hotelRoomDtoToUpdate.PricePerDay, updatedHotelRoom.PricePerDay);
            Assert.Equal(hotelRoomDtoToUpdate.IsAvailable, updatedHotelRoom.IsAvailable);

        }
        [Fact]
        public async Task Delete_HotelRoom()
        {
            var hotelRoom = await CreateHotelRoom();
            var hotelRoomService = new HotelRoomService(_db);
            await hotelRoomService.DeleteHotelRoom(hotelRoom.HotelID, hotelRoom.RoomNumber);
            var deletedHotRoom = await hotelRoomService.GetHotelRoomId(hotelRoom.HotelID, hotelRoom.RoomNumber);
            Assert.Null(deletedHotRoom);

        }



         [Fact]
         public async Task GetTourSpotIfExists()
         {
             var TourSpotService = new TourSpotService(_db);
             var tourSpot = await CreateAndSaveTestTourSpot();

             var tourSpotDTO = await TourSpotService.GetSpotById(tourSpot.ID);

             Assert.Equal(tourSpot.ID, tourSpotDTO.ID);
             Assert.Equal(tourSpot.Name, tourSpotDTO.Name);
             Assert.Equal(tourSpot.Country, tourSpotDTO.Country);
             Assert.Equal(tourSpot.City, tourSpotDTO.City);
             Assert.Equal(tourSpot.Description, tourSpotDTO.Description);
             Assert.Equal(tourSpot.Category, tourSpotDTO.Category);
             Assert.Equal(tourSpot.PhoneNumber, tourSpotDTO.PhoneNumber);
        }

     
        [Fact]
        public async Task UpdateTourSpot()
        {
            
            var service = new TourSpotService(_db);

            var tourSpot = await CreateAndSaveTestTourSpot();

            var tourSpotDTO = new newTourSpotDTO
            {

                Name = tourSpot.Name,
                Country = tourSpot.Country,
                City = tourSpot.City,
                Description = tourSpot.Description,
                Category = tourSpot.Category,
                PhoneNumber = tourSpot.PhoneNumber
            };

            await service.CreateTourSpot(tourSpotDTO);
            var UpdatedTourSpot = new newTourSpotDTO()
            {
                Name = tourSpotDTO.Name,
                Country = tourSpotDTO.Country,
                City = tourSpotDTO.City,
                Description = tourSpotDTO.Description,
                Category = tourSpotDTO.Category,
                PhoneNumber = tourSpotDTO.PhoneNumber
            };
            var actualTourSpot = await service.UpdateTourSpot(UpdatedTourSpot, tourSpotDTO.ID);
            Assert.Equal(UpdatedTourSpot.Name, actualTourSpot.Name);
            Assert.Equal(UpdatedTourSpot.Country, actualTourSpot.Country);
            Assert.Equal(UpdatedTourSpot.City, actualTourSpot.City);
            Assert.Equal(UpdatedTourSpot.Description, actualTourSpot.Description);
            Assert.Equal(UpdatedTourSpot.Category, actualTourSpot.Category);
            Assert.Equal(UpdatedTourSpot.PhoneNumber, actualTourSpot.PhoneNumber);
        }



        [Fact]
         public async Task DeleteTourSpot_Successfully()
         {
             var tourSpotService = new TourSpotService(_db);
             var tourSpot = await CreateAndSaveTestTourSpot();

             await tourSpotService.DeleteTourSpot(tourSpot.ID);

             var deletedTourSpot = await tourSpotService.GetSpotById(tourSpot.ID);
             Assert.Null(deletedTourSpot);
         }

        [Fact]
        public async Task CreateTourSpot_Successfully()
        {
            var TourSpotService = new TourSpotService(_db);

            var tourSpot = await CreateAndSaveTestTourSpot();

            var tourSpotDTO = new newTourSpotDTO
            {
                
                Name = tourSpot.Name,
                Country = tourSpot.Country,
                City = tourSpot.City,
                Description = tourSpot.Description,
                Category = tourSpot.Category,
                PhoneNumber = tourSpot.PhoneNumber
            };

            await TourSpotService.CreateTourSpot(tourSpotDTO);

            Assert.NotNull(tourSpotDTO);
            
            Assert.Equal("Test", tourSpotDTO.Name);
        }


    }
}