using System.Text.Json.Serialization;

namespace LLServer.Models;

public class RankingData
{
    [JsonPropertyName("rank")] public required int Rank { get; set; }
    [JsonPropertyName("score")] public required int Score { get; set; }
    [JsonPropertyName("music_id")] public required int MusicId { get; set; }
}

public class MemberRanking
{
    [JsonPropertyName("rank")] public required int Rank { get; set; }
    [JsonPropertyName("score")] public required int Score { get; set; }
    [JsonPropertyName("character_id")] public required int CharacterId { get; set; }
}

public class MusicRanking : RankingData
{
    
}

public class CoopRanking : MemberRanking
{
    
}

public class YellRanking
{
    [JsonPropertyName("rank")] public required int Rank { get; set; }
    [JsonPropertyName("score")] public required int Score { get; set; }
    [JsonPropertyName("honor")] public required int Honor { get; set; }
    [JsonPropertyName("name")] public required string Name { get; set; } = "";
    [JsonPropertyName("tenpo_name")] public required string TenpoName { get; set; } = "";
    [JsonPropertyName("nameplate")] public required int Nameplate { get; set; }
    [JsonPropertyName("badge")] public required int Badge { get; set; }
    [JsonPropertyName("uid")] public required string Uid { get; set; } = "";
}