namespace GlobeWander.Models
{
    public class Rate
    {
        public int ID { get; set; }

        public int TripID { get; set; }

        public string Comments { get; set;}

        public int Rating { get; set; }

        public string Username { get; set; }

        public Trip? Trip { get; set; }
    }
}
