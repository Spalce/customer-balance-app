using System.ComponentModel.DataAnnotations;
using CustomerBalance.Core.Shared;

namespace CustomerBalance.Server.Models;

public class Customer : IEntity
{
    [Key]
    public Guid? Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public bool IsActive { get; set; }
    [Required(ErrorMessage = "Number is required")]
    [Display(Name = "Customer Number")]
    [StringLength(50, MinimumLength = 2)]
    public string? Number { get; set; }
    [Required(ErrorMessage = "First is required")]
    [Display(Name = "First Name")]
    [StringLength(50, MinimumLength = 2)]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Last is required")]
    [Display(Name = "Last Name")]
    [StringLength(50, MinimumLength = 2)]
    public string? LastName { get; set; }
    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(50, MinimumLength = 2)]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Phone Number is required")]
    [Display(Name = "Phone Number")]
    [StringLength(10)]
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
    // crate a method to get the full name of the customer
    public string? GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
    public List<Transaction>? Transactions { get; set; }
    // get the total credit of the customer
    public decimal? GetTotalCredit()
    {
        return Transactions?
            .Where(t => t.Type == TransactionType.Credit)
            .Sum(t => t.Amount) ?? 0;
    }

    // get the total debit of the customer
    public decimal? GetTotalDebit()
    {
        return Transactions?
            .Where(t => t.Type == TransactionType.Debit)
            .Sum(t => t.Amount) ?? 0;
    }

    // get the balance of the customer
    public decimal? GetBalance()
    {
        return GetTotalCredit() - GetTotalDebit();
    }

}
