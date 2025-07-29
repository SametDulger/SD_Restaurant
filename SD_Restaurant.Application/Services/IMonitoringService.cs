namespace SD_Restaurant.Application.Services
{
    public interface IMonitoringService
    {
        void TrackEvent(string eventName, Dictionary<string, string>? properties = null);
        void TrackMetric(string metricName, double value, Dictionary<string, string>? properties = null);
        void TrackException(Exception exception, Dictionary<string, string>? properties = null);
        void TrackDependency(string dependencyTypeName, string target, string dependencyName, DateTimeOffset startTime, TimeSpan duration, bool success);
        void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success);
    }

    public class PerformanceMetrics
    {
        public string Endpoint { get; set; } = string.Empty;
        public double ResponseTime { get; set; }
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class BusinessMetrics
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveCustomers { get; set; }
        public int LowStockItems { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 