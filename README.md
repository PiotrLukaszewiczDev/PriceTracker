# PriceTracker

A web application that tracks product prices in online stores and checks whether advertised discounts are genuine.

## Why

Under the EU **Omnibus Directive** (in force in Poland since January 2023), a store announcing a price reduction must display the **lowest price from the 30 days before the reduction**. In practice, stores sometimes:

- raise the price shortly before a "sale", so the discount is not real,
- declare a "lowest 30-day price" that does not match the actual price history,
- change the declared lowest price from day to day while the sale price stays the same.

PriceTracker builds its own price history for selected products and compares it with what the store declares, so each promotion gets a verdict: **fair**, **suspicious**, or **insufficient data**.

## Status

Work in progress, built incrementally (MVP first).

## Planned stack

- ASP.NET Core (.NET 10) Web API, Razor/MVC frontend
- EF Core + SQL Server
- Scheduled price collection (BackgroundService)
- xUnit, Moq, integration tests with WebApplicationFactory
- Serilog, Docker Compose, GitHub Actions, Azure App Service

## Data collection

Prices are collected at a low request rate, only from publicly available product pages, respecting each store's `robots.txt` and terms of service. This is a non-commercial educational project.

## Roadmap

1. [x] Solution skeleton
2. [ ] Domain model + promotion verdict logic (TDD)
3. [ ] Persistence (EF Core, SQL Server)
4. [ ] Price collection from the first store (Morele)
5. [ ] Scheduled collection
6. [ ] REST API
7. [ ] Razor frontend with price history chart
8. [ ] Docker, CI/CD, integration tests
9. [ ] Azure deployment