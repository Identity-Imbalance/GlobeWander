using GlobeWander.Models;
using GlobeWander.Models.Services;

namespace Test
{
    public class UnitTest1 :Mock
    {
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