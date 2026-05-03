namespace InventoryManagement.UI.Helpers;

public static class JsonOptions
{
    public static readonly System.Text.Json.JsonSerializerOptions Default = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
    };
}