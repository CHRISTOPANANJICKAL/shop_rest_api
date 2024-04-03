namespace WebApplication1.Models;

public class DataBaseResult<T>(T? data, bool success, string message)
{
    public readonly T? Data = data;
    public readonly string Message = message;
    public readonly bool Success = success;
}