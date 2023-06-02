using CryptoCurrencyQuery.Application.Common.Models;

namespace CryptoCurrencyQuery.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
}

public class IdentityService : IIdentityService
{
    public Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        return Task.FromResult(true);
    }

    public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        return Task.FromResult((new Result(true, null), "1"));
    }

    public Task<Result> DeleteUserAsync(string userId)
    {
        return Task.FromResult(new Result(true, null));
    }

    public Task<string?> GetUserNameAsync(string userId)
    {
        return Task.FromResult("1");
    }

    public Task<bool> IsInRoleAsync(string userId, string role)
    {
        return Task.FromResult(true);
    }
}
