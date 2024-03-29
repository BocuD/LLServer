﻿using System.Text.Json.Serialization;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class SetUserDataParam
{
    [JsonPropertyName("equipskill")]
    public EquipSkill[]? EquipSkills { get; set; }

    [JsonPropertyName("member_yell")]
    public MemberYell[]? MemberYells { get; set; }

    //same format as UserDataResponse
    [JsonPropertyName("userdata")]
    public NullableUserData? UserData { get; set; }

    [JsonPropertyName("userdata_aqours")]
    public NullableUserDataBase? UserDataAqours { get; set; }

    [JsonPropertyName("userdata_saintsnow")]
    public NullableUserDataBase? UserDataSaintSnow { get; set; }
}

public class EquipSkill
{
    [JsonPropertyName("camera")]
    public int Camera { get; set; }

    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }

    [JsonPropertyName("m_card_member_id")]
    public int CardMemberId { get; set; }

    [JsonPropertyName("m_card_memorial_id")]
    public int CardMemorialId { get; set; }

    [JsonPropertyName("main")]
    public int Main { get; set; }

    [JsonPropertyName("stage")]
    public int Stage { get; set; }
}

public class MemberYell
{
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }

    [JsonPropertyName("yell_point")]
    public int YellPoint { get; set; }

    [JsonPropertyName("yell_rank")]
    public int YellRank { get; set; }
}