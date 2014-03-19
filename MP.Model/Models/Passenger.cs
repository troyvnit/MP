using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Model.Models
{
    public class Passenger : TransportingObject
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int TicketQuantity { get; set; }
        public string SeatNumber { get; set; }
        public Town Town { get; set; }
    }

    public enum Town
    {
        TR, DD, TX, MH, TK, GT, CM, CC, N2, MD, LB, MQ, DT, MDHU
    }
}
