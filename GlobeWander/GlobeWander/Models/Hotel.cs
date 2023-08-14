namespace GlobeWander.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TourSpotID { get; set; }

        public TourSpot TourSpot { get; set; }

        public List<HotelRoom>? HotelRoom { get; set; }
    }
}
