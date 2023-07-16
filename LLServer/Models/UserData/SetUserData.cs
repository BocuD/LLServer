using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

//will be filled with the data from param in a userdata.set post request
//mark as JsonDerivedType so it can be deserialized into the correct type
public class SetUserData
{
    [JsonPropertyName("equipskill")] public EquipSkill[]? EquipSkills { get; set; }
    [JsonPropertyName("member_yell")] public MemberYell[]? MemberYells { get; set; }
    
    //same format as UserDataResponse
    [JsonPropertyName("userdata")] public UserData? UserData { get; set; } = new();
    [JsonPropertyName("userdata_aqours")] public UserDataAqours? UserDataAqours { get; set; }
    [JsonPropertyName("userdata_saintsnow")] public UserDataSaintSnow? UserDataSaintSnow { get; set; }
}

public class EquipSkill
{
    [JsonPropertyName("camera")] public int Camera { get; set; }
    [JsonPropertyName("character_id")] public int CharacterId { get; set; }
    [JsonPropertyName("m_card_member_id")] public int CardMemberId { get; set; }
    [JsonPropertyName("m_card_memorial_id")] public int CardMemorialId { get; set; }
    [JsonPropertyName("main")] public int Main { get; set; }
    [JsonPropertyName("stage")] public int Stage { get; set; }
}

public class MemberYell
{
    [JsonPropertyName("character_id")] public int CharacterId { get; set; }
    [JsonPropertyName("yell_point")] public int YellPoint { get; set; }
    [JsonPropertyName("yell_rank")] public int YellRank { get; set; }
}