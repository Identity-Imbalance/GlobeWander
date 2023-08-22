namespace GlobeWander.Models
{
    public class Trip
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string Activity  { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Theme { get; set; }
            
        public int Capacity { get; set; }

        public int Count { get; set; }

        public int TourSpotID { get; set; }

        public TourSpot? TourSpots { get; set; }

        public List<BookingTrip>? BookingTrips { get; set; }

        public List<Rate>? Rates { get; set; }

    }
}
