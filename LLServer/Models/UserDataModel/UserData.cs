using System.Text.Json.Serialization;

namespace LLServer.Models.UserDataModel;

public class UserData
{
    //returned directly by game in userdata.initialize
    [JsonPropertyName("character_id")] public int CharacterId { get; set; } = 0;
    [JsonPropertyName("idol_kind")] public int IdolKind { get; set; } = 0;
    [JsonPropertyName("name")] public string Name { get; set; } = "Test";
    [JsonPropertyName("note_speed_level")] public int NoteSpeedLevel { get; set; } = 0;
    [JsonPropertyName("submonitor_type")] public int SubMonitorType { get; set; } = 0;
    [JsonPropertyName("volume_bgm")] public int VolumeBgm { get; set; } = 0;
    [JsonPropertyName("volume_se")] public int VolumeSe { get; set; } = 0;
    [JsonPropertyName("volume_voice")] public int VolumeVoice { get; set; } = 0;
    
    //from decompiled method parsePlayerDataJson
    [JsonPropertyName("tenpo_name")] public string TenpoName { get; set; } = "Test";
    [JsonPropertyName("play_date")] public string PlayDate { get; set; } = "2021-01-01 00:00:00";
    [JsonPropertyName("play_satellite")] public int PlaySatellite { get; set; } = 0;
    [JsonPropertyName("play_center")] public int PlayCenter { get; set; } = 0;
    [JsonPropertyName("level")] public int Level { get; set; } = 0;
    [JsonPropertyName("total_exp")] public int TotalExp { get; set; } = 0;
    [JsonPropertyName("honor")] public int Honor { get; set; } = 0;
    [JsonPropertyName("badge")] public int Badge { get; set; } = 0;
    [JsonPropertyName("nameplate")] public int Nameplate { get; set; } = 0;
    
    //TODO: profile card ids seem to be parsed as 32 character strings, look into this and test it (.text:000000014021D08E and .text:000000014021BE00)
    [JsonPropertyName("profile_card_id_1")] public int ProfileCardId1 { get; set; } = 0;
    [JsonPropertyName("profile_card_id_2")] public int ProfileCardId2 { get; set; } = 0;
    
    [JsonPropertyName("credit_count_satellite")] public int CreditCountSatellite { get; set; } = 0;
    [JsonPropertyName("credit_count_center")] public int CreditCountCenter { get; set; } = 0;
    [JsonPropertyName("play_ls4")] public int PlayLs4 { get; set; } = 0;
}

public class UserDataAqours
{
    [JsonPropertyName("character_id")] public int CharacterId { get; set; } = 0;
    [JsonPropertyName("honor")] public int Honor { get; set; } = 0;
    [JsonPropertyName("badge")] public int Badge { get; set; } = 0;
    [JsonPropertyName("nameplate")] public int Nameplate { get; set; } = 0;
    [JsonPropertyName("profile_card_id_1")] public int ProfileCardId1 { get; set; } = 0;
    [JsonPropertyName("profile_card_id_2")] public int ProfileCardId2 { get; set; } = 0;
}

public class UserDataSaintSnow : UserDataAqours
{
    
}

public class MemberData
{
    [JsonPropertyName("character_id")] public int CharacterId { get; set; } = 0;
    [JsonPropertyName("m_card_member_id")] public int CardMemberId { get; set; } = 0;
    [JsonPropertyName("yell_point")] public int YellPoint { get; set; } = 0;
    [JsonPropertyName("m_card_memorial_id")] public int CardMemorialId { get; set; } = 0;
    [JsonPropertyName("achieve_rank")] public int AchieveRank { get; set; } = 0;
    [JsonPropertyName("main")] public int Main { get; set; } = 0;
    [JsonPropertyName("camera")] public int Camera { get; set; } = 0;
    [JsonPropertyName("stage")] public int Stage { get; set; } = 0;
    [JsonPropertyName("select_count")] public int SelectCount { get; set; } = 0;
    [JsonPropertyName("new")] public bool New { get; set; } = true;
}

