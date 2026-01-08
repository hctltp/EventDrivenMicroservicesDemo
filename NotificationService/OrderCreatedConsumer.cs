using MassTransit;
using SharedEvents;

namespace NotificationService
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;
            // Simulate sending notification
            Console.WriteLine($"Notification: Order {message.OrderId} created for {message.CustomerName} at {message.CreatedAt}");
            await Task.CompletedTask;
        }
    }
    
}
