using System;

namespace MP.Model.Models
{
    public abstract class TransportingObject
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
