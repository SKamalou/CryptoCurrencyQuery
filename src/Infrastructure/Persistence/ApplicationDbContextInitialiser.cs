using CryptoCurrencyQuery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoCurrencyQuery.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.PopularCurrencies.Any())
        {
            _context.PopularCurrencies.AddRange(
                new PopularCurrency { Symbol = "USD", Title = "United States Dollar" },
                new PopularCurrency { Symbol = "EUR", Title = "Euro" },
                new PopularCurrency { Symbol = "BRL", Title = "Brazilian Real" },
                new PopularCurrency { Symbol = "GBP", Title = "Pound sterling" },
                new PopularCurrency { Symbol = "AUD", Title = "Australian Dollar" }
                );

            await _context.SaveChangesAsync();
        }
    }
}