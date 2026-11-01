using PriceTracker.Domain;

namespace PriceTracker.Infrastructure
{
    public class FakePriceProvider : IPriceProvider
    {
        private readonly Dictionary<string, PriceData> _scenarios = new()
        {
            // Promo price equal to the declared 30-day minimum. Whether this is
            // fair or suspicious depends on the collected history, which only
            // the evaluator can judge - not something this provider decides.
            ["fake-promotion-matching-declared-minimum"] = new PriceData
            {
                Price = 79.99m,
                CrossedOutPrice = 99.99m,
                DeclaredLowestPrice = 79.99m,
                IsOnPromotion = true
            },

            // Rule 5: promotion with no declared minimum -> suspicious
            // (the only verdict rule decidable from a single snapshot)
            ["fake-promotion-no-declared-minimum"] = new PriceData
            {
                Price = 59.99m,
                CrossedOutPrice = 79.99m,
                DeclaredLowestPrice = null,
                IsOnPromotion = true
            },

            // Regular price, no promotion - baseline case
            ["fake-regular-price"] = new PriceData
            {
                Price = 49.99m,
                CrossedOutPrice = null,
                DeclaredLowestPrice = null,
                IsOnPromotion = false
            },
        };

        public Task<PriceData?> GetPriceAsync(string url)
        {
            if (_scenarios.TryGetValue(url, out var data))
            {
                return Task.FromResult<PriceData?>(data);
            }
            else
            {
                return Task.FromResult<PriceData?>(null);
            }
        }
    }
}
