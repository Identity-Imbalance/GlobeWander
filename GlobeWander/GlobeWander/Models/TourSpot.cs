using System.ComponentModel.DataAnnotations;

namespace GlobeWander.Models
{
    public class TourSpot
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public long PhoneNumber { get; set; }

        public List<Hotel>? Hotels { get; set; }

        public List<Trip>? Trips { get; set; }
    }
    
    public enum Category
    {
        [Display(Name = "Ritual")]
        Ritual = 1,
        [Display(Name = "Medical")]
        Medical = 2,
        [Display(Name = "Historical")]
        Historical = 3
    }

}
