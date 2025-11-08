using BankSystem.EF.Entities;

namespace BankSystem.Services.Services;

public abstract class BaseService
{
    protected BankContext Context { get; }

    protected BaseService(BankContext context)
    {
        this.Context = context;
    }
}
