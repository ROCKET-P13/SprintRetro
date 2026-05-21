using System.Text.Json;

namespace SprintRetroAPI.Serialization;

public static class AppJsonSerializerOptions
{
    public static readonly JsonSerializerOptions ApplicationDefault = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
    };
}
