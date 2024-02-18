namespace CustomerBalance.Core.ViewModels;

public class ReportModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? CustomerId { get; set; }
}
