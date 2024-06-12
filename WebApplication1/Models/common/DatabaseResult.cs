using System.Text.Json.Serialization;


namespace WebApplication1.Models.common;

public class DataBaseResult<T>(T? data, bool success, string? message, int statusCode, List<string>? errors = null)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; } = data;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Errors { get; set; } = errors;
    public string Message { get; set; } = message ?? "";
    public int StatusCode { get; set; } = statusCode;
    public bool Success { get; set; } = success;
}