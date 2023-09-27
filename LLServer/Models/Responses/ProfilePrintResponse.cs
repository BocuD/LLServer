using System.Text.Json.Serialization;

namespace LLServer.Models.Responses;

public class ProfilePrintResponse : ResponseBase
{
    [JsonPropertyName("profile_card_id")]
    public string ProfileCardId { get; set; } = "";
    
    [JsonPropertyName("created")]
    public string Created { get; set; } = ""; //datetime
}