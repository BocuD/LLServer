using System.Text.Json.Serialization;

namespace LLServer.Models.Requests;

public class AuthParam
{
    [JsonPropertyName("guest_flag")]
    public int GuestFlag { get; set; }

    [JsonPropertyName("nesicaid")]
    public string NesicaId { get; set; } = "7020392000000000";
    
    [JsonPropertyName("physical_nesicaid")]
    public string PhysicalNesicaId { get; set; } = "7020392000000000";
}