using FluentValidation;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public class GetCurrentQuotesQueryValidator : AbstractValidator<GetCurrentQuotesQuery>
{
    public GetCurrentQuotesQueryValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty().WithMessage("Symbol is required.");
    }
}