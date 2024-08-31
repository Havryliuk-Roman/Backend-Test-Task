namespace FinancialBrokerApp.Serviсes;

public interface IExceptionLogService
{
    Task<Guid> LogExceptionAsync(Exception ex, HttpRequest request);
}