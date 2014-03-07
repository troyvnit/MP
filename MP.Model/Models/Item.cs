namespace MP.Model.Models
{
    public class Item : TransportingObject
    {
        public string Description { get; set; }
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public Town Town { get; set; }
    }

    public enum Town
    {
        MA, MQ, LB, MD
    }
}
