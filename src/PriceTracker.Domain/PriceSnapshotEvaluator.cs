namespace PriceTracker.Domain
{
    public class PriceSnapshotEvaluator
    {
        public Verdict Evaluate(IEnumerable<PriceSnapshot> snapshots)
        {
            PriceSnapshot? snapshot = snapshots.LastOrDefault();
            if (snapshot == null)
            {
                return Verdict.InsufficientData;
            }
            if (snapshot.IsOnPromotion == true && snapshot.DeclaredLowestPrice == null)
            {
                return Verdict.Suspicious;
            }
            if (snapshots.Min(s => s.Price) < snapshot.DeclaredLowestPrice)
            {
                return Verdict.Suspicious;
            }
            else
            {
                return Verdict.Fair;
            }

        }
    }
}
