﻿namespace GlobeWander.Models.DTO
{
    public class TripDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Cost { get; set; }

        public string Activity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Theme { get; set; }

        public List<BookingTrip>? BookingTrips { get; set; }

        public List<Rate>? Rates { get; set; }
    }
}