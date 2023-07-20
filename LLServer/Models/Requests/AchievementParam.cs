using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Requests;
public class AchievementParam
{
    [JsonPropertyName("record_books")]
    public AchievementRecordBook[] RecordBooks { get; set; } = Array.Empty<AchievementRecordBook>();
}