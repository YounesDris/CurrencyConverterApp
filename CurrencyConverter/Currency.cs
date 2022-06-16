namespace CurrencyConverter
{
    public class Currency
    {
        public string CurrencyCode { get; private set; }

        public Currency(string currencyCode)
        {
            CurrencyCode = currencyCode.ToUpper();
        }
    }
}
