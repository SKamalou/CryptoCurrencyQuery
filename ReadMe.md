# Crypto Currency Solution

This application first gets a list of all cryptocurrencies from CoinMarketCap using the [Listings API](https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest). Since changes in the cryptocurrency list rarely happen, it caches the list for up to 2 hours and does not query that API again within that time frame (which is adjustable).

You can then select a symbol from that list, and it retrieves the price for that cryptocurrency in 5 common currencies by calling the [Quotes API](https://pro-api.coinmarketcap.com/v2/cryptocurrency/quotes/latest?symbol=BTC&convert=EUR) for each currency (the list of supported currencies is stored in the database).

Naturally, we know that it's not efficient to call an API for each currency, and I spent a lot of time finding a better solution. However, the free plan from CoinMarketCap only allows you to convert a cryptocurrency to one currency per request, but paid plans allow you to get the price for a cryptocurrency in multiple currencies with one request.

Alternative solution:
There was another solution to reduce the number of API calls made. First, we can use the CoinMarketCap Quotes API above to get the price of the desired cryptocurrency in euros from CoinMarketCap. Then, we can use the [Exchangerates API](http://api.exchangeratesapi.io/latest?access_key=[YourKey]&base=EUR&symbols=GBP,USD,BRL,AUD) to get the conversion rate of euros to other currencies from exchangeratesapi with one API call. (Exchangeratesapi only allows EUR conversion with other currencies in the free plan)

Then, based on the result, we can calculate the price of the desired cryptocurrency in other currencies ourselves. This way, we can achieve our desired result with just two API requests.
However, in my opinion, this solution has two problems. First, we become dependent on two different APIs, and if either of them encounters a problem, our site will face issues. Second, we are getting data from two different sites and do not have a specific reference for prices, which could be problematic from a business perspective, and users may not receive consistent prices.


## Technologies

* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 7](https://docs.microsoft.com/en-us/ef/core/)
* [React 18.2](https://react.dev/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [Refit](https://github.com/reactiveui/refit)
* [Polly](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)


## Getting Started
1. Install the latest [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Download this Repository
4. Run `dotnet run` in WebUI folder

For testing APIs, there is the possibility to use Swagger which is accessible through the menu at the top of the application.

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebUI

This layer is a single page application based on Angular 14 and ASP.NET Core 7. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.
