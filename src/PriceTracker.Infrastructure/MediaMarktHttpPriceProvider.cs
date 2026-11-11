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

        public async Task<PriceData?> GetPriceAsync(string url)
        {
            string html;
            try
            {
                html = await _httpClient.GetStringAsync(url);
            }
            catch (HttpRequestException)
            {
                return null;
            }
            throw new NotImplementedException();
        }
    }
}
