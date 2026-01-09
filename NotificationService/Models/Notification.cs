namespace NotificationService.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}