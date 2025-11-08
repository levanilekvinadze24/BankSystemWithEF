using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.EF.Entities;

[Table("bank_account")]
public class BankAccount
{
    [Key]
    [Column("bank_account_id")]
    public int BankAccountId { get; set; }

    [Column("account_owner_id")]
    public int AccountOwnerId { get; set; }

    [ForeignKey(nameof(AccountOwnerId))]
    public AccountOwner AccountOwner { get; set; }

    [Column("account_number"), Required]
    public string AccountNumber { get; set; }

    [Column("balance")]
    public decimal Balance { get; set; }

    [Column("currency_code_id")]
    public int CurrencyCodeId { get; set; }

    [ForeignKey(nameof(CurrencyCodeId))]
    public CurrencyCode CurrencyCode { get; set; }

    [Column("bonus_points")]
    public int BonusPoints { get; set; }

    [Column("overdraft")]
    public decimal Overdraft { get; set; }

    // 1) Use the target-typed `new()` initializer to satisfy IDE0028
    // 2) Drop the incorrect `[ForeignKey]` on a collection nav—EF will pick up the FK from the
    //    other side (AccountCashOperation.BankAccountId) automatically
    // 3) Give it a private setter so it can’t be overridden in a constructor (avoids CA2214)
    public ICollection<AccountCashOperation> AccountCashOperations { get; private set; } = [];
}