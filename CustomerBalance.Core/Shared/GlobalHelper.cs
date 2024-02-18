using CustomerBalance.Core.ViewModels;

namespace CustomerBalance.Core.Shared;

public static class GlobalHelper
{
    // generate a random string upper case and numbers
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }

    // generate random payment reference
    public static string GeneratePaymentReference()
    {
        return $"PAY-{DateTime.Now:yyyyMMddHHmmss}-{GenerateRandomString(5)}";
    }

    // generate random invoice reference
    public static string GenerateInvoiceReference()
    {
        return $"INV-{DateTime.Now:yyyyMMddHHmmss}-{GenerateRandomString(5)}";
    }

    // generate random customer reference
    public static string GenerateCustomerReference()
    {
        return $"CUS-{DateTime.Now:yyyyMMddHHmmss}-{GenerateRandomString(5)}";
    }

    // generate a method that will take TransactionType and return the transaction name, if Credit return Payment, if Debit return Invoice
    public static string GetTransactionName(TransactionType transactionType)
    {
        return transactionType switch
        {
            TransactionType.Credit => "Payment",
            TransactionType.Debit => "Invoice",
            _ => "Unknown"
        };
    }

    public static SetDocument GetDocument(string sub, string title)
    {
        return new SetDocument
        {
            Author = "Pandora Technologies Ltd.",
            CreationDate = DateTime.UtcNow,
            Creator = "Philemon Sackey",
            Keywords = "Pandora Reports, Company Reports, Amazing Reports",
            Subject = sub,
            Title = title
        };
    }

    public static Response<CompanyDetail> GetCompany()
    {
        var model = new CompanyDetail();
        model.Name = "Pandora Software Consulting Ltd.";
        model.Address = "P.O. Box 1234, Accra, Ghana";
        model.Contact = "0241234567";
        model.Website = "www.pscgh.com";
        model.Branch = "Head Office";

        return new Response<CompanyDetail>
        {
            Success = false,
            Data = model,
            Message = null
        };
    }
}
