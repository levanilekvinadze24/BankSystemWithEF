using System.Globalization;
using System.Text.RegularExpressions;

namespace BankSystem.Services.Helpers
{
    public static class ValidatorService
    {
        /// <summary>
        /// Checks if the specified currency is valid by comparing against known ISO currency codes
        /// </summary>
        /// <param name="currency">The currency code to validate (e.g. "USD", "EUR")</param>
        /// <returns>True if the currency is valid, false otherwise</returns>
        public static bool IsCurrencyValid(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            {
                return false;
            }

            // Get all cultures to check their currencies
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                try
                {
                    RegionInfo region = new RegionInfo(culture.Name);
                    if (region.ISOCurrencySymbol.Equals(currency, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                catch (ArgumentException)
                {
                    // Some cultures might not have region information
                    // redundant continue removed
                }
            }

            return false;
        }

        /// <summary>
        /// Verifies that a specified email is in a correct format
        /// </summary>
        /// <param name="email">The email address to validate</param>
        /// <returns>True if the email format is valid, false otherwise</returns>
        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Use simple regex pattern for basic email validation
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
