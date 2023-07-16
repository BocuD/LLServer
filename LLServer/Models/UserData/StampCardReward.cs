using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class StampCardReward
{
    [JsonPropertyName("stamp_card_id")]
    public int StampCardId { get; set; } 

    [JsonPropertyName("m_card_member_id")]
    public int CardMemberId { get; set; } 

    [JsonPropertyName("trade_coin")]
    public bool TradeCoin { get; set; } 
}