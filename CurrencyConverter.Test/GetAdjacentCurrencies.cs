namespace CurrencyConverter.Test
{
    using FluentAssertions;
    using Xunit;

    public class GetAdjacentCurrencies
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
            string currencyCode = "EUR";

            // Act & Assert
            string[] adjacentCurrencies = Converter.GetAdjacentCurrencies(currencyExchangeTable, currencyCode);
        }

        [Fact]
        public void WhenCurrencyIsEuro_ShouldReturnAdjacentCurrencies()
        {
            // Arrange
            string euro = "EUR";
            var expectedCurrencies = new string[] { "CHF", "USD" };

            // Act
            string[] adjacentCurrencies = Converter.GetAdjacentCurrencies(currencyExchangeTable, euro);

            // Assert
            adjacentCurrencies.Should().BeEquivalentTo(expectedCurrencies);
        }

        [Fact]
        public void WhenCurrencyIsAustralianDollar_ShouldReturnAdjacentCurrencies()
        {
            // Arrange
            string australianDollar = "AUD";
            var expectedCurrencies = new string[] { "CHF", "JPY" };

            // Act
            string[] adjacentCurrencies = Converter.GetAdjacentCurrencies(currencyExchangeTable, australianDollar);

            // Assert
            adjacentCurrencies.Should().BeEquivalentTo(expectedCurrencies);
        }

        [Fact]
        public void WhenCurrencyIsSwissFranc_ShouldReturnAdjacentCurrencies()
        {
            // Arrange
            string swissFranc = "CHF";
            var expectedCurrencies = new string[] { "AUD", "EUR" };

            // Act
            string[] adjacentCurrencies = Converter.GetAdjacentCurrencies(currencyExchangeTable, swissFranc);

            // Assert
            adjacentCurrencies.Should().BeEquivalentTo(expectedCurrencies);
        }

        [Fact]
        public void WhenCurrencyIsJapaneseYen_ShouldReturnAdjacentCurrencies()
        {
            // Arrange
            string japaneseYen = "JPY";
            var expectedCurrencies = new string[] { "KRW", "AUD", "INR" };

            // Act
            string[] adjacentCurrencies = Converter.GetAdjacentCurrencies(currencyExchangeTable, japaneseYen);

            // Assert
            adjacentCurrencies.Should().BeEquivalentTo(expectedCurrencies);
        }

        [Fact]
        public void WhenCurrencyIsSouthKoreanWon_ShouldReturnAdjacentCurrencies()
        {
            // Arrange
            string southKoreanWon = "KRW";
            var expectedCurrencies = new string[] { "JPY" };

            // Act
            string[] adjacentCurrencies = Converter.GetAdjacentCurrencies(currencyExchangeTable, southKoreanWon);

            // Assert
            adjacentCurrencies.Should().BeEquivalentTo(expectedCurrencies);
        }

        [Fact]
        public void WhenCurrencyIsUsDollar_ShouldReturnAdjacentCurrencies()
        {
            // Arrange
            string usDollar = "USD";
            var expectedCurrencies = new string[] { "EUR" };

            // Act
            string[] adjacentCurrencies = Converter.GetAdjacentCurrencies(currencyExchangeTable, usDollar);

            // Assert
            adjacentCurrencies.Should().BeEquivalentTo(expectedCurrencies);
        }

        [Fact]
        public void WhenCurrencyIsIndianRupee_ShouldReturnAdjacentCurrencies()
        {
            // Arrange
            string indianRupee = "INR";
            var expectedCurrencies = new string[] { "JPY" };

            // Act
            string[] adjacentCurrencies = Converter.GetAdjacentCurrencies(currencyExchangeTable, indianRupee);

            // Assert
            adjacentCurrencies.Should().BeEquivalentTo(expectedCurrencies);
        }
    }
}
