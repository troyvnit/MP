namespace MP.Model.Models
{
    public class Item : TransportingObject
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
