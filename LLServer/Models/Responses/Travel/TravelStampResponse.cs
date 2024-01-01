using System.Text.Json.Serialization;
using LLServer.Models.Travel;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses.Travel;

public class TravelStampResponse : ResponseBase
{
    //todo: figure out why it seems to only want travel_history and not travel_history_aqours or w/e
    [JsonPropertyName("travel_history")]
    public TravelHistory[] TravelHistory { get; set; } = Array.Empty<TravelHistory>();
}
