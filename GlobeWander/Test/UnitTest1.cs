using GlobeWander.Models;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Services;

namespace Test
{
    public class UnitTest1 :Mock
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
        }
        [Fact]
        public async void enrol_Room_method()
        {
            var room = await CreateandSaveRoom();


            var service = new RoomService(_db);
            var actRoom = await service.GetRoomId(room.ID);
            Assert.NotNull(actRoom);
            Assert.Equal(room.ID, actRoom.ID);
            Assert.Equal(room.Name, actRoom.Name);
            Assert.Equal(room.Layout, actRoom.Layout);
        }
       
 
        [Fact]
        public async void enrol_HotelRoom_method()
        {
            var hotelRoom = await CreateandSaveHotelRoom();
            var service = new HotelRoomService(_db);
            var actHotelRoom = await service.GetHotelRoomId(hotelRoom.HotelID ,hotelRoom.RoomNumber);
            Assert.NotNull(actHotelRoom);
            Assert.Equal(hotelRoom.RoomNumber, actHotelRoom.RoomNumber);
            Assert.Equal(hotelRoom.RoomID, actHotelRoom.RoomID);
            Assert.Equal(hotelRoom.HotelID , actHotelRoom.HotelID);
            Assert.Equal(hotelRoom.PricePerDay, actHotelRoom.PricePerDay);
            


        }
    }
}