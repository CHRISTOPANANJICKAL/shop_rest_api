namespace WebApplication1.utils;

public abstract class Utils
{
    public static int? ValidId(string id)
    {
        var parseSuccess = int.TryParse(id, out var parsedId);
        if (!parseSuccess || parsedId < 1 || parsedId >= int.MaxValue) return null;
        return parsedId;
    }
}