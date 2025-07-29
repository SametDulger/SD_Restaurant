namespace SD_Restaurant.Application.Services
{
    public interface IMessageQueueService
    {
        Task PublishAsync<T>(string topic, T message);
        Task SubscribeAsync<T>(string topic, Func<T, Task> handler);
        Task UnsubscribeAsync(string topic);
    }

    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class OrderStatusChangedEvent
    {
        public int OrderId { get; set; }
        public string OldStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; }
    }

    public class StockLowEvent
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
        public DateTime DetectedAt { get; set; }
    }
} 