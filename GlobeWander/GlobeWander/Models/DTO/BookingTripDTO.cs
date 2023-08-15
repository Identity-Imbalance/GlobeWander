namespace GlobeWander.Models.DTO
{
    public class BookingTripDTO
    {
        public int ID { get; set; }

        public int TripID { get; set; }

        public int NumberOfPersons { get; set; }

        public decimal CostPerPerson { get; set; }

        public string Duration { get; set; }
    }
}
