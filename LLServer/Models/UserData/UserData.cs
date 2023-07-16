using System.Globalization;
using System.Text.Json.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LLServer.Models.UserData;

//todo decompile sub_14021C260 and figure out what this is responsible for parsing
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
    [JsonIgnore] public ProfileCard ProfileCard1 { get; set; } = new ProfileCard(0);

    [JsonPropertyName("profile_card_id_1")]
    public string ProfileCardId1
    {
        get => ProfileCard1.ToString();
        set => ProfileCard1 = new ProfileCard(long.Parse(value, NumberStyles.HexNumber));
    }

    [JsonIgnore] public ProfileCard ProfileCard2 { get; set; } = new ProfileCard(0);
    
    [JsonPropertyName("profile_card_id_2")]
    public string ProfileCardId2
    {
        get => ProfileCard2.ToString();
        set => ProfileCard2 = new ProfileCard(long.Parse(value, NumberStyles.HexNumber));
    }
    
    [JsonPropertyName("credit_count_satellite")] public int CreditCountSatellite { get; set; } = 0;
    [JsonPropertyName("credit_count_center")] public int CreditCountCenter { get; set; } = 0;
    [JsonPropertyName("play_ls4")] public int PlayLs4 { get; set; } = 0;
}

public class ProfileCard
{
    private Int128 Id = 0;
    
    public ProfileCard(long id)
    {
        Id = id;
    }
    
    public override string ToString()
    {
        //return as 32 character hex string
        return Id.ToString("X32");
    }
}

public class UserDataAqours
{
    [JsonPropertyName("character_id")] public int CharacterId { get; set; } = 0;
    [JsonPropertyName("honor")] public int Honor { get; set; } = 0;
    [JsonPropertyName("badge")] public int Badge { get; set; } = 0;
    [JsonPropertyName("nameplate")] public int Nameplate { get; set; } = 0;

    
    [JsonIgnore] public ProfileCard ProfileCard1 { get; set; } = new ProfileCard(0);

    [JsonPropertyName("profile_card_id_1")]
    public string ProfileCardId1
    {
        get => ProfileCard1.ToString();
        set => ProfileCard1 = new ProfileCard(long.Parse(value, NumberStyles.HexNumber));
    }

    [JsonIgnore] public ProfileCard ProfileCard2 { get; set; } = new ProfileCard(0);
    
    [JsonPropertyName("profile_card_id_2")]
    public string ProfileCardId2
    {
        get => ProfileCard2.ToString();
        set => ProfileCard2 = new ProfileCard(long.Parse(value, NumberStyles.HexNumber));
    }
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

public class MemorialCardData
{
    [JsonPropertyName("m_card_memorial_id")] public int CardMemorialId { get; set; } = 0;
    [JsonPropertyName("count")] public int Count { get; set; } = 0;
    [JsonPropertyName("print_rest")] public int PrintRest { get; set; } = 0;
    [JsonPropertyName("select_count")] public int SelectCount { get; set; } = 0;
    [JsonPropertyName("talk_count")] public int TalkCount { get; set; } = 0;
    [JsonPropertyName("goal_count")] public int GoalCount { get; set; } = 0;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class Item
{
    [JsonPropertyName("m_item_id")] public int ItemId { get; set; } = 0;
    [JsonPropertyName("count")] public int Count { get; set; } = 0;
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

    public static readonly int[] Stages =
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 
        111, 112, 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312
    };
}

public class TravelHistory
{
    [JsonPropertyName("id")] public long Id { get; set; } = 0;
    [JsonPropertyName("m_card_member_id")] public int CardMemberId { get; set; } = 0;
    [JsonPropertyName("m_snap_background_id")] public int SnapBackgroundId { get; set; } = 0;
    [JsonPropertyName("other_character_id")] public int OtherCharacterId { get; set; } = 0;
    [JsonPropertyName("other_player_name")] public string OtherPlayerName { get; set; } = "";
    [JsonPropertyName("other_player_nameplate")] public int OtherPlayerNameplate { get; set; } = 0;
    [JsonPropertyName("other_player_badge")] public int OtherPlayerBadge { get; set; } = 0;
    [JsonPropertyName("m_travel_pamphlet_id")] public int TravelPamphletId { get; set; } = 0;
    [JsonPropertyName("create_type")] public int CreateType { get; set; } = 0;
    [JsonPropertyName("tenpo_name")] public string TenpoName { get; set; } = "";
    [JsonPropertyName("snap_stamp_list")] public SnapStamp[] SnapStampList { get; set; } = new SnapStamp[0];
    [JsonPropertyName("coop_info")] public CoopInfo[] CoopInfo { get; set; } = new CoopInfo[0];
    [JsonPropertyName("created")] public string Created { get; set; } = "";
    [JsonPropertyName("print_rest")] public bool PrintRest { get; set; } = false;
}

public class SnapStamp
{
    [JsonPropertyName("stamp_id")] public int StampId { get; set; } = 0;
    [JsonPropertyName("stamp_nameplate_id")] public int StampNameplateId { get; set; } = 0;
}

public class CoopInfo
{
    [JsonPropertyName("coop_player_name")] public string CoopPlayerName { get; set; } = "";
    [JsonPropertyName("coop_player_m_nameplate_id")] public int CoopPlayerMNameplateId { get; set; } = 0;
    [JsonPropertyName("coop_player_m_badge_id")] public int CoopPlayerMBadgeId { get; set; } = 0;
}

public class MailBoxItem
{
    [JsonPropertyName("id")] public int Id { get; set; } = 0;
    [JsonPropertyName("attrib")] public int Attrib { get; set; } = 0;
    [JsonPropertyName("category")] public int Category { get; set; } = 0;
    [JsonPropertyName("item_id")] public int ItemId { get; set; } = 0;
    [JsonPropertyName("count")] public int Count { get; set; } = 0;
}

//todo: the method responsible for parssing specials in the game (at 000140220DF0) seems to get the system time but it is currently not clear what this is used for
public class SpecialData
{
    [JsonPropertyName("idol_kind")] public int IdolKind { get; set; } = 0;
    [JsonPropertyName("special_id")] public int SpecialId { get; set; } = 0;
}

public class Achievement
{
    [JsonPropertyName("m_achievement_id")] public int AchievementId { get; set; } = 0;
    [JsonPropertyName("unlocked")] public bool Unlocked { get; set; } = false;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class AchievementRecordBook
{
    [JsonPropertyName("type")] public string Type { get; set; } = "";
    [JsonPropertyName("values")] public int[] Values { get; set; } = new int[0];
}

public class YellAchievement
{
    [JsonPropertyName("m_yell_achievement_id")] public int YellAchievementId { get; set; } = 0;
    [JsonPropertyName("unlocked")] public bool Unlocked { get; set; } = false;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
}

public class LimitedAchievement
{
    [JsonPropertyName("m_limited_achievement_id")] public int LimitedAchievementId { get; set; } = 0;
    [JsonPropertyName("unlocked")] public bool Unlocked { get; set; } = false;
    [JsonPropertyName("new")] public bool New { get; set; } = false;
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

public class TravelData
{
    [JsonPropertyName("slot")] public int Slot { get; set; } = 0;
    [JsonPropertyName("m_travel_pamphlet_id")] public int TravelPamphletId { get; set; } = 0;
    [JsonPropertyName("character_id")] public int CharacterId { get; set; } = 0;
    [JsonPropertyName("m_card_memorial_id")] public int CardMemorialId { get; set; } = 0;
    
