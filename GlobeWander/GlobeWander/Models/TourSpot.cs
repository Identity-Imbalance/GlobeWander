namespace GlobeWander.Models
{
    public class TourSpot
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Description { get; set; }

        public string Categoary { get; set; }

        public string PhoneNumber { get; set; }

        public List<Hotel> Hotels { get; set; }

        public List<Trip> Trips { get; set; }
    }
}
