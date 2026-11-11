using PriceTracker.Domain;

namespace PriceTracker.Infrastructure
{
    public class MediaMarktHttpPriceProvider : IPriceProvider
    {
        private readonly HttpClient _httpClient;
        public MediaMarktHttpPriceProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<PriceData?> GetPriceAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}
