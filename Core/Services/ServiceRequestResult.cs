namespace Core.Services;

public class ServiceRequestResult
{
    public bool IsSuccessful { get; }
    public string Description { get; }
    
    public ServiceRequestResult(bool isSuccessful, string description = "")
    {
        IsSuccessful = isSuccessful;
        Description = description;
    }
}

public class ServiceRequestResult<T>
{
    public bool IsSuccessful { get; }
    public string Description { get; }
    public T Result { get; }
    
    public ServiceRequestResult(bool isSuccessful, string description = "")
    {
        IsSuccessful = isSuccessful;
        Description = description;
    }
    
    public ServiceRequestResult(bool isSuccessful, T result, string description = "")
    {
        IsSuccessful = isSuccessful;
        Result = result;
        Description = description;
    }
    
    public ServiceRequestResult(T result, string description = "")
    {
        IsSuccessful = true;
        Result = result;
        Description = description;
    }
}