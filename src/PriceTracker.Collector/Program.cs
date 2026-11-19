using Microsoft.EntityFrameworkCore;
using PriceTracker.Domain;
using PriceTracker.Infrastructure;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("PriceTrackerBot/1.0");
        var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PriceTracker;Trusted_Connection=True;TrustServerCertificate=True";
        var dbOptionsBuilder = new DbContextOptionsBuilder<PriceTrackerDbContext>();
        dbOptionsBuilder.UseSqlServer(connectionString);
        DbContextOptions<PriceTrackerDbContext> options = dbOptionsBuilder.Options;
        using var priceTrackerDbContext = new PriceTrackerDbContext(options);
        IPriceProvider priceProvider = new MediaMarktHttpPriceProvider(httpClient);

        var products = await priceTrackerDbContext.Products.ToListAsync();
        foreach (var product in products)
        {
            var priceData = await priceProvider.GetPriceAsync(product.Url);
            if (priceData == null)
            {
                continue;
            }

            var snapshot = new PriceSnapshot
            {
                ProductId = product.Id,
                Price = priceData.Price,
                CrossedOutPrice = priceData.CrossedOutPrice,
                DeclaredLowestPrice = priceData.DeclaredLowestPrice,
                IsOnPromotion = priceData.IsOnPromotion,
                CapturedAt = DateTimeOffset.UtcNow
            };

            priceTrackerDbContext.PriceSnapshots.Add(snapshot);
            await priceTrackerDbContext.SaveChangesAsync();
        }
    }
}
