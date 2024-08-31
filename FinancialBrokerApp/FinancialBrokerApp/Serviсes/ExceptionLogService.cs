namespace FinancialBrokerApp.Serviсes;

using FinancialBrokerApp.Models;
using FinancialBrokerApp.Repositories;
using System.Text.Json;

public class ExceptionLogService : IExceptionLogService
{
    private readonly IRepository<ExceptionLog> _exceptionLogRepository;

    public ExceptionLogService(IRepository<ExceptionLog> exceptionLogRepository)
    {
        _exceptionLogRepository = exceptionLogRepository;
    }

    public async Task<Guid> LogExceptionAsync(Exception ex, HttpRequest request)
    {
        request.EnableBuffering(); 
        var exceptionLog = new ExceptionLog
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow,
            QueryParameters = request.QueryString.ToString(),
            BodyParameters = await ReadRequestBodyAsync(request),
            StackTrace = ex.StackTrace
        };

        await _exceptionLogRepository.AddAsync(exceptionLog);
        await _exceptionLogRepository.SaveChangesAsync();

        return exceptionLog.Id;
    }

    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.Body.Position = 0; 
        using (var reader = new StreamReader(request.Body, leaveOpen: true))
        {
            return await reader.ReadToEndAsync();
        }
    }

}
