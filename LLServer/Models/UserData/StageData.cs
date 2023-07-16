using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class StageData
{
    [JsonPropertyName("m_stage_id")]
    public int StageId { get; set; } 
    
    [JsonPropertyName("select_count")]
    public int SelectCount { get; set; } 

    [JsonPropertyName("unlocked")]
    public bool Unlocked { get; set; } 

    [JsonPropertyName("new")]
    public bool New { get; set; } 

    public static readonly int[] Stages =
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110,
        111, 112, 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312
    };
}