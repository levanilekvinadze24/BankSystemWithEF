using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.EF.Entities;

[Table("account_cash_operation")]
public class AccountCashOperation
{
    [Key]
    [Column("account_cash_operation_id")]
    public int AccountCashOperationId { get; set; }

    [Column("bank_account_id")]
    public int BankAccountId { get; set; }

    [ForeignKey("BankAccountId")]
    public BankAccount BankAccount { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("operation_date_time")]
    public DateTime OperationDateTime { get; set; }

    [Column("note")]
    public string Note { get; set; }
}
