namespace SharedEvents
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
