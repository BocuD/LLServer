using System.Text.Json.Serialization;
using LLServer.Models.Travel;

namespace LLServer.Models.Responses.Travel;

public class TravelStartResponse : ResponseBase
{
    [JsonPropertyName("other_players")]
    public TravelOtherPlayerData[] OtherPlayers { get; set; } = new TravelOtherPlayerData[0];
}