using GlobeWander.Controllers;
using GlobeWander.Data;
using GlobeWander.Models;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using GlobeWander.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Diagnostics.Metrics;
using System.Security.Claims;


namespace Test
{
    public class UnitTest1 : Mock
    {
        /// <summary>
        /// Get Tour Tip test if exist 
        /// </summary>
        /// <returns></returns>

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
        /// <summary>
        /// Update Tour trip test
        /// </summary>
        /// <returns></returns>


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
                Name = "test",
                Country = "test",
                City = "test",
                Description = "test",
                Category = Category.Medical,
                PhoneNumber = 324
            };
            var actualTourSpot = await service.UpdateTourSpot(UpdatedTourSpot, tourSpotDTO.ID);
            Assert.Equal(UpdatedTourSpot.Name, actualTourSpot.Name);
            Assert.Equal(UpdatedTourSpot.Country, actualTourSpot.Country);
            Assert.Equal(UpdatedTourSpot.City, actualTourSpot.City);
            Assert.Equal(UpdatedTourSpot.Description, actualTourSpot.Description);
            Assert.Equal(UpdatedTourSpot.Category, actualTourSpot.Category);
            Assert.Equal(UpdatedTourSpot.PhoneNumber, actualTourSpot.PhoneNumber);
        }
        /// <summary>
        /// delete tour trip by id test
        /// </summary>
        /// <returns></returns>



        [Fact]
        public async Task DeleteTourSpot_Successfully()
        {
            var tourSpotService = new TourSpotService(_db);
            var tourSpot = await CreateAndSaveTestTourSpot();

            await tourSpotService.DeleteTourSpot(tourSpot.ID);

            var deletedTourSpot = await tourSpotService.GetSpotById(tourSpot.ID);
            Assert.Null(deletedTourSpot);
        }
        /// <summary>
        /// Create Tour trip test 
        /// </summary>
        /// <returns></returns>

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
        /// <summary>
        /// Get Hotel test 
        /// </summary>
        /// <returns></returns>

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
        /// <summary>
        /// create hotel test
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// update hotel test 
        /// </summary>

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

        /// <summary>
        /// delete hotel test 
        /// </summary>
        /// <returns></returns>
        //Delete
        [Fact]
        public async Task DeleteHotelTest()
        {
            var hotel = await CreateAndSaveTestHotel();
            var service = new HotelService(_db);


            var deleteHotel = await service.DeleteHotel(hotel.Id);

            Assert.NotNull(deleteHotel);
        }
        /// <summary>
        /// create room test
        /// </summary>
        [Fact]
        public async void CreateRoomTest()
        {
            var room = await CreateandSaveRoom();


            var service = new RoomService(_db);
            var actRoom = await service.GetRoomId(room.ID);
            Assert.NotNull(actRoom);
            Assert.Equal(room.ID, actRoom.ID);
            Assert.Equal(room.Name, actRoom.Name);
            Assert.Equal(room.Layout, actRoom.Layout);
        }

        /// <summary>
        /// CreateHotel Room Test
        /// </summary>

        [Fact]
        public async void CreateHotelRoomTest()
        {
            var hotelRoom = await CreateandSaveHotelRoom();
            var service = new HotelRoomService(_db);
            var actHotelRoom = await service.GetHotelRoomId(hotelRoom.HotelID, hotelRoom.RoomNumber);
            Assert.NotNull(actHotelRoom);
            Assert.Equal(hotelRoom.RoomNumber, actHotelRoom.RoomNumber);
            Assert.Equal(hotelRoom.RoomID, actHotelRoom.RoomID);
            Assert.Equal(hotelRoom.HotelID, actHotelRoom.HotelID);
            Assert.Equal(hotelRoom.PricePerDay, actHotelRoom.PricePerDay);
        }

        /// <summary>
        ///  can update the rate 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void CreateRate_ReturnsCreatedRate()
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

        [Fact]
        public async Task UpdateRate_ReturnsUpdatedRate()
        {
            // Arrange
            var rate = await CreateRatesAndSave();
            var rateService = new RateService(_db, _UserManager);
            var updatedRateDTO = new UpdateRateDTO
            {

                Comments = "Updated Comment",
                Rating = 5
            };

            // Act
            var x = await rateService.UpdateRate(rate.ID, rate.TripID, updatedRateDTO);
            var newRateUpdate = await rateService.GetRateById(rate.ID);

            // Assert
            Assert.NotNull(newRateUpdate);
            Assert.Equal(x.TripID, newRateUpdate.TripID);
            Assert.Equal(updatedRateDTO.Comments, newRateUpdate.Comments);
            Assert.Equal(updatedRateDTO.Rating, newRateUpdate.Rating);
        }

        /// <summary>
        /// can delete the rate
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteRate_ReturnsDeletedRate()
        {
            var rate = await CreateRatesAndSave();
            var rateService = new RateService(_db, _UserManager);
            var deletedRate = await rateService.DeleteRate(rate.ID, rate.TripID);

            Assert.NotNull(deletedRate);
            Assert.Equal(rate.ID, deletedRate.ID);
        }

        /// <summary>
        /// can get the hotel room 
        /// </summary>
        /// <returns></returns>
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
            var hotelRoom = await CreateandSaveHotelRoom();
            var hotelRoomService = new HotelRoomService(_db);
            var RetrievedHotelRoom = await hotelRoomService.GetHotelRoomId(hotelRoom.HotelID, hotelRoom.RoomNumber);
            Assert.NotNull(RetrievedHotelRoom);
        }
        /// <summary>
        /// can update a hotel room
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task Update_HotelRoom()
        {

            // Arrange
            var hotelRoom = await CreateandSaveHotelRoom();
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

        /// <summary>
        /// can delete a hotel room 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_HotelRoom()
        {
            var hotelRoom = await CreateandSaveHotelRoom();
            var hotelRoomService = new HotelRoomService(_db);
            await hotelRoomService.DeleteHotelRoom(hotelRoom.HotelID, hotelRoom.RoomNumber);
            var deletedHotRoom = await hotelRoomService.GetHotelRoomId(hotelRoom.HotelID, hotelRoom.RoomNumber);
            Assert.Null(deletedHotRoom);

        }

        /// <summary>
        /// can get a rate 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetRate_ReturnsRate()
        {

            var rate = await CreateRatesAndSave();
            var rateService = new RateService(_db, _UserManager);


            var retrievedRate = await rateService.GetRateById(rate.ID);


            Assert.NotNull(retrievedRate);
            Assert.Equal(rate.ID, retrievedRate.ID);

        }

        /// <summary>
        ///  updating the book trip 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_BookingTripTest()
        {
            await CreateTripAndSave();
            var bookTrip = await CreateBookTrip();
            var service = new BookingTripService(_db, _UserManager);
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
                Duration = 10
            };
            var actualUpdate = await service.UpdateBookingTrip(bookTrip.ID, updateBookingTrip, bookTrip.TripID);
            var newRateUpdate = await service.GetBookingTripById(bookTrip.ID);
            Assert.NotNull(actualUpdate);
            Assert.Equal(newRateUpdate.NumberOfPersons, actualUpdate.NumberOfPersons);
            Assert.Equal(newRateUpdate.Duration, actualUpdate.Duration);
        }


        /// <summary>
        /// user can book a trip
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostBookingTrip_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            var bookingTrip = new NewBookingTripDTO
            {
                TripID = 123,
                Duration = 5,
                NumberOfPersons = 8
            };
            var bookingTripServiceMock = new Mock<IBookingTrip>();
            bookingTripServiceMock.Setup(service => service.Create(It.IsAny<NewBookingTripDTO>(), It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(new BookingTripDTO { ID = 1, TripID = bookingTrip.TripID, Duration = bookingTrip.Duration, NumberOfPersons = bookingTrip.NumberOfPersons }); // Replace with your expected result
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Role, "Admin Manager"),
                new Claim(ClaimTypes.NameIdentifier, "UserId"),
                new Claim(ClaimTypes.Name, "TestUser") // Add the username claim
                }));

            var controller = new BookingTripsController(bookingTripServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.PostBookingTrip(bookingTrip);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var bookingTripResult = Assert.IsAssignableFrom<BookingTripDTO>(createdResult.Value);
            Assert.Equal(1, bookingTripResult.ID); // Replace with your expected result
            Assert.Equal(123, bookingTripResult.TripID);
            Assert.Equal(5, bookingTripResult.Duration);
            var claimsIdentity = user.Identity as ClaimsIdentity;
            Assert.NotNull(claimsIdentity);
            var nameIdentifierClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var NameClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var RoleClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            Assert.Equal("UserId", nameIdentifierClaim.Value);
            Assert.Equal("TestUser", NameClaim.Value);
            Assert.Equal("Admin Manager", RoleClaim.Value);
        }


        /// <summary>
        /// can get a trip
        /// </summary>
        /// <returns></returns>
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
            Assert.Equal(trip.Capacity, actulalTrip.Capacity);
            Assert.Equal(trip.Count, actulalTrip.Count);
            Assert.Equal(trip.TourSpotID, actulalTrip.TourSpotID);
        }

        /// <summary>
        /// can create a trip
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateTripTest()
        {
            var trip = await CreateAndSaveTestTrip();
            var service = new TripService(_db);

            var newTripDTO = new NewTripDTO()
            {
                Name = "Test",
                Description = "Test",
                Cost = 1,
                Activity = "Test",
                StartDate = DateTime.Parse("2020-01-01"),
                EndDate = DateTime.Parse("2020-02-02"),
                Capacity = 20,
                Count = 15,
                TourSpotID = 1
            };

            var actualTrip = await service.CreateTrip(newTripDTO);

            Assert.NotNull(actualTrip);
            Assert.Equal(actualTrip.Name, newTripDTO.Name);
        }

        /// <summary>
        /// can update a trip 
        /// </summary>
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
                Cost = 1,
                Activity = "Test",
                StartDate = DateTime.Parse("2020-01-01"),
                EndDate = DateTime.Parse("2020-02-02"),
                Capacity = 20,
                Count = 15,
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
                Capacity = newTripDTO.Capacity,
                Count = newTripDTO.Count,
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
            Assert.Equal(UpdatedTrip.Capacity, y.Capacity);
            Assert.Equal(UpdatedTrip.Count, y.Count);
            Assert.Equal(UpdatedTrip.TourSpotID, y.TourSpotID);

        }

        /// <summary>
        /// can delete a trip
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteTripTest()
        {
            var trip = await CreateAndSaveTestTrip();
            var service = new TripService(_db);


            await service.DeleteTrip(trip.Id);
            var deletedTrip = await service.GetTripByID(trip.Id);
            Assert.Null(deletedTrip);

        }
        /// <summary>
        /// can get the record of booking trip
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_BookingTripTest()
        {
            var bookingTrip = await CreateBookTrip();
            var service = new BookingTripService(_db, _UserManager);
            var bookingTripDTO = await service.GetBookingTripById(bookingTrip.ID);
            Assert.NotNull(bookingTripDTO);

        }
        /// <summary>
        /// can delete the booking trip 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_BookingTripTest()
        {
            var bookingTrip = await CreateBookTrip();
            var service = new BookingTripService(_db, null);
            await service.Delete(bookingTrip.ID, bookingTrip.TripID);
            var bookingTripDeleted = await service.GetBookingTripById(bookingTrip.ID);
            Assert.Null(bookingTripDeleted);
        }


        /// <summary>
        /// user can book a room
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task PostBookingRoom_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            await CreateandSaveHotelRoom();
            var bookingRoom = new NewBookingRoomDTO
            {
                HotelID = 1,
                RoomNumber = 101,
                Duration = 5
            };

            var bookingRoomServiceMock = new Mock<IBookingRoom>();
            bookingRoomServiceMock.Setup(service => service.CreateBookingRoom(It.IsAny<NewBookingRoomDTO>(), It.IsAny<string>()))
                .ReturnsAsync(new BookingRoomDTO { ID = 1, HotelID = bookingRoom.HotelID, Duration = bookingRoom.Duration, RoomNumber = bookingRoom.RoomNumber }); // Replace with your expected result
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Role, "Admin Manager"),
                new Claim(ClaimTypes.NameIdentifier, "UserId"),
                new Claim(ClaimTypes.Name, "TestUser") // Add the username claim
                }));

            var controller = new BookingRoomsController(bookingRoomServiceMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await controller.PostBookingRoom(bookingRoom);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var bookingRoomResult = Assert.IsAssignableFrom<BookingRoomDTO>(createdResult.Value);
            Assert.Equal(1, bookingRoomResult.ID); // Replace with your expected result
            Assert.Equal(101, bookingRoomResult.RoomNumber);
            Assert.Equal(5, bookingRoomResult.Duration);
            Assert.Equal(1, bookingRoomResult.HotelID);
            var claimsIdentity = user.Identity as ClaimsIdentity;
            Assert.NotNull(claimsIdentity);
            var nameIdentifierClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var NameClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            var RoleClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            Assert.Equal("UserId", nameIdentifierClaim.Value);
            Assert.Equal("TestUser", NameClaim.Value);
            Assert.Equal("Admin Manager", RoleClaim.Value);
        }

        /// <summary>
        /// can get a booking room 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_BookingRoomTest()
        {
            await CreateandSaveHotelRoom();
            var bookingRoom = await CreateBookRoom();
            var service = new BookingRoomService(_db, _UserManager);
            var bookingRoomDTO = await service.GetBookingRoomById(bookingRoom.ID);
            Assert.NotNull(bookingRoomDTO);
            Assert.Equal(1, bookingRoomDTO.ID);

        }
        /// <summary>
        /// can delete the booking trip 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_BookingRoomTest()
        {
            await CreateandSaveHotelRoom();
            var bookingRoom = await CreateBookRoom();
            var service = new BookingRoomService(_db, _UserManager);
            await service.DeleteBookingRoom(bookingRoom.ID);
            var bookingRoomDeleted = await service.GetBookingRoomById(bookingRoom.ID);
            Assert.Null(bookingRoomDeleted);
        }


        /// <summary>
        /// Register as a role admin 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// user can log in successfully 
        /// </summary>
        /// <returns></returns>
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
