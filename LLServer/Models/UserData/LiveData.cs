using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class LiveData
{
    [JsonPropertyName("m_live_id")]
    public int LiveId { get; set; } 

    [JsonPropertyName("select_count")]
    public int SelectCount { get; set; } 

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; } 

    [JsonPropertyName("new")]
    public bool New { get; set; } 

    [JsonPropertyName("full_combo")]
    public bool FullCombo { get; set; } 

    [JsonPropertyName("total_hi_score")]
    public int TotalHiScore { get; set; } 

    [JsonPropertyName("technical_hi_score")]
    public int TechnicalHiScore { get; set; } 

    [JsonPropertyName("technical_hi_rate")]
    public int TechnicalHiRate { get; set; } 

    [JsonPropertyName("coop_total_hi_score_2")]
    public int CoopTotalHiScore2 { get; set; } 

    [JsonPropertyName("coop_total_hi_score_3")]
    public int CoopTotalHiScore3 { get; set; }

    [JsonPropertyName("player_count_1")]
    public int PlayerCount1 { get; set; }

    [JsonPropertyName("player_count_2")]
    public int PlayerCount2 { get; set; } 

    [JsonPropertyName("player_count_3")]
    public int PlayerCount3 { get; set; } 

    [JsonPropertyName("rank_count_0")]
    public int RankCount0 { get; set; } 

    [JsonPropertyName("rank_count_1")]
    public int RankCount1 { get; set; } 

    [JsonPropertyName("rank_count_2")]
    public int RankCount2 { get; set; } 

    [JsonPropertyName("rank_count_3")]
    public int RankCount3 { get; set; } 

    [JsonPropertyName("rank_count_4")]
    public int RankCount4 { get; set; } 

    [JsonPropertyName("rank_count_5")]
    public int RankCount5 { get; set; } 

    [JsonPropertyName("rank_count_6")]
    public int RankCount6 { get; set; } 

    [JsonPropertyName("trophy_count_gold")]
    public int TrophyCountGold { get; set; } 

    [JsonPropertyName("trophy_count_silver")]
    public int TrophyCountSilver { get; set; } 

    [JsonPropertyName("trophy_count_bronze")]
    public int TrophyCountBronze { get; set; } 

    [JsonPropertyName("finale_count")]
    public int FinaleCount { get; set; } 

    [JsonPropertyName("technical_rank")]
    public int TechnicalRank { get; set; } 
}