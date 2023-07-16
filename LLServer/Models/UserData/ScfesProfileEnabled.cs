using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class ScfesProfileEnabled : ScfesProfile
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "Test";

    [JsonPropertyName("unit_id")]
    public int UnitId { get; set; } = 0;

    [JsonPropertyName("award_id")]
    public int AwardId { get; set; } = 0;

    [JsonPropertyName("rank_up_flag")]
    public int RankUpFlag { get; set; } = 0;

    [JsonPropertyName("invalid_name")]
    public bool InvalidName { get; set; } = false;

    [JsonPropertyName("live_list")]
    public int[] LiveList { get; set; } = Array.Empty<int>();
}