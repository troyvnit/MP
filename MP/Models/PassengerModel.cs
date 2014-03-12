using MP.Model.Models;

namespace MP.Models
{
    public class PassengerModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int TicketQuantity { get; set; }
        public string SeatNumber { get; set; }
        public Town Town { get; set; }
    }
}