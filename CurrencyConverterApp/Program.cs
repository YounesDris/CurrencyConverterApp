namespace CurrencyConverterApp
{
    using CurrencyConverter;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string message = "Input data is invalid";

            if (IsInputDataValid(input))
            {
                string[] inputs = input.Split(";");

                var initialCurrency = new Currency(inputs[0]);
                var targetCurrency = new Currency(inputs[2]);
                int amount = int.Parse(inputs[1]);

                int numExchangeRates = int.Parse(Console.ReadLine());
                var currencyExchangeTable = new CurrencyExchange[numExchangeRates];

                for (int i = 0; i < numExchangeRates; i++)
                {
                    string exchangeRate = Console.ReadLine();

                    if (IsExchangeRateValid(exchangeRate))
                    {
                        string[] exchangeRates = exchangeRate.Split(";");
                        currencyExchangeTable[i] = new CurrencyExchange(exchangeRates[0], exchangeRates[1], double.Parse(exchangeRates[2], CultureInfo.InvariantCulture));
                        continue;
                    }

                    Array.Clear(currencyExchangeTable, 0, currencyExchangeTable.Length);
                    break;
                }

                if (currencyExchangeTable.Length > 0)
                {
                    int convertedAmount = Converter.Convert(currencyExchangeTable, initialCurrency, targetCurrency, amount);
                    message = convertedAmount != 0 ? convertedAmount.ToString() : "No conversion found";
                }
            }

            Console.WriteLine(message);
        }

        // The case where the amount is given in the form: 000550 is not taken into account
        // The case where the both currency code is identical is not taken into account
        private static bool IsInputDataValid(string input)
        {
            var rx = new Regex(@"^[a-zA-Z]{3};[1-9]\d*;[a-zA-Z]{3}$");
            return rx.IsMatch(input);
        }

        // The case where the exchange rate is equal to zero (0.0000) is not taken into account
        // The case where the both currency code is identical is not taken into account
        private static bool IsExchangeRateValid(string exchangeRate)
        {
            var rx = new Regex(@"^[a-zA-Z]{3};[a-zA-Z]{3};\d+\.\d{4}$");
            return rx.IsMatch(exchangeRate);
        }
    }
}
