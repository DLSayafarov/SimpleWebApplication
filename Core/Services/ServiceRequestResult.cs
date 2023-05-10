namespace Core.Services;

public class ServiceRequestResult
{
    public bool IsSuccessful { get; }
    public string Description { get; }

    public ServiceRequestResult(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }
    
    public ServiceRequestResult(bool isSuccessful, string description)
    {
        IsSuccessful = isSuccessful;
        Description = description;
    }
}