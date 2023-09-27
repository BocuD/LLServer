using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Responses;

public class ProfileInquiryResponse : ResponseBase
{
    [JsonPropertyName("owner_id")]
    public ulong OwnerId { get; set; }
    
    [JsonPropertyName("first_relation")]
    public int FirstRelation { get; set; }
    
    [JsonPropertyName("userdata")]
    public UserDataBase UserData { get; set; } = new();

    [JsonPropertyName("userdata_aqours")]
    public UserDataAqours UserDataAqours { get; set; } = new();

    [JsonPropertyName("userdata_saintsnow")]
    public UserDataSaintSnow? UserDataSaintSnow { get; set; }
    
    [JsonPropertyName("game")]
    public GameInformation GameInformation { get; set; } = new();
}

public class GameInformation : GameHistoryBase
{
    [JsonPropertyName("game_version")]
    public int GameVersion { get; set; }
    
    [JsonPropertyName("profile_version")]
    public int ProfileVersion { get; set; }
}