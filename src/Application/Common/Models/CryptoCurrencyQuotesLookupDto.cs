﻿using CryptoCurrencyQuery.Domain.ValueObjects;

namespace CryptoCurrencyQuery.Application.Common.Models;
public record CryptoCurrencyQuotesLookupDto(CurrencySymbol SourceCryptoCurrencySymbol, IEnumerable<CurrencySymbol> TargeCurrencySymbols)
{
    public virtual bool Equals(CryptoCurrencyQuotesLookupDto? other)
    {
        return
            other != null &&
            SourceCryptoCurrencySymbol == other.SourceCryptoCurrencySymbol &&
            TargeCurrencySymbols.OrderBy(a => a.Symbol).SequenceEqual(other.TargeCurrencySymbols.OrderBy(a => a.Symbol));
    }

    public override int GetHashCode()
    {
        var hash = $"{SourceCryptoCurrencySymbol}_{string.Join("_", TargeCurrencySymbols?.OrderBy(a => a.Symbol)?.Select(t => t.ToString()))}";
        return hash.GetHashCode();
    }
}