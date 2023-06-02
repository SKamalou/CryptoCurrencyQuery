namespace CryptoCurrencyQuery.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
}
public class CurrentUserService : ICurrentUserService
{
    public string? UserId => "1";
}
