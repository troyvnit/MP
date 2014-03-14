namespace MP.Models
{
    public class PassengerModel : TransportingObjectModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int TicketQuantity { get; set; }
        public string SeatNumber { get; set; }
        public string Town { get; set; }
    }
}