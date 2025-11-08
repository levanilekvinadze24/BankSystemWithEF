namespace BankSystem.Services.Models;
public sealed class AccountCashOperation
{
    public decimal Amount { get; }
    public DateTime Date { get; }
    public string Note { get; }

    public AccountCashOperation(decimal amount, DateTime date, string note)
    {
        this.Amount = amount;
        this.Date = date;
        this.Note = note ?? throw new ArgumentNullException(nameof(note), "Note cannot be null.");
    }

    public override string ToString()
    {
        string action = this.Amount >= 0 ? "Credited to account" : "Debited from account";
        return $"{this.Date:MM/dd/yyyy HH:mm:ss} {this.Note} : {action} {this.Amount}.";
    }
}
