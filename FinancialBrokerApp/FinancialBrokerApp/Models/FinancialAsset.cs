namespace FinancialBrokerApp.Models;

public record FinancialAsset
{
    public int Id { get; init; } 
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public int PortfolioId { get; set; }

    public FinancialAsset? Parent { get; set; }
    public List<FinancialAsset>? Children { get; set; } = new List<FinancialAsset>();
}