using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace BankSystem.Services.Generators
{
    public sealed class SimpleGenerator : IUniqueNumberGenerator
    {
        private int _lastNumber = 1234567890;
        private static readonly Lazy<SimpleGenerator> _instance =
            new Lazy<SimpleGenerator>(() => new SimpleGenerator());

        public static SimpleGenerator Instance => _instance.Value;

        private SimpleGenerator() { }

        public string Generate()
        {
            // Increment the counter
            this._lastNumber++;

            // Turn into text with invariant culture
            var raw = this._lastNumber.ToString(CultureInfo.InvariantCulture);

            // Use SHA256 instead of MD5 for security (CA5351)
            byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(raw));

            // Convert to hex string
            var sb = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                _ = sb.Append(b.ToString("x2", CultureInfo.InvariantCulture));
            }

            // Truncate the result to 32 characters
            return sb.ToString().Substring(0, 32);
        }

    }
}
