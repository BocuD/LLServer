using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class InitializeUserData
{
    [JsonPropertyName("userdata")]
    public UserData? UserData { get; set; }

    [JsonPropertyName("userdata_aqours")]
    public UserDataAqours? UserDataAqours { get; set; }
}