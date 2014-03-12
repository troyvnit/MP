using System;
using MP.Model.Models;

namespace MP.Models
{
    public class TripModel
    {
        public int Id { get; set; }
        public TripName TripName { get; set; }
        public string Description { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public DateTime DepartureDate { get; set; }
        public DepartureTime DepartureTime { get; set; }
    }
}