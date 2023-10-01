using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;
using LLServer.Models.Travel;
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
        "item": [],  //sometimes contains items with item count, sometimes does not, needs more testing
        "level": 45, //user level
        "lot_gachas": [ //gacha of some kind, if this has data we need to respond to this somehow
            {
                "card_count": 1,
                "gacha_id": "gta_travel_ss_normal",
                "location": 900,
                "order": 1
            }
        ],
        "m_card_member_id": 170901,  //used card member
        "member_yell": [],
        "nameplates": [
            1710222  //unlocked nameplate
        ],
        "release_pamphlet_ids": [], //unlocked pamphlet
        "special_ids": [
            14  //gained special item (cracker)
        ],
        "stage_ids": [
            107  //unlocked stage
        ],
        "talk_count": 1,
        "tenpo_name": "LLServer",
        "total_exp": 160402,
        "travel_ex_rewards": [
            10068  //no idea what this does
        ],
        "travel_history": [
            {
                "create_type": 2,
                "m_snap_background_id": 20100,
                "other_character_id": 6,
                "other_d_user_id": 0
            }
        ],
        "travel_talks": [
            {
                "my_character_id": 4,
                "other_character_id": 6,
                "talk_id": 400006
            }
        ],
        "user_travel": {
            "character_id": 17,
            "is_goal": 1,
            "last_landmark": 9,
            "m_card_memorial_id": 17100,
            "m_travel_pamphlet_id": 130222,
            "positions": [
                50,
                51,
                51
            ],
            "slot": 10
        },
        "walk_count": 1
    },
    "protocol": "TravelResult",
 */

public class TravelResultParam
{
    [JsonPropertyName("badges")] public int[] Badges { get; set; } = Array.Empty<int>();
    [JsonPropertyName("card_frames")] public int[] CardFrames { get; set; } = Array.Empty<int>();
    [JsonPropertyName("coop_player_ids")] public ulong[] CoopPlayerIds { get; set; } = Array.Empty<ulong>();
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
    
    //todo: figure out what the purpose of this is
    [JsonPropertyName("travel_ex_rewards")] public int[] TravelExRewards { get; set; } = Array.Empty<int>();
    [JsonPropertyName("travel_history")] public TravelHistory_[] TravelHistory { get; set; } = Array.Empty<TravelHistory_>();
    [JsonPropertyName("travel_talks")] public TravelTalk[] TravelTalks { get; set; } = Array.Empty<TravelTalk>();
    //note: traveldata is a perfect match aside from the modified property which is missing. Keep in mind when implementing this. 
    [JsonPropertyName("user_travel")] public UserTravel UserTravel { get; set; } = new();
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
    [JsonPropertyName("gacha_id")] public string GachaId { get; set; } = "";
    [JsonPropertyName("card_count")] public int CardCount { get; set; }
    [JsonPropertyName("location")] public int Location { get; set; }
    [JsonPropertyName("order")] public int Order { get; set; }
}

/*
{
  "character_id": 17,
  "is_goal": 1,
  "last_landmark": 9,
  "m_card_memorial_id": 17100,
  "m_travel_pamphlet_id": 100221,
  "positions": [
    47,
    56,
    56
  ],
  "slot": 10
}
 */

public class UserTravel
{
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; } 
    
    [JsonPropertyName("is_goal")]
    public int IsGoal { get; set; }
    
    [JsonPropertyName("last_landmark")]
    public int LastLandmark { get; set; } 
    
    [JsonPropertyName("m_card_memorial_id")]
    public int CardMemorialId { get; set; }
    
    [JsonPropertyName("m_travel_pamphlet_id")]
    public int TravelPamphletId { get; set; }
    
    //always seems to be 3 integers
    [JsonPropertyName("positions")]
    public int[] Positions { get; set; } = new int[3];
    
    [JsonPropertyName("slot")]
    public int Slot { get; set; }
}

public class TravelHistory_
{
    [JsonPropertyName("create_type")] public int CreateType { get; set; }
    [JsonPropertyName("m_snap_background_id")] public int SnapBackgroundId { get; set; }
    [JsonPropertyName("other_character_id")] public int OtherCharacterId { get; set; }
    [JsonPropertyName("other_d_user_id")] public int OtherDUserId { get; set; }
}