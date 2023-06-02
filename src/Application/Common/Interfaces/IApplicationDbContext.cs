using CryptoCurrencyQuery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoCurrencyQuery.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<PopularCurrency> PopularCurrencies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
