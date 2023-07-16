using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class TravelData
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("m_travel_pamphlet_id")]
    public int TravelPamphletId { get; set; } 

    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; } 

    [JsonPropertyName("m_card_memorial_id")]
    public int CardMemorialId { get; set; }

    //todo: double check the type of the data inside the positions array
    //always seems to be 3 items
    [JsonPropertyName("positions")]
    public int[] Positions { get; set; } = new int[3];

    [JsonPropertyName("last_landmark")]
    public int LastLandmark { get; set; } 

    [JsonPropertyName("modified")]
    public string Modified { get; set; } = "";
}