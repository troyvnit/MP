using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Model.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public TripName TripName { get; set; }
        public string Description { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public DateTime DepartureDate { get; set; }
        public DepartureTime DepartureTime { get; set; }
    }

    public enum DepartureTime
    {
        First, Second
    }

    public enum TripName
    {
        SG, MA
    }
}
