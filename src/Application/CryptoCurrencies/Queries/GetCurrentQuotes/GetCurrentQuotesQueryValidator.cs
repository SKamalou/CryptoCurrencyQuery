using FluentValidation;

namespace CryptoCurrencyQuery.Application.CryptoCurrencies.Queries.GetCurrentQuotes;
public class GetCurrentQuotesQueryValidator : AbstractValidator<GetCurrentQuotesQuery>
{
    public GetCurrentQuotesQueryValidator()
    {
        RuleFor(x => x.Symbol)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(10)
            .WithMessage("Symbol is required.");
    }
}