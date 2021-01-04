namespace Code
{
    public class Invoice
    {
        public readonly string Customer;
        public readonly Performance[] Performances;

        public Invoice(string customer, Performance[] performances)
        {
            Customer = customer;
            Performances = performances;
        }
    }
}