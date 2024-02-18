using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerBalance.Core.Shared;

namespace CustomerBalance.Server.Models;

public class Transaction : IEntity
{
    [Key]
    public Guid? Id { get; set; }

    public DateTime? DateCreated { get; set; }
    public bool IsActive { get; set; }

    [Required]
    public string? Remarks { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string? Number { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    [Required]
    public DateTime? Date { get; set; }
    [Required]
    public Guid? CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
    // get type of transaction
    [Required]
    public TransactionType Type { get; set; }
    public string? GetTransactionType()
    {
        return Type == TransactionType.Credit ? "Credit" : "Debit";
    }
}
