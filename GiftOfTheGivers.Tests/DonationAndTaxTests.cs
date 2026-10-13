using Xunit;
using GiftOfTheGivers.Helpers;
using System;

namespace GiftOfTheGivers.Tests
{
    public class DonationAndTaxTests
    {
        [Fact]
        public void TestCertificateFormatting()
        {
            // Arrange
            int donationId = 456;
            DateTime date = new DateTime(2026, 10, 13);

            // Act
            string certNumber = DonationHelper.GenerateTaxCertificateNumber(donationId, date);

            // Assert
            Assert.Equal("GOTG-20261013-000456", certNumber);
        }

        [Fact]
        public void TestDonationTotalCalculation()
        {
            // Arrange
            decimal baseAmount = 2500.00m;
            decimal additional = 500.00m;

            // Act
            decimal total = DonationHelper.CalculateDonationTotal(baseAmount, additional);

            // Assert
            Assert.Equal(3000.00m, total);
        }

        [Fact]
        public void TestNegativeAmountThrowsException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                DonationHelper.CalculateDonationTotal(-50m, 10m);
            });
        }
    }
}