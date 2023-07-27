using System.Text.Json.Serialization;
using LLServer.Models.UserData;

namespace LLServer.Models.Requests;

public class AchievementParam
{
    [JsonPropertyName("achievements")]
    public int[] Achievements { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("limited_achievements")]
    public int[] LimitedAchievements { get; set; } = Array.Empty<int>();
    
    [JsonPropertyName("record_books")]
    public AchievementRecordBook[] RecordBooks { get; set; } = Array.Empty<AchievementRecordBook>();
}

/*
{
    "blockseq": 11,
    "game": {
        "eventcode": "000",
        "version": "2.4.1"
    },
    "param": {
        "achievements": [
            12001,
            12002,
            12003,
            12004,
            12005,
            12006,
            12007,
            501001,
            501002,
            501016,
            501031,
            501032,
            501046,
            501047,
            501061,
            501136,
            501151,
            501166,
            501256,
            501271,
            501286,
            501346,
            501361,
            501376,
            501391,
            501421,
            501451,
            501481,
            501496,
            501556,
            501586,
            502556,
            505001,
            505002,
            505003,
            506361,
            506362,
            506363,
            514601,
            514602,
            514603,
            526001,
            551001,
            552001,
            552002,
            553001,
            506364,
            551002
        ],
        "limited_achievements": [],
        "record_books": [
            {
                "type": "countFinale",
                "values": [
                    0,
                    1,
                    0
                ]
            },
            {
                "type": "countSkillInvoke",
                "values": [
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0
                ]
            },
            {
                "type": "countSelectMember",
                "values": [
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0
                ]
            },
            {
                "type": "countSelectUnit",
                "values": [
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0
                ]
            },
            {
                "type": "countGetMemberCard",
                "values": [
                    0,
                    0,
                    0,
                    1,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    1,
                    0,
                    0,
                    1,
                    0
                ]
            },
            {
                "type": "countGetSkillCard",
                "values": [
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0
                ]
            },
            {
                "type": "countPhotograph",
                "values": [
                    0,
                    0,
                    0
                ]
            },
            {
                "type": "countVisualScoreIconSuccess",
                "values": [
                    0,
                    0,
                    0
                ]
            }
        ]
    },
    "protocol": "achievement",
    "sessionkey": "7d079ffc0ae445a58814951a09c6617e",
    "terminal": {
        "tenpo_id": "1337",
        "tenpo_index": 1337,
        "terminal_attrib": 0,
        "terminal_id": "D8BBC1B24587"
    }
}
 */