public class MemberCardData
{
    [JsonPropertyName("m_card_member_id")] public int CardMemberId { get; set; } = 0;
    [JsonPropertyName("count")] public int Count { get; set; } = 0;
    [JsonPropertyName("print_rest")] public int PrintRest { get; set; } = 0;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class SkillCardData
{
    [JsonPropertyName("m_card_skill_id")] public int CardSkillId { get; set; } = 0;
    [JsonPropertyName("skill_level")] public int SkillLevel { get; set; } = 0;
    [JsonPropertyName("print_rest")] public int PrintRest { get; set; } = 0;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class MusicData
{
    [JsonPropertyName("music_id")] public required int MusicId { get; set; } = 0;
    [JsonPropertyName("unlocked")] public required bool Unlocked { get; set; } = false;
    [JsonPropertyName("new")] public required bool New { get; set; } = false;
}

public class LiveData
{
    [JsonPropertyName("m_live_id")] public int LiveId { get; set; } = 0;
    [JsonPropertyName("select_count")] public int SelectCount { get; set; } = 0;
    [JsonPropertyName("unlocked")] public bool Unlocked { get; set; } = false;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
    [JsonPropertyName("full_combo")] public bool FullCombo { get; set; } = false;
    [JsonPropertyName("total_hi_score")] public int TotalHiScore { get; set; } = 0;
    [JsonPropertyName("technical_hi_score")] public int TechnicalHiScore { get; set; } = 0;
    [JsonPropertyName("technical_hi_rate")] public int TechnicalHiRate { get; set; } = 0;
    [JsonPropertyName("coop_total_hi_score_2")] public int CoopTotalHiScore2 { get; set; } = 0;
    [JsonPropertyName("coop_total_hi_score_3")] public int CoopTotalHiScore3 { get; set; } = 0;
    [JsonPropertyName("player_count_1")] public int PlayerCount1 { get; set; } = 0;
    [JsonPropertyName("player_count_2")] public int PlayerCount2 { get; set; } = 0;
    [JsonPropertyName("player_count_3")] public int PlayerCount3 { get; set; } = 0;
    [JsonPropertyName("rank_count_0")] public int RankCount0 { get; set; } = 0;
    [JsonPropertyName("rank_count_1")] public int RankCount1 { get; set; } = 0;
    [JsonPropertyName("rank_count_2")] public int RankCount2 { get; set; } = 0;
    [JsonPropertyName("rank_count_3")] public int RankCount3 { get; set; } = 0;
    [JsonPropertyName("rank_count_4")] public int RankCount4 { get; set; } = 0;
    [JsonPropertyName("rank_count_5")] public int RankCount5 { get; set; } = 0;
    [JsonPropertyName("rank_count_6")] public int RankCount6 { get; set; } = 0;
    [JsonPropertyName("trophy_count_gold")] public int TrophyCountGold { get; set; } = 0;
    [JsonPropertyName("trophy_count_silver")] public int TrophyCountSilver { get; set; } = 0;
    [JsonPropertyName("trophy_count_bronze")] public int TrophyCountBronze { get; set; } = 0;
    [JsonPropertyName("finale_count")] public int FinaleCount { get; set; } = 0;
    [JsonPropertyName("technical_rank")] public int TechnicalRank { get; set; } = 0;
}

public class StageData
{
    [JsonPropertyName("m_stage_id")] public int StageId { get; set; } = 0;
    [JsonPropertyName("select_count")] public int SelectCount { get; set; } = 0;
    [JsonPropertyName("unlocked")] public bool Unlocked { get; set; } = false;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class MailBoxItem
{
    [JsonPropertyName("id")] public int Id { get; set; } = 0;
    [JsonPropertyName("attrib")] public int Attrib { get; set; } = 0;
    [JsonPropertyName("category")] public int Category { get; set; } = 0;
    [JsonPropertyName("item_id")] public int ItemId { get; set; } = 0;
    [JsonPropertyName("count")] public int Count { get; set; } = 0;
}

public class Mission
{
    [JsonPropertyName("mission_id")] public int MissionId { get; set; } = 0;
    [JsonPropertyName("value")] public int Value { get; set; } = 0;
    [JsonPropertyName("achieved")] public bool Achieved { get; set; } = false;
}

public class MissionPoint
{
    [JsonPropertyName("point")] public int Point { get; set; } = 0;
    [JsonPropertyName("achieved_point")] public int AchievedPoint { get; set; } = 0;
}

public class HonorData
{
    [JsonPropertyName("m_honor_id")] public int HonorId { get; set; } = 0;
    [JsonPropertyName("unlocked")] public bool Unlocked { get; set; } = false;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class ScfesProfile
{
    [JsonPropertyName("enable")] public bool Enable { get; set; } = false;
}

public class ScfesProfileEnabled : ScfesProfile
{
    [JsonPropertyName("name")] public string Name { get; set; } = "Test";
    [JsonPropertyName("unit_id")] public int UnitId { get; set; } = 0;
    [JsonPropertyName("award_id")] public int AwardId { get; set; } = 0;
    [JsonPropertyName("rank_up_flag")] public int RankUpFlag { get; set; } = 0;
    [JsonPropertyName("invalid_name")] public bool InvalidName { get; set; } = false;
    [JsonPropertyName("live_list")] public int[] LiveList { get; set; } = new int[0];
}

public class GachaStatus
{
    [JsonPropertyName("m_gacha_id")] public int GachaId { get; set; } = 0;
    [JsonPropertyName("usage_count")] public int UsageCount { get; set; } = 0;
    [JsonPropertyName("idol_kind")] public int IdolKind { get; set; } = 0;
    [JsonPropertyName("in_use")] public bool InUse { get; set; } = false;
}

public class NamePlate
{
    [JsonPropertyName("m_nameplate_id")] public int NamePlateId { get; set; } = 0;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class Badge
{
    [JsonPropertyName("m_badge_id")] public int BadgeId { get; set; } = 0;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class StampCard
{
    [JsonPropertyName("stamp_card_id")] public int StampCardId { get; set; } = 0;
    [JsonPropertyName("stamp_count")] public int StampCount { get; set; } = 0;
    [JsonPropertyName("achieved")] public bool Achieved { get; set; } = false;
    
    //seems to go up to 9 characters (see .text:0000000140226E52)
    [JsonPropertyName("stamp_characters")] public int[] StampCharacters { get; set; } = new int[0];
}

public class StampCardReward
{
    [JsonPropertyName("stamp_card_id")] public int StampCardId { get; set; } = 0;
    [JsonPropertyName("m_card_member_id")] public int CardMemberId { get; set; } = 0;
    [JsonPropertyName("trade_coin")] public bool TradeCoin { get; set; } = false;
}

public class ActiveInformation
{
    [JsonPropertyName("id")] public int Id { get; set; } = 0;
}