namespace PriceTracker.Domain
{
    public class PriceSnapshot
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal? CrossedOutPrice { get; set; }
        public decimal? DeclaredLowestPrice { get; set; }
        public bool IsOnPromotion { get; set; }
        public DateTimeOffset CapturedAt { get; set; }
    }
}
