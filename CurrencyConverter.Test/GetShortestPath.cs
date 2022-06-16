namespace CurrencyConverter.Test
{
    using FluentAssertions;
    using System;
    using Xunit;

    public class GetShortestPath
    {
        private readonly CurrencyExchange[] currencyExchangeTable = new CurrencyExchange[]
        {
            new CurrencyExchange("AUD", "CHF", 0.9661),
            new CurrencyExchange("jpy", "krw", 13.1151),
            new CurrencyExchange("EUR", "chf", 1.2053),
            new CurrencyExchange("aud", "JPY", 86.0305),
            new CurrencyExchange("eur", "usd", 1.2989),
            new CurrencyExchange("JPY", "INR", 0.6571),
        };

        [Fact]
        public void ItExists()
        {
            // Arrange
            var initialCurrency = new Currency("USD");
            var targetCurrency = new Currency("CHF");

            // Act & Assert
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);
        }

        [Fact]
        public void WhenConvertingUsDollarToEuro_ShouldReturnTwoCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("USD");
            var targetCurrency = new Currency("EUR");
            var expectedPath = new string[] { "USD", "EUR" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingEuroToUsDollar_ShouldReturnTwoCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("EUR");
            var targetCurrency = new Currency("USD");
            var expectedPath = new string[] { "EUR", "USD" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingAustralianDollarToSwissFranc_ShouldReturnTwoCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("AUD");
            var targetCurrency = new Currency("CHF");
            var expectedPath = new string[] { "AUD", "CHF" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingSwissFrancToAustralianDollar_ShouldReturnTwoCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("CHF");
            var targetCurrency = new Currency("AUD");
            var expectedPath = new string[] { "CHF", "AUD" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingEuroToAustralianDollar_ShouldReturnThreeCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("EUR");
            var targetCurrency = new Currency("AUD");
            var expectedPath = new string[] { "EUR", "CHF", "AUD" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingAustralianDollarToEuro_ShouldReturnThreeCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("AUD");
            var targetCurrency = new Currency("EUR");
            var expectedPath = new string[] { "AUD", "CHF", "EUR" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingUsDollarToAustralianDollar_ShouldReturnFourCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("USD");
            var targetCurrency = new Currency("AUD");
            var expectedPath = new string[] { "USD", "EUR", "CHF", "AUD" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingAustralianDollarToUsDollar_ShouldReturnFourCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("AUD");
            var targetCurrency = new Currency("USD");
            var expectedPath = new string[] { "AUD", "CHF", "EUR", "USD" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingUsDollarToJapaneseYen_ShouldReturnFiveCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("USD");
            var targetCurrency = new Currency("JPY");
            var expectedPath = new string[] { "USD", "EUR", "CHF", "AUD", "JPY" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingJapaneseYenToUsDollar_ShouldReturnFiveCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("JPY");
            var targetCurrency = new Currency("USD");
            var expectedPath = new string[] { "JPY", "AUD", "CHF", "EUR", "USD" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingUsDollarToSouthKoreanWon_ShouldReturnSixCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("USD");
            var targetCurrency = new Currency("KRW");
            var expectedPath = new string[] { "USD", "EUR", "CHF", "AUD", "JPY", "KRW" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Fact]
        public void WhenConvertingSouthKoreanWonToUsDollar_ShouldReturnSixCurrencyCodes()
        {
            // Arrange
            var initialCurrency = new Currency("KRW");
            var targetCurrency = new Currency("USD");
            var expectedPath = new string[] { "KRW", "JPY", "AUD", "CHF", "EUR", "USD" };

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().Equal(expectedPath);
        }

        [Theory]
        [InlineData("CNY", "USD")]
        [InlineData("USD", "CNY")]
        public void WhenNoConversionExists_ShouldReturnEmptyArray(string initialCurrencyCode, string targetCurrencyCode)
        {
            // Arrange
            var initialCurrency = new Currency(initialCurrencyCode);
            var targetCurrency = new Currency(targetCurrencyCode);

            // Act
            string[] path = Converter.GetShortestPath(currencyExchangeTable, initialCurrency, targetCurrency);

            // Assert
            path.Should().BeEmpty();
        }
    }
}
