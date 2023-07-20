using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Requests.Travel;

/*
{
    "param": {
        "badges": [],
        "card_frames": [],
        "coop_player_ids": [],
        "dice_count": 1,
        "get_memorial_cards": [],
        "get_skill_cards": [],
        "item": [
            {
                "count": 1,
                "m_item_id": 20102
            }
        ],
        "level": 9,
        "lot_gachas": [],
        "m_card_member_id": 40011,
        "member_yell": [],
        "nameplates": [],
        "release_pamphlet_ids": [],
        "special_ids": [
            1
        ],
        "stage_ids": [],
        "talk_count": 0,
        "tenpo_name": "LLServer",
        "total_exp": 2223,
        "travel_ex_rewards": [],
        "travel_history": [
            {
                "create_type": 0,
                "m_snap_background_id": 20100,
                "other_character_id": 0,
                "other_d_user_id": 0
            }
        ],
        "travel_talks": [],
        "user_travel": {
            "character_id": 4,
            "is_goal": 0,
            "last_landmark": 2,
            "m_card_memorial_id": 4000,
            "m_travel_pamphlet_id": 201,
            "positions": [
                43
            ],
            "slot": 0
        },
        "walk_count": 5
    },
    "protocol": "TravelResult",
}
 */

public class TravelResultParam
{
    [JsonPropertyName("badges")] public int[] Badges { get; set; } = Array.Empty<int>();
    [JsonPropertyName("card_frames")] public int[] CardFrames { get; set; } = Array.Empty<int>();
    [JsonPropertyName("coop_player_ids")] public Int128[] CoopPlayerIds { get; set; } = Array.Empty<Int128>();
    [JsonPropertyName("dice_count")] public int DiceCount { get; set; }
    [JsonPropertyName("get_memorial_cards")] public GetMemorialCard[] GetMemorialCards { get; set; } = Array.Empty<GetMemorialCard>();
    [JsonPropertyName("get_skill_cards")] public GetSkillCard[] GetSkillCards { get; set; } = Array.Empty<GetSkillCard>();
    [JsonPropertyName("item")] public Item[] Item { get; set; } = Array.Empty<Item>();
    [JsonPropertyName("level")] public int Level { get; set; } 
    [JsonPropertyName("lot_gachas")] public LotGacha[] LotGachas { get; set; } = Array.Empty<LotGacha>();
    [JsonPropertyName("m_card_member_id")] public int CardMemberId { get; set; }
    [JsonPropertyName("member_yell")] public MemberYell[] MemberYells { get; set; } = Array.Empty<MemberYell>();
    [JsonPropertyName("nameplates")] public int[] Nameplates { get; set; } = Array.Empty<int>();
    [JsonPropertyName("release_pamphlet_ids")] public int[] ReleasePamphletIds { get; set; } = Array.Empty<int>();
    [JsonPropertyName("special_ids")] public int[] SpecialIds { get; set; } = Array.Empty<int>();
    
    //todo: actual type is unknown, test this with the game
    [JsonPropertyName("stage_ids")] public int[] StageIds { get; set; } = Array.Empty<int>();
    [JsonPropertyName("talk_count")] public int TalkCount { get; set; }
    [JsonPropertyName("tenpo_name")] public string TenpoName { get; set; } = "";
    [JsonPropertyName("total_exp")] public int TotalExp { get; set; }
    
    //todo: actual type is unknown, test this with the game
    [JsonPropertyName("travel_ex_rewards")] public int[] TravelExRewards { get; set; } = Array.Empty<int>();
    [JsonPropertyName("travel_history")] public TravelHistory_[] TravelHistory { get; set; } = Array.Empty<TravelHistory_>();
    [JsonPropertyName("travel_talks")] public TravelTalk[] TravelTalks { get; set; } = Array.Empty<TravelTalk>();
    //note: traveldata is a perfect match aside from the modified property which is missing. Keep in mind when implementing this. 
    [JsonPropertyName("user_travel")] public TravelData UserTravel { get; set; } = new();
    [JsonPropertyName("walk_count")] public int WalkCount { get; set; }
}

public class GetMemorialCard
{
    [JsonPropertyName("memorial_card_id")] public int MemorialCardId { get; set; }
    [JsonPropertyName("location")] public int Location { get; set; }
}

public class GetSkillCard
{
    [JsonPropertyName("skill_card_id")] public int SkillCardId { get; set; }
    [JsonPropertyName("location")] public int Location { get; set; }
}

//todo: actual type is unknown, test this with the game (this is purely based on vague decompiled code)
public class LotGacha
{
    [JsonPropertyName("gacha_id")] public int GachaId { get; set; }
    [JsonPropertyName("card_count")] public int CardCount { get; set; }
    [JsonPropertyName("location")] public int Location { get; set; }
    [JsonPropertyName("order")] public int Order { get; set; }
}

public class TravelTalk
{
    [JsonPropertyName("talk_id")] public int TalkId { get; set; }
    [JsonPropertyName("my_character_id")] public int MyCharacterId { get; set; }
    [JsonPropertyName("other_character_id")] public int OtherCharacterId { get; set; }
}

public class TravelHistory_
{
    [JsonPropertyName("create_type")] public int CreateType { get; set; }
    [JsonPropertyName("m_snap_background_id")] public int SnapBackgroundId { get; set; }
    [JsonPropertyName("other_character_id")] public int OtherCharacterId { get; set; }
    [JsonPropertyName("other_d_user_id")] public int OtherDUserId { get; set; }
}