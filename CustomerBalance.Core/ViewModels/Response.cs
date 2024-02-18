namespace CustomerBalance.Core.ViewModels;

public class Response<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}
