namespace CryptoCurrencyQuery.Domain.Entities;
public class PopularCurrency : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Symbol { get; set; }
}