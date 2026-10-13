using Xunit;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGivers.Data;
using GiftOfTheGivers.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiftOfTheGivers.Tests
{
    public class DonationIntegrationTests
    {
        [Fact]
        public async Task DonationFlow_FromFormToDatabase_PersistsSuccessfully()
        {
            // Arrange: Set up an in-memory SQLite database context for isolated integration testing
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using var context = new ApplicationDbContext(options);
            await context.Database.OpenConnectionAsync();
            await context.Database.EnsureCreatedAsync();

            // Simulate a donation submission payload coming from the form/controller
            var donation = new Donation
            {
                Amount = 1500.00m,
                Currency = "ZAR",
                DonationType = "One-Time",
                DonationDate = DateTime.UtcNow,
                Anonymous = false
            };

            // Act: Save the donation to the database context (simulating the form -> function -> database workflow)
            context.Donations.Add(donation);
            await context.SaveChangesAsync();

            // Assert: Retrieve the record back from the database and verify persistence
            var savedDonation = await context.Donations.FirstOrDefaultAsync(d => d.Amount == 1500.00m);
            
            Assert.NotNull(savedDonation);
            Assert.Equal("ZAR", savedDonation.Currency);
            Assert.Equal("One-Time", savedDonation.DonationType);
            Assert.False(savedDonation.Anonymous);
        }
    }
}