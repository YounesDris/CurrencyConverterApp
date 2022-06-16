namespace CurrencyConverter.Test
{
    using FluentAssertions;
    using System;
    using Xunit;

    public class UnitConvert
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
            string initialCurrencyCode = "USD";
            string targetCurrencyCode = "CHF";
            double amount = 550;

            // Act & Assert
            double convertedAmount = Converter.UnitConvert(currencyExchangeTable, initialCurrencyCode, targetCurrencyCode, amount);
        }

        [Theory]
        [InlineData("AUD", "CHF", 550, 0.9661)]
        [InlineData("JPY", "KRW", 550, 13.1151)]
        [InlineData("EUR", "CHF", 550, 1.2053)]
        [InlineData("AUD", "JPY", 550, 86.0305)]
        [InlineData("EUR", "USD", 550, 1.2989)]
        [InlineData("JPY", "INR", 550, 0.6571)]
        public void WhenConvertingInLinearDirection_ShouldReturnConvertedAndRoundedAmount(string initialCurrency, string targetCurrency, double amount, double exchangeRate)
        {
            // Arrange
            double expectedAmount = Math.Round(amount * exchangeRate, 4);

            // Act
            double convertedAmount = Converter.UnitConvert(currencyExchangeTable, initialCurrency, targetCurrency, amount);

            // Assert
            convertedAmount.Should().Be(expectedAmount);
        }

        [Theory]
        [InlineData("CHF", "AUD", 550, 0.9661)]
        [InlineData("KRW", "JPY", 550, 13.1151)]
        [InlineData("CHF", "EUR", 550, 1.2053)]
        [InlineData("JPY", "AUD", 550, 86.0305)]
        [InlineData("USD", "EUR", 550, 1.2989)]
        [InlineData("INR", "JPY", 550, 0.6571)]
        public void WhenConvertingInReverseDirection_ShouldReturnConvertedAndRoundedAmount(string initialCurrencyCode, string targetCurrencyCode, double amount, double reverseExchangeRate)
        {
            // Arrange
            double expectedAmount = Math.Round(amount * Math.Round(1 / reverseExchangeRate, 4), 4);

            // Act
            double convertedAmount = Converter.UnitConvert(currencyExchangeTable, initialCurrencyCode, targetCurrencyCode, amount);

            // Assert
            convertedAmount.Should().Be(expectedAmount);
        }

        [Fact]
        public void WhenNoConversionExists_ShouldReturnZero()
        {
            // Arrange
            string initialCurrency = "EUR";
            string targetCurrency = "JPY";
            double amount = 550;
            double expectedAmount = 0;

            // Act
            double convertedAmount = Converter.UnitConvert(currencyExchangeTable, initialCurrency, targetCurrency, amount);

            // Assert
            convertedAmount.Should().Be(expectedAmount);
        }
    }
}
