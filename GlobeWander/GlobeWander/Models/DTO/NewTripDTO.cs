namespace GlobeWander.Models.DTO
{
    public class NewTripDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Cost { get; set; }

        public string Activity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Theme { get; set; }
    }
}
