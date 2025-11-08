using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts
{
    public sealed class GoldAccount : BankAccount
    {
        private const int GoldDepositCostPerPoint = 10;
        private const int GoldWithdrawCostPerPoint = 5;
        private const int GoldBalanceCostPerPoint = 5;

        public override decimal Overdraft => 3 * this.BonusPoints;

        // Primary constructor that matches test expectations
        public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
            : base(owner, currencyCode, new DelegateNumberGenerator(numberGenerator), 0m)
        {
        }

        // Constructor with initial balance
        public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
            : base(owner, currencyCode, new DelegateNumberGenerator(numberGenerator), initialBalance)
        {
        }

        // Keep interface-based constructors for other usage
        public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator generator)
            : this(owner, currencyCode, () => generator.Generate())
        {
        }

        public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator generator, decimal initialBalance)
            : this(owner, currencyCode, () => generator.Generate(), initialBalance)
        {
        }

        protected override int CalculateDepositRewardPoints(decimal amount)
        {
            decimal balancePoints = this.Balance / GoldBalanceCostPerPoint;
            decimal depositPoints = amount / GoldDepositCostPerPoint;
            return (int)Math.Ceiling(balancePoints) + (int)Math.Ceiling(depositPoints);
        }

        protected override int CalculateWithdrawRewardPoints(decimal amount)
        {
            decimal balancePoints = this.Balance / GoldBalanceCostPerPoint;
            decimal withdrawPoints = amount / GoldWithdrawCostPerPoint;
            return (int)Math.Ceiling(balancePoints) + (int)Math.Ceiling(withdrawPoints);
        }
    }

    public class DelegateNumberGenerator : IUniqueNumberGenerator
    {
        private readonly Func<string> _generator;

        public DelegateNumberGenerator(Func<string> generator)
        {
            this._generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public string Generate() => this._generator();
    }
}
