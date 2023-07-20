using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using LLServer.Database.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.UserData;

public class TravelHistoryBase
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("m_card_member_id")]
    public int CardMemberId { get; set; } 

    [JsonPropertyName("m_snap_background_id")]
    public int SnapBackgroundId { get; set; } 

    [JsonPropertyName("other_character_id")]
    public int OtherCharacterId { get; set; }

    [JsonPropertyName("other_player_name")]
    public string OtherPlayerName { get; set; } = "";

    [JsonPropertyName("other_player_nameplate")]
    public int OtherPlayerNameplate { get; set; } 

    [JsonPropertyName("other_player_badge")]
    public int OtherPlayerBadge { get; set; } 

    [JsonPropertyName("m_travel_pamphlet_id")]
    public int TravelPamphletId { get; set; } 

    [JsonPropertyName("create_type")]
    public int CreateType { get; set; }

    [JsonPropertyName("tenpo_name")]
    public string TenpoName { get; set; } = "";
    
    /*//todo: map these to the database
    [JsonPropertyName("snap_stamp_list"), NotMapped]
    public SnapStamp[] SnapStampList { get; set; } = Array.Empty<SnapStamp>();
    
    //todo: map these to the database
    [JsonPropertyName("coop_info"), NotMapped]
    public CoopInfo[] CoopInfo { get; set; } = Array.Empty<CoopInfo>();*/

    [JsonPropertyName("created")]
    public string Created { get; set; } = "";

    [JsonPropertyName("print_rest")]
    public bool PrintRest { get; set; }
}

public class TravelHistory : TravelHistoryBase
{
    //Database key
    [JsonIgnore, Key] public int DbId { get; set; }

    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User User { get; set; }
}

public class TravelHistoryAqours : TravelHistoryBase
{
    //Database key
    [JsonIgnore, Key] public int DbId { get; set; }

    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User User { get; set; }
}

public class TravelHistorySaintSnow : TravelHistoryBase
{
    //Database key
    [JsonIgnore, Key] public int DbId { get; set; }

    //Database association to user
    [JsonIgnore, ForeignKey("User")] public ulong UserID { get; set; }
    [JsonIgnore] public User User { get; set; }
}