using System.Text.Json.Serialization;

namespace LLServer.Models.Responses.Terminal;

public class TravelPrintResponse : ResponseBase
{
    [JsonPropertyName("travel_snap_id")]
    public string TravelSnapId { get; set; }
    
    [JsonPropertyName("created")] //datetime
    public string Created { get; set; } = string.Empty;
}