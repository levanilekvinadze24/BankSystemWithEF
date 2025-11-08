using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public sealed class SilverAccount : BankAccount
{
    private const int SilverDepositCostPerPoint = 5;
    private const int SilverWithdrawCostPerPoint = 2;
    private const int SilverBalanceCostPerPoint = 100;

    public override decimal Overdraft => 2 * this.BonusPoints;

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : this(owner, currencyCode, uniqueNumberGenerator, 0m)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, () => uniqueNumberGenerator.Generate(), initialBalance)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        decimal balancePoints = this.Balance / SilverBalanceCostPerPoint;
        decimal depositPoints = amount / SilverDepositCostPerPoint;
        return Math.Max((int)Math.Floor(balancePoints) + (int)Math.Floor(depositPoints), 0);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        decimal balancePoints = this.Balance / SilverBalanceCostPerPoint;
        decimal withdrawPoints = amount / SilverWithdrawCostPerPoint;  // Changed from Deposit to Withdraw
        return Math.Max((int)Math.Floor(balancePoints) + (int)Math.Floor(withdrawPoints), 0);
    }
}
