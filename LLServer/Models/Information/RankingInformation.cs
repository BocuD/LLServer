using System.Text.Json.Serialization;

namespace LLServer.Models.Information;

public class RankingInformation
{
    [JsonPropertyName("yell_rankings")]
    public List<InformationYellRanking> YellRankings { get; set; } = new();
}

public class InformationYellRanking
{
    [JsonPropertyName("month")]
    public int Month { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    
    [JsonPropertyName("yell_ranking_id")] //32 chars
    public string YellRankingId { get; set; } = string.Empty;
}