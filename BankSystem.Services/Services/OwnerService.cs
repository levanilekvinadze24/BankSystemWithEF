using BankSystem.EF.Entities;
using BankSystem.Services.Models;

namespace BankSystem.Services.Services
{
    public class OwnerService : BaseService, IDisposable
    {
        private bool disposed;

        public OwnerService(BankContext context) : base(context) { }

        public IReadOnlyList<AccountOwnerTotalBalanceModel> GetAccountOwnersTotalBalance()
        {
            var result = (from ba in this.Context.BankAccounts
                          join ao in this.Context.AccountOwners on ba.AccountOwnerId equals ao.AccountOwnerId
                          join cc in this.Context.CurrencyCodes on ba.CurrencyCodeId equals cc.CurrencyCodeId
                          group ba by new { ao.AccountOwnerId, ao.FirstName, ao.LastName, cc.Code } into g
                          select new AccountOwnerTotalBalanceModel
                          {
                              AccountOwnerId = g.Key.AccountOwnerId,
                              FirstName = g.Key.FirstName,
                              LastName = g.Key.LastName,
                              CurrencyCode = g.Key.Code,
                              Total = (decimal)g.Sum(x => (double)x.Balance)
                          })
                          .OrderByDescending(x => x.Total)
                          .ToList();

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }
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
