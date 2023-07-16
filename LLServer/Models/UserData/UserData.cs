using System.Globalization;
using System.Text.Json.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LLServer.Models.UserData;

//todo decompile sub_14021C260 and figure out what this is responsible for parsing
public class UserData
{
    //returned directly by game in userdata.initialize
    [JsonPropertyName("character_id")]
    public int CharacterId { get; set; }

    [JsonPropertyName("idol_kind")]
    public int IdolKind { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "Test";

    [JsonPropertyName("note_speed_level")]
    public int NoteSpeedLevel { get; set; }

    [JsonPropertyName("submonitor_type")]
    public int SubMonitorType { get; set; }

    [JsonPropertyName("volume_bgm")]
    public int VolumeBgm { get; set; }

    [JsonPropertyName("volume_se")]
    public int VolumeSe { get; set; } 

    [JsonPropertyName("volume_voice")]
    public int VolumeVoice { get; set; } 

    //from decompiled method parsePlayerDataJson
    [JsonPropertyName("tenpo_name")]
    public string TenpoName { get; set; } = "Test";

    [JsonPropertyName("play_date")]
    public string PlayDate { get; set; } = "2021-01-01";

    [JsonPropertyName("play_satellite")]
    public int PlaySatellite { get; set; }

    [JsonPropertyName("play_center")]
    public int PlayCenter { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("total_exp")]
    public int TotalExp { get; set; }

    [JsonPropertyName("honor")]
    public int Honor { get; set; }

    [JsonPropertyName("badge")]
    public int Badge { get; set; }

    [JsonPropertyName("nameplate")]
    public int Nameplate { get; set; }

    [JsonIgnore]
    public ProfileCard ProfileCard1 { get; set; } = new(0);

    [JsonPropertyName("profile_card_id_1")]
    public string ProfileCardId1
    {
        get => ProfileCard1.ToString();
        set => ProfileCard1 = new ProfileCard(long.Parse(value, NumberStyles.HexNumber));
    }

    [JsonIgnore]
    public ProfileCard ProfileCard2 { get; set; } = new(0);

    [JsonPropertyName("profile_card_id_2")]
    public string ProfileCardId2
    {
        get => ProfileCard2.ToString();
        set => ProfileCard2 = new ProfileCard(long.Parse(value, NumberStyles.HexNumber));
    }

    [JsonPropertyName("credit_count_satellite")]
    public int CreditCountSatellite { get; set; }

    [JsonPropertyName("credit_count_center")]
    public int CreditCountCenter { get; set; }

    [JsonPropertyName("play_ls4")]
    public int PlayLs4 { get; set; }
}

//todo: the method responsible for parssing specials in the game (at 000140220DF0) seems to get the system time but it is currently not clear what this is used for