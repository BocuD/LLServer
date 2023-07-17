using System.Text.Json;
using System.Text.Json.Serialization;

namespace LLServer.Models;

public class GameResult
{
    [JsonPropertyName("badge")]
    public int Badge { get; set; }

    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }

    [JsonPropertyName("combo_rank")]
    public int ComboRank { get; set; }

    [JsonPropertyName("combo_score")]
    public int ComboScore { get; set; }

    [JsonPropertyName("coop_mode")]
    public int CoopMode { get; set; }

    [JsonPropertyName("coop_player_id_1")]
    public string CoopPlayerId1 { get; set; } = "";

    [JsonPropertyName("coop_player_id_2")]
    public string CoopPlayerId2 { get; set; } = "";

    [JsonPropertyName("coop_player_level_1")]
    public int CoopPlayerLevel1 { get; set; }

    [JsonPropertyName("coop_player_level_2")]
    public int CoopPlayerLevel2 { get; set; }

    [JsonPropertyName("coop_player_name_1")]
    public string CoopPlayerName1 { get; set; } = "";

    [JsonPropertyName("coop_player_name_2")]
    public string CoopPlayerName2 { get; set; } = "";

    [JsonPropertyName("coop_total_score")]
    public int CoopTotalScore { get; set; }

    [JsonPropertyName("d_user_level")]
    public int DUserLevel { get; set; }

    [JsonPropertyName("event_mode")]
    public int EventMode { get; set; }

    [JsonPropertyName("finale_point")]
    public int FinalePoint { get; set; }

    [JsonPropertyName("full_combo")]
    public int FullCombo { get; set; }

    [JsonPropertyName("got_exp")]
    public int GotExp { get; set; }

    [JsonPropertyName("honor")]
    public int Honor { get; set; }

    [JsonPropertyName("idol_kind")]
    public int IdolKind { get; set; }

    [JsonPropertyName("last_cut_focus")]
    public int LastCutFocus { get; set; }

    [JsonPropertyName("live_break")]
    public int LiveBreak { get; set; }

    [JsonPropertyName("m_live_id")]
    public int LiveId { get; set; }

    [JsonPropertyName("m_membercard_id")]
    public int MembercardId { get; set; }

    [JsonPropertyName("m_stage_id")]
    public int StageId { get; set; }

    [JsonPropertyName("max_combo")]
    public int MaxCombo { get; set; }

    [JsonPropertyName("member_count")]
    public int MemberCount { get; set; }

    [JsonPropertyName("memorial_card")]
    public int MemorialCard { get; set; }

    [JsonPropertyName("nameplate")]
    public int Nameplate { get; set; }

    [JsonPropertyName("note_bad_count")]
    public int NoteBadCount { get; set; }

    [JsonPropertyName("note_good_count")]
    public int NoteGoodCount { get; set; }

    [JsonPropertyName("note_great_count")]
    public int NoteGreatCount { get; set; }

    [JsonPropertyName("note_miss_count")]
    public int NoteMissCount { get; set; }

    [JsonPropertyName("note_perfect_count")]
    public int NotePerfectCount { get; set; }

    [JsonPropertyName("play_mode")]
    public int PlayMode { get; set; }

    [JsonPropertyName("play_no")]
    public int PlayNo { get; set; }

    [JsonPropertyName("play_part")]
    public int PlayPart { get; set; }

    [JsonPropertyName("profile_card_id_1")]
    public string ProfileCardId1 { get; set; } = "";

    [JsonPropertyName("profile_card_id_2")]
    public string ProfileCardId2 { get; set; } = "";

    [JsonPropertyName("profile_version")]
    public int ProfileVersion { get; set; }

    [JsonPropertyName("skill_arg_camera")]
    public int[] SkillArgCamera { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_cards_camera")]
    public int[] SkillCardsCamera { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_cards_main")]
    public int[] SkillCardsMain { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_cards_stage")]
    public int[] SkillCardsStage { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_levels_camera")]
    public int[] SkillLevelsCamera { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_levels_main")]
    public int[] SkillLevelsMain { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_levels_stage")]
    public int[] SkillLevelsStage { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_rank")]
    public int SkillRank { get; set; }

    [JsonPropertyName("skill_score")]
    public int SkillScore { get; set; }

    [JsonPropertyName("skill_status_camera")]
    public int[] SkillStatusCamera { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_status_main")]
    public int[] SkillStatusMain { get; set; } = Array.Empty<int>();

    [JsonPropertyName("skill_status_stage")]
    public int[] SkillStatusStage { get; set; } = Array.Empty<int>();

    [JsonPropertyName("synchro_rank")]
    public int SynchroRank { get; set; }

    [JsonPropertyName("synchro_score")]
    public int SynchroScore { get; set; }

    [JsonPropertyName("technical_rank")]
    public int TechnicalRank { get; set; }

    [JsonPropertyName("technical_rate")]
    public int TechnicalRate { get; set; }

    [JsonPropertyName("technical_score")]
    public int TechnicalScore { get; set; }

    [JsonPropertyName("tenpo_name")]
    public string TenpoName { get; set; } = "";

    [JsonPropertyName("total_exp")]
    public int TotalExp { get; set; }

    [JsonPropertyName("total_rank")]
    public int TotalRank { get; set; }

    [JsonPropertyName("total_score")]
    public int TotalScore { get; set; }

    [JsonPropertyName("unlock_live_id")]
    public JsonElement? UnlockLiveId { get; set; }

    [JsonIgnore]
    public int[] UnlockLiveIdArray
    {
        get
        {
            if (UnlockLiveId is { ValueKind: JsonValueKind.Array })
            {
                return UnlockLiveId?.EnumerateArray().Select(x => x.GetInt32()).ToArray() ?? Array.Empty<int>();
            }

            return Array.Empty<int>();
        }
    }

    [JsonPropertyName("used_member_card")]
    public int UsedMemberCard { get; set; }

    [JsonPropertyName("yell_rank")]
    public int YellRank { get; set; }
}


/*
    "badge": 901001,
    "character_id": 1,
    "combo_rank": 0,
    "combo_score": 0,
    "coop_mode": 0,
    "coop_player_id_1": "0",
    "coop_player_id_2": "0",
    "coop_player_level_1": 0,
    "coop_player_level_2": 0,
    "coop_player_name_1": "",
    "coop_player_name_2": "",
    "coop_total_score": 0,
    "d_user_level": 2,
    "event_mode": 0,
    "finale_point": 9,
    "full_combo": 0,
    "got_exp": 468,
    "honor": 2,
    "idol_kind": 0,
    "last_cut_focus": 1,
    "live_break": 0,
    "m_live_id": 80002,
    "m_membercard_id": 10011,
    "m_stage_id": 1,
    "max_combo": 211,
    "member_count": 1,
    "memorial_card": 0,
    "nameplate": 19001,
    "note_bad_count": 0,
    "note_good_count": 1,
    "note_great_count": 65,
    "note_miss_count": 0,
    "note_perfect_count": 187,
    "play_mode": 0,
    "play_no": 0,
    "play_part": 1,
    "profile_card_id_1": "00000000000000000000000000000000",
    "profile_card_id_2": "00000000000000000000000000000000",
    "profile_version": 65536,
    "skill_arg_camera": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_cards_camera": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_cards_main": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_cards_stage": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_levels_camera": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_levels_main": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_levels_stage": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_rank": 0,
    "skill_score": 0,
    "skill_status_camera": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_status_main": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    "skill_status_stage": [
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0
    ],
    
    "synchro_rank": 0,
    "synchro_score": 0,
    "technical_rank": 5,
    "technical_rate": 114480,
    "technical_score": 77097,
    "tenpo_name": "LLServer",
    "total_exp": 468,
    "total_rank": 5,
    "total_score": 0,
    "unlock_live_id": [
      80003
    ],
    "used_member_card": 10011,
    "yell_rank": 1
 */