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
        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Item> Items { get; set; } 
    }

    public enum DepartureTime
    {
        C300 = 300, 
        C530 = 530, 
        C700 = 700, 
        C830 = 830, 
        C900 = 900, 
        C1030 = 1030, 
        C1130 = 1130, 
        C1230 = 1230, 
        C1330 = 1330,
        C1400 = 1400,
        C1500 = 1500,
        C1530 = 1530,
        C1700 = 1700
    }

    public enum TripName
    {
        SG, MA
    }
}
