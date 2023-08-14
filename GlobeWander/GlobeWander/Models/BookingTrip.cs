namespace GlobeWander.Models
{
    public class BookingTrip
    {
        public int ID { get; set; }

        public int TripID { get; set; }

        public int NumberOfPersons { get; set; }

        public decimal CostPerPerson { get; set; }

        public string Duration { get; set; }

        public Trip Trip { get; set; }

    }
}
