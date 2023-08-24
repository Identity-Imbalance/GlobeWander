namespace GlobeWander.Models.DTO
{
    public class TrendTripDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string Activity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Capacity { get; set; }

        public int Count { get; set; }

    }
}
