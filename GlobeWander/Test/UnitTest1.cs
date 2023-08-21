using GlobeWander.Data;
using GlobeWander.Models;
using GlobeWander.Models.DTO;
using GlobeWander.Models.Interfaces;
using GlobeWander.Models.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;


namespace Test
{
    public class UnitTest1 : Mock
    {


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