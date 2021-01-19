namespace Code
{
    public class StatementData
    {
        public readonly string Customer;
        public readonly PerformanceData[] Performances;
        public readonly int TotalAmount;
        public readonly int TotalVolumeCredits;

        public StatementData(string customer, PerformanceData[] performances, int totalAmount,
                             int totalVolumeCredits)
        {
            Customer = customer;
            Performances = performances;
            TotalAmount = totalAmount;
            TotalVolumeCredits = totalVolumeCredits;
        }
    }
}