using GlobeWander.Data;
using GlobeWander.Models;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Test
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async void CreateRate_ReturnsCreatedRate()
        {

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

    }
}