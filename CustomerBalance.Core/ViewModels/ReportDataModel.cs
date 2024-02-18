namespace CustomerBalance.Core.ViewModels;

public class ReportDataModel
{
    public string? Title { get; set; }
    public string? Query { get; set; }
    public SetMargin? Margin { get; set; }
    public SetDocument? Document { get; set; }
    public CompanyDetail? Company { get; set; }
}

public class ReportDataActual<T>
{
    public T? Details { get; set; }

    public int Count { get; set; }
}

public class FinanceModel
{
    public string? Remarks { get; set; }
    public decimal Amount { get; set; }
    public string? Customer { get; set; }
    public string? Type { get; set; }
    public string? Number { get; set; }
    public string? Date { get; set; }
    public Guid? Id { get; set; }
}

public class SetMargin
{
    public float Top { get; set; }
    public float Down { get; set; }
    public float Right { get; set; }
    public float Left { get; set; }
    public bool IsLandscape { get; set; }
    public bool IsSameMargin { get; set; }
    public float Width { get; set; }
    public float Margin { get; set; }
}

public class SetDocument
{
    public string? Author { get; set; }
    public DateTime? CreationDate { get; set; }
    public string? Creator { get; set; }
    public string? Keywords { get; set; }
    public string? Subject { get; set; }
    public string? Title { get; set; }
}

public class CompanyDetail
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Contact { get; set; }
    public string? Website { get; set; }
    public string? Branch { get; set; }
    public string? Image { get; set; }
}
