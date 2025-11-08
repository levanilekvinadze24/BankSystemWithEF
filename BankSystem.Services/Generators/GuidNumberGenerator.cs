using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;
public sealed class GuidNumberGenerator : IUniqueNumberGenerator
{
    public string Generate()
    {
        // Generate a new GUID
        var guid = Guid.NewGuid();

        // Convert to string without hyphens
        string guidString = guid.ToString("N");

        // Apply cryptographic hashing for additional security
        string secureHash = CryptoHelper.GenerateHash(guidString);

        // Return the first 32 characters (already guaranteed to be alphanumeric)
        // This maintains consistency with the original GUID length while adding crypto strength
        return secureHash.Substring(0, 32);
    }
}
