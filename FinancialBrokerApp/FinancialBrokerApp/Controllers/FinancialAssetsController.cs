namespace FinancialBrokerApp.Controllers;

using FinancialBrokerApp.Exceptions;
using FinancialBrokerApp.Models;
using FinancialBrokerApp.Repositories;
using FinancialBrokerApp.Serviсes;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class FinancialAssetsController : ControllerBase
{
    private readonly IRepository<FinancialAsset> _financialAssetRepository;
    private readonly IExceptionLogService _exceptionLogService;

    public FinancialAssetsController(IRepository<FinancialAsset> financialAssetRepository, IExceptionLogService exceptionLogService)
    {
        _financialAssetRepository = financialAssetRepository;
        _exceptionLogService = exceptionLogService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsset([FromBody] FinancialAsset asset)
    {
        try
        {
            await _financialAssetRepository.AddAsync(asset);
            await _financialAssetRepository.SaveChangesAsync(); 
            //healthcheck
            return Ok(asset);
        }
        catch (SecureException ex)
        {
            var eventId = await _exceptionLogService.LogExceptionAsync(ex, Request);
            return StatusCode(500, new { type = "Secure", id = eventId, data = new { message = ex.Message } });
        }
        catch (Exception ex)
        {
            var eventId = await _exceptionLogService.LogExceptionAsync(ex, Request);
            return StatusCode(500, new { type = "Exception", id = eventId, data = new { message = $"Internal server error ID = {eventId}" } });
        }
    }
}

