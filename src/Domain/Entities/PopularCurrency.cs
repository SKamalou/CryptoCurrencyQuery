namespace CryptoCurrencyQuery.Domain.Entities;
public class PopularCurrency : BaseAuditableEntity
{
    public string Symbol { get; set; }
    public string? Title { get; set; }
}