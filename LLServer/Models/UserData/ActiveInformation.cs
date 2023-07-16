using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class ActiveInformation
{
    [JsonPropertyName("id")]
    public int Id { get; set; } = 0;
}