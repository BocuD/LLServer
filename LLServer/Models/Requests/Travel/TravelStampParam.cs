using System.Text.Json.Serialization;

namespace LLServer.Models.Requests.Travel;

/*
{
    "param": {
        "travel_history_ids": [
            "1"
        ]
    },
    "protocol": "travelstamp",
}
 */

public class TravelStampParam
{
    [JsonPropertyName("travel_history_ids")]
    public string[] TravelHistoryIds { get; set; } = Array.Empty<string>();
}