    //todo: double check the type of the data inside the positions array
    //always seems to be 3 items
    [JsonPropertyName("positions")] public int[] Positions { get; set; } = new int[3];
    [JsonPropertyName("last_landmark")] public int LastLandmark { get; set; } = 0;
    [JsonPropertyName("modified")] public string Modified { get; set; } = "";
}

public class TravelPamphlet
{
    [JsonPropertyName("m_travel_pamphlet_id")] public int TravelPamphletId { get; set; } = 0;
    [JsonPropertyName("round")] public int Round { get; set; } = 0;
    [JsonPropertyName("total_talk_count")] public int TotalTalkCount { get; set; } = 0;
    [JsonPropertyName("total_dice_count")] public int TotalDiceCount { get; set; } = 0;
    [JsonPropertyName("is_new")] public bool IsNew { get; set; } = false;
    [JsonPropertyName("travel_ex_rewards")] public int[] TravelExRewards { get; set; } = new int[0];
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

public class EventStatus
{
    [JsonPropertyName("m_event_id")] public int EventId { get; set; } = 0;
    [JsonPropertyName("event_point")] public int EventPoint { get; set; } = 0;
    [JsonPropertyName("next_reward")] public int NextReward { get; set; } = 0;
    [JsonPropertyName("level")] public int Level { get; set; } = 0;
    [JsonPropertyName("rank")] public int Rank { get; set; } = 0;
    [JsonPropertyName("first_play")] public int FirstPlay { get; set; } = 0;
}

public class EventReward
{
    [JsonPropertyName("m_event_id")] public int EventId { get; set; } = 0;
    [JsonPropertyName("reward_category")] public int RewardCategory { get; set; } = 0;
    [JsonPropertyName("reward_id")] public int RewardId { get; set; } = 0;
    [JsonPropertyName("event_point")] public int EventPoint { get; set; } = 0;
    [JsonPropertyName("rank")] public int Rank { get; set; } = 0;
    [JsonPropertyName("reward_num")] public int RewardNum { get; set; } = 0;
}

public class EventResult
{
    [JsonPropertyName("m_event_id")] public int EventId { get; set; } = 0;
    [JsonPropertyName("add_point")] public int AddPoint { get; set; } = 0;
    [JsonPropertyName("event_point")] public int EventPoint { get; set; } = 0;
    [JsonPropertyName("update_score")] public int UpdateScore { get; set; } = 0;
    [JsonPropertyName("rewards")] public EventResultReward[] Rewards { get; set; } = new EventResultReward[0];
}

public class EventResultReward
{
    [JsonPropertyName("reward")] public EventResultRewardData Reward { get; set; } = new EventResultRewardData();

    [JsonPropertyName("next_reward")] public EventResultRewardData NextReward { get; set; } = new EventResultRewardData();
    
    public class EventResultRewardData
    {
        [JsonPropertyName("require_point")] public int RequirePoint { get; set; } = 0;
        [JsonPropertyName("reward_type")] public int RewardType { get; set; } = 0;
        [JsonPropertyName("reward_arg")] public int RewardArg { get; set; } = 0;
    }
}

public class StampCard
{
    [JsonPropertyName("stamp_card_id")] public int StampCardId { get; set; } = 0;
    [JsonPropertyName("stamp_count")] public int StampCount { get; set; } = 0;
    [JsonPropertyName("achieved")] public bool Achieved { get; set; } = false;
    
    //seems to go up to 9 characters (see .text:0000000140226E52)
    [JsonPropertyName("stamp_characters")] public int[] StampCharacters { get; set; } = new int[9];
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