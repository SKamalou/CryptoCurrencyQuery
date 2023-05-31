using CryptoCurrencyQuery.Application.Common.Interfaces;

namespace CryptoCurrencyQuery.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
