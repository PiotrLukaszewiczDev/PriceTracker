using PriceTracker.Domain;

namespace PriceTracker.Domain.Tests
{
    public class PriceSnapshotEvaluatorTests
    {
        [Fact]
        public void Evaluate_PromotionWithoutDeclaredLowestPrice_ReturnsSuspicious()
        {
            // Arrange
            var snapshot = new PriceSnapshot
            {
                IsOnPromotion = true,
                DeclaredLowestPrice = null
            };
            var evaluator = new PriceSnapshotEvaluator();

            // Act
            var result = evaluator.Evaluate(new[] { snapshot });

            // Assert
            Assert.Equal(Verdict.Suspicious, result);
        }

        [Fact]
        public void Evaluate_EmptyHistory_ReturnsInsufficientData()
        {
            // Arrange
            var snapshot = Array.Empty<PriceSnapshot>();
            var evaluator = new PriceSnapshotEvaluator();

            // Act
            var result = evaluator.Evaluate(snapshot);

            // Assert
            Assert.Equal(Verdict.InsufficientData, result);

        }
    }
}
