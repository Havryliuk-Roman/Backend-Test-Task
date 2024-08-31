namespace FinancialBrokerApp.Models;

public record ExceptionLog
{
    public Guid Id { get; init; }
    public DateTime Timestamp { get; set; }
    public string QueryParameters { get; set; }
    public string BodyParameters { get; set; }
    public string StackTrace { get; set; }
}
