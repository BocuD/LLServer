﻿using System.Text.Json.Serialization;

namespace LLServer.Models.Responses;

public class GameConfigResponse : ResponseBase
{
    [JsonPropertyName("version")]
    public string Version { get; set; } = "2.4.1";

    [JsonPropertyName("season_event_uid")]
    public string SeasonEventUid { get; set; } = "0";

    [JsonPropertyName("level_cap")]
    public int LevelCap { get; set; } = 200;

    [JsonPropertyName("skill_level_cap")]
    public int SkillLevelCap { get; set; } = 999;

    [JsonPropertyName("yell_rank_cap")]
    public int YellRankCap { get; set; } = 999;

    [JsonPropertyName("card_sells_value")]
    public int CardSellsValue { get; set; } = 100;

    [JsonPropertyName("music_release_version")]
    public int MusicReleaseVersion { get; set; } = 99999;

    [JsonPropertyName("finale_focus_release_version")]
    public int FinaleFocusReleaseVersion { get; set; } = 99999;

    [JsonPropertyName("stage_release_version")]
    public int StageReleaseVersion { get; set; } = 99999;

    [JsonPropertyName("member_card_release_version")]
    public int MemberCardReleaseVersion { get; set; } = 99999;

    [JsonPropertyName("memorial_card_release_version")]
    public int MemorialCardReleaseVersion { get; set; } = 99999;

    [JsonPropertyName("skill_card_release_version")]
    public int SkillCardReleaseVersion { get; set; } = 99999;

    [JsonPropertyName("advertise_movie_status_0")]
    public bool AdvertiseMovieStatus0 { get; set; } = true;

    [JsonPropertyName("advertise_movie_status_1")]
    public bool AdvertiseMovieStatus1 { get; set; } = false;

    [JsonPropertyName("advertise_movie_status_2")]
    public bool AdvertiseMovieStatus2 { get; set; } = false;

    [JsonPropertyName("advertise_movie_status_3")]
    public bool AdvertiseMovieStatus3 { get; set; } = false;

    [JsonPropertyName("live_demo_table_id")]
    public int LiveDemoTableId { get; set; } = 2;

    [JsonPropertyName("exp_mag")]
    public float ExpMag { get; set; } = 1.0f;

    [JsonPropertyName("exp_mag_target_level")]
    public int ExpMagTargetLevel { get; set; } = 5;

    [JsonPropertyName("cheer_enable")]
    public bool CheerEnable { get; set; } = false;

    [JsonPropertyName("achievement_release_version")]
    public int AchievementReleaseVersion { get; set; } = 99999;

    [JsonPropertyName("travel_pamphlet_release_version")]
    public int TravelPamphletReleaseVersion { get; set; } = 99999;

    [JsonPropertyName("live_break_max")]
    public int LiveBreakMax { get; set; } = 1;

    [JsonPropertyName("yell_achieve_mobile_point")]
    public int YellAchieveMobilePoint { get; set; } = 100;

    [JsonPropertyName("yell_point_mag")]
    public float YellPointMag { get; set; } = 1.0f;

    [JsonPropertyName("dice_bonus_credit")]
    public int DiceBonusCredit { get; set; } = 100;

    public static ResponseBase DefaultGameConfigResponse()
    {
        GameConfigResponse response = new GameConfigResponse();
        
        //random number between 0 and 10
        response.LiveDemoTableId = new Random().Next(0, 10);

        return response;
    }
}