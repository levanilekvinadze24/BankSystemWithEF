using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.EF.Entities;
[Table("currency_code")]
public class CurrencyCode
{
    [Key]
    [Column("currency_code_id")]
    public int CurrencyCodeId { get; set; }

    [Column("currency_code")]
    public string Code { get; set; }

    public ICollection<BankAccount> BankAccounts { get; set; }
}
