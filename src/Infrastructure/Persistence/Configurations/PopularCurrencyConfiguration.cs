using CryptoCurrencyQuery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoCurrencyQuery.Infrastructure.Persistence.Configurations;

internal class PopularCurrencyConfiguration : IEntityTypeConfiguration<PopularCurrency>
{
    public void Configure(EntityTypeBuilder<PopularCurrency> builder)
    {
        builder.Property(t => t.Symbol)
            .HasMaxLength(10)
            .IsRequired();
    }
}