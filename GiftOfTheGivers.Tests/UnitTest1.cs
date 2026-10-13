using Xunit;
using GiftOfTheGivers.Helpers;
namespace GiftOfTheGivers.Helpers
{
    public static class DonationHelper
    {
        public static string GenerateTaxCertificateNumber(int donationId, DateTime date)
        {
            return $"GOTG-{date:yyyyMMdd}-{donationId:000000}";
        }

        public static decimal CalculateDonationTotal(decimal amount, decimal additionalContribution = 0m)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Donation amount cannot be negative.");
            if (additionalContribution < 0) throw new ArgumentOutOfRangeException(nameof(additionalContribution), "Additional contribution cannot be negative.");
            
            return amount + additionalContribution;
        }
    }
}