namespace GlobeWander.Models.DTO
{
    public class RateDTO
    {
       public int ID { get; set; }

        public int TripID { get; set; }

        public string Comments { get; set; }

        public int Rating { get; set; }

        public string Username { get; set; }
    }
}
