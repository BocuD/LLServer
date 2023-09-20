﻿using System.Text.Json.Serialization;

namespace LLServer.Models.UserData;

public class GameHistoryBase
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("play_place")] 
    public string PlayPlace { get; set; } = "";

    [JsonPropertyName("created")] //standard datetime used in other places (e.g. playdate or now)
    public string Created { get; set; } = "";
    
    [JsonPropertyName("d_user_id")]
    public string DUserId { get; set; }
    
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }
    
    [JsonPropertyName("m_membercard_id")]
    public int MemberCardId { get; set; }
    
    [JsonPropertyName("used_member_card")]
    public int UsedMemberCard { get; set; }
    
    [JsonPropertyName("yell_rank")]
    public int YellRank { get; set; }
    
    [JsonPropertyName("badge")]
    public int Badge { get; set; }
    
    [JsonPropertyName("nameplate")]
    public int Nameplate { get; set; }
    
    [JsonPropertyName("honor")]
    public int Honor { get; set; }
    
    [JsonPropertyName("skill_cards_main")]
    public int[] SkillCardsMain { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_cards_camera")]
    public int[] SkillCardsCamera { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_cards_stage")]
    public int[] SkillCardsStage { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_levels_main")]
    public int[] SkillLevelsMain { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_levels_camera")]
    public int[] SkillLevelsCamera { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_levels_stage")]
    public int[] SkillLevelsStage { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_status_main")]
    public int[] SkillStatusMain { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_status_camera")]
    public int[] SkillStatusCamera { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_status_stage")]
    public int[] SkillStatusStage { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("skill_arg_camera")]
    public int[] SkillArgCamera { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("m_live_id")]
    public int LiveId { get; set; }
    
    [JsonPropertyName("m_stage_id")]
    public int StageId { get; set; }
    
    [JsonPropertyName("event_mode")]
    public int EventMode { get; set; }
    
    [JsonPropertyName("member_count")]
    public int MemberCount { get; set; }
    
    [JsonPropertyName("play_part")]
    public int PlayPart { get; set; }
    
    [JsonPropertyName("max_combo")]
    public int MaxCombo { get; set; }
    
    [JsonPropertyName("full_combo")]
    public int FullCombo { get; set; }
    
    [JsonPropertyName("note_miss_count")]
    public int NoteMissCount { get; set; }
    
    [JsonPropertyName("note_bad_count")]
    public int NoteBadCount { get; set; }
    
    [JsonPropertyName("note_good_count")]
    public int NoteGoodCount { get; set; }
    
    [JsonPropertyName("note_great_count")]
    public int NoteGreatCount { get; set; }
    
    [JsonPropertyName("note_perfect_count")]
    public int NotePerfectCount { get; set; }
    
    [JsonPropertyName("finale_point")]
    public int FinalePoint { get; set; }
    
    [JsonPropertyName("technical_score")]
    public int TechnicalScore { get; set; }
    
    [JsonPropertyName("skill_score")]
    public int SkillScore { get; set; }
    
    [JsonPropertyName("synchro_score")]
    public int SynchroScore { get; set; }
    
    [JsonPropertyName("combo_score")]
    public int ComboScore { get; set; }
    
    [JsonPropertyName("technical_rank")]
    public int TechnicalRank { get; set; }
}