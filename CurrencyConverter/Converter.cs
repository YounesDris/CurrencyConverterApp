namespace CurrencyConverter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Converter
    {
        public static int Convert(
            CurrencyExchange[] currencyExchangeTable,
            Currency initialCurrency,
            Currency targetCurrency,
            int amount)
        {
            HashSet<string> currencyCodes = GetAllCurrencyCodes(currencyExchangeTable);
            int convertedAmount = default;

            if (currencyCodes.Contains(initialCurrency.CurrencyCode) && currencyCodes.Contains(targetCurrency.CurrencyCode))
            {
                string[] shortestPath = GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

                if (shortestPath.Length > 0)
                {
                    double interAmount = amount;

                    for (int i = 0; i < shortestPath.Length - 1; i++)
                    {
                        interAmount = UnitConvert(currencyExchangeTable, shortestPath[i], shortestPath[i + 1], interAmount);
                    }

                    convertedAmount = (int)Math.Round(interAmount, 0);
                }
            }

            return convertedAmount;
        }

        public static double UnitConvert(
            CurrencyExchange[] currencyExchangeTable,
            string initialCurrencyCode,
            string targetCurrencyCode,
            double amount)
        {
            double exchangeRate = GetExchangeRate(currencyExchangeTable, initialCurrencyCode, targetCurrencyCode);
            return Math.Round(amount * exchangeRate, 4);
        }

        public static string[] GetAdjacentCurrencies(CurrencyExchange[] currencyExchangeTable, string currencyCode)
        {
            // Not optimized for performance
            string[] adjacentCurrencies = currencyExchangeTable
                .Where(ce => ce.InitialCurrencyCode == currencyCode)
                .Select(ce => ce.TargetCurrencyCode)
                .Union(currencyExchangeTable
                    .Where(ce => ce.TargetCurrencyCode == currencyCode)
                    .Select(ce => ce.InitialCurrencyCode))
                .ToArray();

            return adjacentCurrencies;
        }

        public static string[] GetShortestPath(
            CurrencyExchange[] currencyExchangeTable,
            Currency initialCurrency,
            Currency targetCurrency)
        {
            var path = new List<string>();
            Dictionary<string, string> previousCurrencies = FindPreviousCurrencyFromCurrentOne(currencyExchangeTable, initialCurrency.CurrencyCode, targetCurrency.CurrencyCode);

            if (previousCurrencies.Count > 0)
            {
                if (previousCurrencies.ContainsKey(targetCurrency.CurrencyCode))
                {
                    string currentCurrency = targetCurrency.CurrencyCode;
                    path.Add(currentCurrency);

                    while (previousCurrencies[currentCurrency] != initialCurrency.CurrencyCode)
                    {
                        string previousCurrency = previousCurrencies[currentCurrency];
                        path.Add(previousCurrency);
                        currentCurrency = previousCurrency;
                    }

                    path.Add(initialCurrency.CurrencyCode);
                    path.Reverse();
                }
            }

            return path.ToArray();
        }

        private static HashSet<string> GetAllCurrencyCodes(CurrencyExchange[] currencyExchangeTable)
        {
            ISet<string> initialCurrencyCodes = currencyExchangeTable
                .Select(ce => ce.InitialCurrencyCode)
                .ToHashSet();

            ISet<string> targetCurrencyCodes = currencyExchangeTable
                .Select(ce => ce.TargetCurrencyCode)
                .ToHashSet();

            HashSet<string> currencyCodes = initialCurrencyCodes
                .Concat(targetCurrencyCodes)
                .ToHashSet();

            return currencyCodes;
        }

        private static double GetExchangeRate(
            CurrencyExchange[] currencyExchangeTable,
            string initialCurrencyCode,
            string targetCurrencyCode)
        {
            // Not optimized for performance
            foreach (CurrencyExchange ce in currencyExchangeTable)
            {
                if (ce.InitialCurrencyCode == initialCurrencyCode
                    && ce.TargetCurrencyCode == targetCurrencyCode)
                {
                    return Math.Round(ce.Rate, 4);
                }

                if (ce.InitialCurrencyCode == targetCurrencyCode
                    && ce.TargetCurrencyCode == initialCurrencyCode)
                {
                    return Math.Round(1 / ce.Rate, 4);
                }
            }

            return default;
        }

        private static Dictionary<string, string> FindPreviousCurrencyFromCurrentOne(
            CurrencyExchange[] currencyExchangeTable,
            string initialCurrencyCode,
            string targetCurrencyCode)
        {
            var nextCurrenciesToExplore = new List<string>() { initialCurrencyCode };
            var reachedCurrencies = new List<string>() { initialCurrencyCode };
            var previousCurrencies = new Dictionary<string, string>();

            while (nextCurrenciesToExplore.Count > 0)
            {
                string currentCurrency = nextCurrenciesToExplore[0];
                nextCurrenciesToExplore.RemoveAt(0);

                IEnumerable<string> adjacentCurrencies = GetAdjacentCurrencies(currencyExchangeTable, currentCurrency)
                    .Except(reachedCurrencies);

                foreach (string currency in adjacentCurrencies)
                {
                    reachedCurrencies.Add(currency);
                    previousCurrencies.Add(currency, currentCurrency);

                    if (currency == targetCurrencyCode)
                    {
                        nextCurrenciesToExplore.Clear();
                        break;
                    }

                    nextCurrenciesToExplore.Add(currency);
                }
            }

            return previousCurrencies;
        }
    }
}
