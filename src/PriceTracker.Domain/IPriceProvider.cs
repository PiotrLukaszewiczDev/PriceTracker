namespace PriceTracker.Domain
{
    public interface IPriceProvider
    {
        Task<PriceData?> GetPriceAsync(string url);
    }
}
