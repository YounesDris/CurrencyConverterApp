namespace CurrencyConverter.Test
{
    using FluentAssertions;
    using Xunit;

    public class Convert
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
            int amount = 550;

            // Act & Assert
            int convertedAmount = Converter.Convert(currencyExchangeTable, initialCurrency, targetCurrency, amount);
        }

        [Fact]
        public void WhenConverting_550_EuroToJapaneseYen_ShouldReturnConvertedAndRoundedAmount()
        {
            // Arrange
            var initialCurrency = new Currency("EUR");
            var targetCurrency = new Currency("JPY");
            int amount = 550;
            int expectedAmount = 59033;

            // Act
            int convertedAmount = Converter.Convert(currencyExchangeTable, initialCurrency, targetCurrency, amount);

            // Assert
            convertedAmount.Should().Be(expectedAmount);
        }

        [Theory]
        [InlineData("CNY", "USD")]
        [InlineData("USD", "CNY")]
        public void WhenNoConversionExists_ShouldReturnZero(string initialCurrencyCode, string targetCurrencyCode)
        {
            // Arrange
            var initialCurrency = new Currency(initialCurrencyCode);
            var targetCurrency = new Currency(targetCurrencyCode);
            int amount = 550;
            int expectedAmount = 0;

            // Act
            int convertedAmount = Converter.Convert(currencyExchangeTable, initialCurrency, targetCurrency, amount);

            // Assert
            convertedAmount.Should().Be(expectedAmount);
        }
    }
}
