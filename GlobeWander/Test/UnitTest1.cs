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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GlobeWander.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.Sqlite;

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
            var RetrievedHotelRoom = await hotelRoomService.GetHotelRoomId(hotelRoom.HotelID, hotelRoom.RoomNumber);
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

        //[Fact]
        //public async Task Create_BookTripTest()
        //{
        //    await CreateTripAndSave();
        //    var bookTrip = await CreateBookTrip();

        //    // Create a mock ClaimsPrincipal with a specific user identifier
        //    var userIdentifier = "user123"; // Replace with an appropriate user identifier
        //    var userClaims = new[] { new Claim(ClaimTypes.NameIdentifier, userIdentifier) };
        //    var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        //    var service = new BookingTripService(_db, _UserManager);

        //    var bookingTripDTO = new NewBookingTripDTO
        //    {
        //        TripID = bookTrip.TripID,
        //        Duration = bookTrip.Duration,
        //        NumberOfPersons = bookTrip.NumberOfPersons,
        //    };

        //    await service.Create(bookingTripDTO, userPrincipal);
        //    Assert.NotEmpty(userClaims);

        //}


        [Fact]
        public async Task GetBookingTrips_ReturnsOkWithBookingTrips()
        {
            // Arrange
            var bookingTripServiceMock = new Mock<IBookingTrip>();
            bookingTripServiceMock.Setup(service => service.GetAllBookingTrips())
                .ReturnsAsync(new List<BookingTripDTO> { new BookingTripDTO() }); // Replace with your expected result

            var controller = new BookingTripsController(bookingTripServiceMock.Object);

            // Act
            var result = await controller.GetbookingTrips();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var bookingTripsResult = Assert.IsAssignableFrom<IEnumerable<BookingTripDTO>>(okResult.Value);
            Assert.Single(bookingTripsResult); // Replace with your expected result count
        }



        [Fact]
        public async Task PostBookingTrip_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            var bookingTripServiceMock = new Mock<IBookingTrip>();
            bookingTripServiceMock.Setup(service => service.Create(It.IsAny<NewBookingTripDTO>(), It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new BookingTripDTO { ID = 1, TripID = 123 }); // Replace with your expected result

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.NameIdentifier, "user12")
    }));

            var controller = new BookingTripsController(bookingTripServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var bookingTrip = new NewBookingTripDTO
            {
                TripID = 123,
                Duration = 5,
                NumberOfPersons = 8
            };

            // Act
            var result = await controller.PostBookingTrip(bookingTrip);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var bookingTripResult = Assert.IsAssignableFrom<BookingTripDTO>(createdResult.Value);
            Assert.Equal(1, bookingTripResult.ID);

            var claimsIdentity = user.Identity as ClaimsIdentity;
            Assert.NotNull(claimsIdentity);
            var nameIdentifierClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            Assert.NotEqual("user123", nameIdentifierClaim.Value);

        }
        //[Fact]
        //public async Task Create_BookingTrip_Success()
        //{
        //    // Arrange
        //    var options = CreateInMemoryDatabaseOptions();

        //    using (var context = new GlobeWanderDbContext(options))
        //    {
        //        var trip = await CreateTripAndSave();
        //        var bookingTrip = await CreateBookTrip();


        //        var userIdentifier = "user123";
        //        var userClaims = new[] { new Claim(ClaimTypes.NameIdentifier, userIdentifier) };
        //        var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        //        var userManagerMock = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object, null, null, null, null, null, null, null, null);

        //        var service = new BookingTripService(context, userManagerMock.Object);

        //        var bookingTripDTO = new NewBookingTripDTO
        //        {
        //            TripID = bookingTrip.TripID,
        //            Duration = bookingTrip.Duration,
        //            NumberOfPersons = bookingTrip.NumberOfPersons,
        //        };

        //        // Act
        //        var result = await service.Create(bookingTripDTO, userPrincipal);

        //        // Assert
        //        Assert.NotNull(result);
        //        // Add more assertions based on your expected results
        //    }
        //}


        [Fact]
            public async Task Update_BookingTripTest()
        {
            await CreateTripAndSave();
            var bookTrip = await CreateBookTrip();
            var service = new BookingTripService( _db, _UserManager);
            var bookingTripDTO = new BookingTripDTO
            {
                TripID = bookTrip.TripID,
                Duration = bookTrip.Duration,
                NumberOfPersons = bookTrip.NumberOfPersons,
                CostPerPerson = bookTrip.CostPerPerson,
                TotalPrice = bookTrip.TotalPrice,

            };
         
            var updateBookingTrip = new UpdateBookingTripDTO
            {
                NumberOfPersons = 3,
            //    CostPerPerson = 20,
                Duration = 25
            };
            var newRateUpdate = await service.GetBookingTripById(bookTrip.ID,bookTrip.TripID);
            var actualUpdate = await service.UpdateBookingTrip(bookTrip.ID, updateBookingTrip, bookTrip.TripID);
            Assert.NotNull(actualUpdate);
            Assert.Equal(newRateUpdate.NumberOfPersons, actualUpdate.NumberOfPersons);
        }

        [Fact]
        public async Task Get_BookingTripTest()
        {
            var bookingTrip = await CreateBookTrip();
            var service = new BookingTripService(_db, _UserManager);
            var bookingTripDTO = await service.GetBookingTripById(bookingTrip.ID, bookingTrip.TripID);
            Assert.NotNull(bookingTripDTO);

        }

    

        [Fact]
        public async Task Delet_BookingTripTest()
        {
            var bookingTrip = await CreateBookTrip();
            var service = new BookingTripService(_db,null);
            await service.Delete(bookingTrip.ID,bookingTrip.TripID);
            var bookingTripDeleted = await service.GetBookingTripById(bookingTrip.ID,bookingTrip.TripID);
            Assert.Null(bookingTripDeleted);

        }
        [Fact]
        public async Task Register_User_As_Admin_Manager()
        {
            // Arrange
            var userMock = new Mock<IUser>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(MockBehavior.Strict, null, null, null, null, null, null, null, null);
            var jwtTokenServiceMock = new Mock<JWTTokenService>(null, null);

            var roles = new List<Claim> { new Claim(ClaimTypes.Role, "Admin Manager") };
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(roles));

            var controller = new UserController(userMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = userPrincipal
                }
            };

            var registerDto = new RegisterUserDTO
            {
                UserName = "TestUser",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Password = "P@ssw0rd",
                Roles = new List<string> { "Admin Manager" } // Adjust the roles as needed
            };

            var expectedResult = new UserDTO
            {
                Id = "UserId",
                UserName = registerDto.UserName,
                Token = "MockedToken",
                Roles = new List<string> { "Admin Manager" } // Adjust the roles as needed
            };

            userMock.Setup(u => u.Register(It.IsAny<RegisterUserDTO>(), It.IsAny<ModelStateDictionary>(), It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(expectedResult);

            // Act
            var result = await controller.Register(registerDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);

            Assert.Equal(expectedResult.UserName, userDto.UserName);
            Assert.Equal(expectedResult.Roles, userDto.Roles);

        }

        [Fact]
        public async Task SignIn_User_Successfully()
        {
            // Arrange
            var expectedResult = new UserDTO
            {
                Id = "UserId",
                UserName = "TestUser",
                Token = "MockedToken",
                Roles = new List<string> { "Admin Manager" }
            };

            var userMock = SetupUserMock(expectedResult);
            var controller = new UserController(userMock);

            var loginDto = new LogInDTO
            {
                UserName = "TestUser",
                Password = "P@ssw0rd"
            };

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var userDto = Assert.IsType<UserDTO>(actionResult.Value);

            Assert.Equal(expectedResult.UserName, userDto.UserName);
            Assert.Equal(expectedResult.Roles, userDto.Roles);
        }
    }
}
