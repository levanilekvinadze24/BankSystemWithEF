using BankSystem.Services.Helpers;
using System.Globalization;

namespace BankSystem.Services.Generators;

public sealed class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    private readonly DateTime _startingPoint;

    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this._startingPoint = startingPoint;
    }

    public string Generate()
    {
        // Calculate elapsed ticks since starting point
        TimeSpan elapsed = DateTime.UtcNow - this._startingPoint;
        long ticks = elapsed.Ticks;

        // Convert ticks to string and hash it
        string tickString = ticks.ToString(CultureInfo.InvariantCulture);
        string hashedTicks = CryptoHelper.GenerateHash(tickString);

        return hashedTicks;
    }
}
