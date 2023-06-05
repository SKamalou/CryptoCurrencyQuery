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
                new PopularCurrency { Symbol = "USD" }
                //new PopularCurrency { Symbol = "EUR" },
                //new PopularCurrency { Symbol = "BRL" },
                //new PopularCurrency { Symbol = "GBP" },
                //new PopularCurrency { Symbol = "AUD" }
                );

            await _context.SaveChangesAsync();
        }
    }
}