using System.Text.Json.Serialization;

namespace LLServer.Models.Requests.Travel;

/*
 "param": {
    "character_id": 4,
    "m_travel_pamphlet_id": 201,
    "map_id": 201,
    "mas_max": 60,
    "slot": 0
  },
 */

public class TravelStartParam
{
    [JsonPropertyName("character_id")] public int CharacterId { get; set; }
    [JsonPropertyName("m_travel_pamphlet_id")] public int TravelPamphletId { get; set; }
    [JsonPropertyName("map_id")] public int MapId { get; set; }
    [JsonPropertyName("mas_max")] public int MasMax { get; set; }
    [JsonPropertyName("slot")] public int Slot { get; set; }
}