using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class AchievementRecordBook
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("values")]
    public int[] Values { get; set; } = Array.Empty<int>();
}