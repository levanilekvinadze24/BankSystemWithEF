using BankSystem.Services.Models.Accounts;
namespace BankSystem.Services.Models
{
    public sealed class AccountOwner
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        private readonly List<BankAccount> _accounts = [];

        public AccountOwner(string firstName, string lastName, string email)
        {
            VerifyString(firstName, nameof(firstName));
            VerifyString(lastName, nameof(lastName));
            VerifyEmail(email);

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        public void Add(BankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Bank account cannot be null.");
            }

            if (!this._accounts.Contains(account))
            {
                this._accounts.Add(account);
            }
        }

        public IReadOnlyList<BankAccount> Accounts() => this._accounts.AsReadOnly();

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}, {this.Email}.";
        }

        private static void VerifyString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{paramName} cannot be null or empty.");
            }
        }

        private static void VerifyEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@', StringComparison.Ordinal))
            {
                throw new ArgumentException("Invalid email format.", nameof(email));
            }
        }
    }
}
