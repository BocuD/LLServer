using System.Text.Json.Serialization;

namespace LLServer.Models.Responses;

/*
 * {
  "response": {
    "version": "",
    "season_event_ui": "",
    "level_cap": "",
    "skill_level_cap": "",
    "yell_rank_cap": "",
    "card_sells_value": "",
    "music_release_version": "",
    "finale_focus_release_version": "",
    "stage_release_version": "",
    "member_card_release_version": "",
    "memorial_card_release_version": "",
    "skill_card_release_version": "",
    "advertise_movie_status_0": "",
    "advertise_movie_status_1": "",
    "advertise_movie_status_2": "",
    "advertise_movie_status_3": "",
    "live_demo_table_id": "",
    "exp_mag": "",
    "exp_mag_target_level": "",
    "cheer_enable": "",
    "achievement_release_version": "",
    "travel_pamphlet_release_version": "",
    "live_break_max": "",
    "yell_achieve_mobile_point": "",
    "yell_point_mag": "",
    "dice_bonus_credit": ""
  }
}
 */

public class GameConfigResponse : ResponseBase
{
    [JsonPropertyName("version")] public string Version { get; set; } = "2.4.1";
    [JsonPropertyName("season_event_uid")] public string SeasonEventUid { get; set; } = "";
    [JsonPropertyName("level_cap")] public int LevelCap { get; set; } = 100;
    [JsonPropertyName("skill_level_cap")] public int SkillLevelCap { get; set; } = 100;
    [JsonPropertyName("yell_rank_cap")] public int YellRankCap { get; set; } = 100;
    [JsonPropertyName("card_sells_value")] public int CardSellsValue { get; set; } = 1;
    [JsonPropertyName("music_release_version")] public int MusicReleaseVersion { get; set; } = 9999;
    [JsonPropertyName("finale_focus_release_version")] public int FinaleFocusReleaseVersion { get; set; } = 9999;
    [JsonPropertyName("stage_release_version")] public int StageReleaseVersion { get; set; } = 9999;
    [JsonPropertyName("member_card_release_version")] public int MemberCardReleaseVersion { get; set; } = 9999;
    [JsonPropertyName("memorial_card_release_version")] public int MemorialCardReleaseVersion { get; set; } = 9999;
    [JsonPropertyName("skill_card_release_version")] public int SkillCardReleaseVersion { get; set; } = 9999;
    [JsonPropertyName("advertise_movie_status_0")] public int AdvertiseMovieStatus0 { get; set; } = 1;
    [JsonPropertyName("advertise_movie_status_1")] public int AdvertiseMovieStatus1 { get; set; } = 1;
    [JsonPropertyName("advertise_movie_status_2")] public int AdvertiseMovieStatus2 { get; set; } = 1;
    [JsonPropertyName("advertise_movie_status_3")] public int AdvertiseMovieStatus3 { get; set; } = 1;
    [JsonPropertyName("live_demo_table_id")] public int LiveDemoTableId { get; set; } = 1;
    [JsonPropertyName("exp_mag")] public float ExpMag { get; set; } = 1.0f;
    [JsonPropertyName("exp_mag_target_level")] public int ExpMagTargetLevel { get; set; } = 10;
    [JsonPropertyName("cheer_enable")] public int CheerEnable { get; set; } = 1; //might be bool but seems to be processed as int
    [JsonPropertyName("achievement_release_version")] public int AchievementReleaseVersion { get; set; } = 1;
    [JsonPropertyName("travel_pamphlet_release_version")] public int TravelPamphletReleaseVersion { get; set; } = 1;
    [JsonPropertyName("live_break_max")] public int LiveBreakMax { get; set; } = 1;
    [JsonPropertyName("yell_achieve_mobile_point")] public int YellAchieveMobilePoint { get; set; } = 10;
    [JsonPropertyName("yell_point_mag")] public float YellPointMag { get; set; } = 1.0f;
    [JsonPropertyName("dice_bonus_credit")] public int DiceBonusCredit { get; set; } = 0;

    public static ResponseBase DefaultGameConfigResponse()
    {
        return new GameConfigResponse();
    }
}