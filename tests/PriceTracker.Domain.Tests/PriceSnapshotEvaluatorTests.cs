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

        [Fact]
        public void Evaluate_DeclaredLowestPriceGreaterThanPrice_ReturnsSuspicious()
        {
            // Arrange
            var snapshot = new PriceSnapshot
            {
                Price = 100,
                DeclaredLowestPrice = 150
            };
            var evaluator = new PriceSnapshotEvaluator();

            // Act
            var result = evaluator.Evaluate(new[] { snapshot });

            // Assert
            Assert.Equal(Verdict.Suspicious, result);
        }

        [Fact]
        public void Evaluate_DeclaredLowestPriceGreaterThanPriceForMoreSnapshots_ReturnsSuspicious()
        {
            // Arrange
            var snapshot1 = new PriceSnapshot
            {
                Price = 70
            };
            var snapshot2 = new PriceSnapshot
            {
                Price = 100,
                DeclaredLowestPrice = 90
            };
            var evaluator = new PriceSnapshotEvaluator();
            // Act
            var result = evaluator.Evaluate(new[] { snapshot1, snapshot2 });
            // Assert
            Assert.Equal(Verdict.Suspicious, result);
        }
    }
}
