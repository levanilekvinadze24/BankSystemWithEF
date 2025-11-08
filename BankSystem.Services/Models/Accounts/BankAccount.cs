using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts
{
    public abstract class BankAccount
    {
        public string Number => this.AccountNumber;
        public string AccountNumber { get; }
        public AccountOwner AccountOwner { get; }
        public string CurrencyCode { get; }
        public decimal Balance { get; protected set; }
        public int BonusPoints { get; protected set; }
        public abstract decimal Overdraft { get; }

        private readonly List<string> operations = [];

        protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator numberGenerator, decimal initialBalance = 0m)
        {
            this.AccountOwner = owner ?? throw new ArgumentNullException(nameof(owner));
            this.CurrencyCode = currencyCode ?? throw new ArgumentNullException(nameof(currencyCode));
            this.AccountNumber = (numberGenerator ?? throw new ArgumentNullException(nameof(numberGenerator))).Generate();
            this.Balance = initialBalance;
        }

        protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance = 0m)
            : this(owner, currencyCode, new DelegateNumberGenerator(numberGenerator), initialBalance)
        {
        }

        public void InitializeInitialDeposit(decimal amount)
        {
            if (amount > 0)
            {
                this.Balance += amount;
                int points = this.CalculateDepositRewardPoints(amount);
                this.BonusPoints += points;
                this.operations.Add($"Initial deposit: {amount} {this.CurrencyCode}");
            }
        }

        public IReadOnlyList<string> GetAllOperations() => this.operations.AsReadOnly();

        public void Deposit(decimal amount, DateTime date, string description)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive", nameof(amount));
            }

            this.Balance += amount;
            int points = this.CalculateDepositRewardPoints(amount);
            this.BonusPoints += points;
            this.operations.Add($"{date:MM/dd/yyyy HH:mm:ss} {description} : Credited to account {amount}.");
        }

        public void Withdraw(decimal amount, DateTime date, string description)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));
            }

            if (amount > this.Balance + this.Overdraft)
            {
                throw new InvalidOperationException("Withdrawal amount exceeds available balance including overdraft.");
            }

            this.Balance -= amount;
            int points = this.CalculateWithdrawRewardPoints(amount);
            this.BonusPoints += points;
            this.operations.Add($"{date:MM/dd/yyyy HH:mm:ss} {description} : Debited from account {amount}.");
        }

        protected abstract int CalculateDepositRewardPoints(decimal amount);
        protected abstract int CalculateWithdrawRewardPoints(decimal amount);

        public override string ToString()
        {
            return $"{this.AccountOwner.FirstName} {this.AccountOwner.LastName}, {this.AccountOwner.Email}. No:{this.AccountNumber}. Balance: {this.Balance}{this.CurrencyCode}.";
        }

        private sealed class DelegateNumberGenerator : IUniqueNumberGenerator
        {
            private readonly Func<string> _generator;

            public DelegateNumberGenerator(Func<string> generator)
            {
                this._generator = generator ?? throw new ArgumentNullException(nameof(generator));
            }

            public string Generate() => this._generator();
        }
    }
}
