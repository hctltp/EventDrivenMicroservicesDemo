using MassTransit;
using NotificationService.Data;
using NotificationService.Models;
using SharedEvents;

namespace NotificationService
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IServiceProvider _provider;

        public OrderCreatedConsumer(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;

            var order = new Notification
            {
                Id = message.OrderId,
                CustomerName = message.CustomerName,
                CreatedAt = message.CreatedAt
            };

            using (var scope = _provider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
                dbContext.Notifications.Add(order);
                await dbContext.SaveChangesAsync();
            }

            Console.WriteLine($"Notification: Order {message.OrderId} created for {message.CustomerName} at {message.CreatedAt}");
        }
    }
}
