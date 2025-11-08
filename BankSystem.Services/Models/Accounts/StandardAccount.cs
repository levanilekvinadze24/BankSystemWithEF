using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public sealed class StandardAccount : BankAccount
{
    private const int StandardBalanceCostPerPoint = 100;

    public override decimal Overdraft => 0m;

    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : this(owner, currencyCode, uniqueNumberGenerator, 0m)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal v)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return this.CalculatePoints();
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return this.CalculatePoints();
    }

    private int CalculatePoints()
    {
        decimal points = this.Balance / StandardBalanceCostPerPoint;
        return Math.Max((int)Math.Floor(points), 0);
    }
}
