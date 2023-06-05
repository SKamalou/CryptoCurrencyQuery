using CryptoCurrencyQuery.Domain.ValueObjects;

namespace CryptoCurrencyQuery.Application.UnitTests.CryptoCurrencies;
internal class Constants
{
    public const string USD = "USD";
    public const string EUR = "EUR";

    public const string BTC = "BTC";
    public const string ETH = "ETH";
    public const string BNB = "BNB";

    public const double BTCtoUSD = 123.456;
    public const double BTCtoEUR = 0.00789;

    public static CurrencySymbol USDSymbol = new CurrencySymbol(USD);
    public static CurrencySymbol EURSymbol = new CurrencySymbol(EUR);
    public static CurrencySymbol BTCSymbol = new CurrencySymbol(BTC);
    public static CurrencySymbol ETHSymbol = new CurrencySymbol(ETH);
    public static CurrencySymbol BNBSymbol = new CurrencySymbol(BNB);
}