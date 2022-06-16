namespace CurrencyConverter
{
    public class CurrencyExchange
    {
        public string InitialCurrencyCode { get; private set; }

        public string TargetCurrencyCode { get; private set; }

        public double Rate { get; private set; }

        public CurrencyExchange(string initialCurrencyCode, string targetCurrencyCode, double rate)
        {
            InitialCurrencyCode = initialCurrencyCode.ToUpper();
            TargetCurrencyCode = targetCurrencyCode.ToUpper();
            Rate = rate;
        }
    }
}
