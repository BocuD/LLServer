using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class InitializeUserData
{
    [JsonPropertyName("userdata")]
    public NullableUserData? UserData { get; set; }

    [JsonPropertyName("userdata_aqours")]
    public NullableUserDataBase? UserDataAqours { get; set; }
    
    [JsonPropertyName("userdata_saintsnow")]
    public NullableUserDataBase? UserDataSaintSnow { get; set; }
}