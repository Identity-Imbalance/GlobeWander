using System.ComponentModel.DataAnnotations;

namespace GlobeWander.Models.DTO
{
    public class TourSpotDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public long PhoneNumber { get; set; }

        public List<HotelDTO>? Hotels { get; set; }

        public List<TripDTO>? Trips { get; set; }
    }
}
