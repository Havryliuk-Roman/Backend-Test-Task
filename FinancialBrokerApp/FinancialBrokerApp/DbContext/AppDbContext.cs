namespace FinancialBrokerApp.DbContext;

using FinancialBrokerApp.Models;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<FinancialAsset> FinancialAssets { get; set; }
    public DbSet<ExceptionLog> ExceptionLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);       
    }
}