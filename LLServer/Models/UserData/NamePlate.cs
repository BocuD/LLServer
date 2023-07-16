using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class NamePlate
{
    [JsonPropertyName("m_nameplate_id")]
    public int NamePlateId { get; set; } 

    [JsonPropertyName("new")]
    public bool New { get; set; } 
}