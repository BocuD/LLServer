using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace LLServer.Models.UserData;

//todo decompile sub_14021C260 and figure out what this is responsible for parsing
public class UserData : UserDataBase
{
    //returned directly by game in userdata.initialize
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

    [JsonPropertyName("credit_count_satellite")]
    public int CreditCountSatellite { get; set; }

    [JsonPropertyName("credit_count_center")]
    public int CreditCountCenter { get; set; }

    [JsonPropertyName("play_ls4")]
    public int PlayLs4 { get; set; } = 1;
    
    [JsonIgnore, Key, ForeignKey("User")] public ulong UserID { get; set; }
    
    //One to one relationship with user
    [JsonIgnore] public User? User { get; set; }
}

//todo: the method responsible for parssing specials in the game (at 000140220DF0) seems to get the system time but it is currently not clear what this is used for