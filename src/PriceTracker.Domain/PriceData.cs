namespace PriceTracker.Domain
{
    public record PriceData
    {
        public decimal Price { get; init; }
        public decimal? CrossedOutPrice { get; init; }
        public decimal? DeclaredLowestPrice { get; init; }
        public bool IsOnPromotion { get; init; }
    }
}
