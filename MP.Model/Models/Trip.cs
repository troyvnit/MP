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
        public string Name { get; set; }
        public string Description { get; set; }
        public DepartureTime DepartureTime { get; set; }
    }

    public enum DepartureTime
    {
        First, Second
    }
}
