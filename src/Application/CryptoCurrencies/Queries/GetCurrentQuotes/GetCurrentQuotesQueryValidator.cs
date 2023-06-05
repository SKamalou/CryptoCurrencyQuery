using FluentValidation;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public class GetCurrentQuotesQueryValidator : AbstractValidator<GetCurrentQuotesQuery>
{
    public GetCurrentQuotesQueryValidator()
    {
        RuleFor(x => x.Symbol)
            .NotNull()
            .WithMessage("Symbol is required.");
    }
}