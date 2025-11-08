using BankSystem.EF.Entities;
using BankSystem.Services.Models;

namespace BankSystem.Services.Services
{
    public class AccountService : BaseService, IDisposable
    {
        private bool disposed;

        public AccountService(BankContext context) : base(context) { }

        public IReadOnlyList<BankAccountFullInfoModel> GetBankAccountsFullInfo()
        {
            var result = (from ba in this.Context.BankAccounts
                          join ao in this.Context.AccountOwners on ba.AccountOwnerId equals ao.AccountOwnerId
                          join cc in this.Context.CurrencyCodes on ba.CurrencyCodeId equals cc.CurrencyCodeId
                          select new BankAccountFullInfoModel
                          {
                              BankAccountId = ba.BankAccountId,
                              FirstName = ao.FirstName,
                              LastName = ao.LastName,
                              AccountNumber = ba.AccountNumber,
                              Balance = ba.Balance,
                              CurrencyCode = cc.Code,
                              BonusPoints = ba.BonusPoints
                          }).ToList();

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    this.Context.Dispose();
                }
                // free unmanaged resources here if any

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
