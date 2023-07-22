using System.Text.Json.Serialization;
using LLServer.Models.UserData;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.Responses;

public class GameEntryResponse : ResponseBase
{
    [JsonPropertyName("userdata")]
    public UserData.UserData UserData { get; set; } = new();

    [JsonPropertyName("userdata_aqours")]
    public UserDataAqours UserDataAqours { get; set; } = new();

    [JsonPropertyName("userdata_saintsnow")]
    public UserDataSaintSnow? UserDataSaintSnow { get; set; }

    [JsonPropertyName("membercard")]
    public List<MemberCardData> MemberCards { get; set; } = new();

    [JsonPropertyName("skillcard")]
    public List<SkillCardData> SkillCards { get; set; } = new();

    [JsonPropertyName("memorialcard")]
    public List<MemorialCardData> MemorialCards { get; set; } = new();

    [JsonPropertyName("item")]
    public List<Item> Items { get; set; } = new();

    [JsonPropertyName("mailbox")]
    public List<MailBoxItem> MailBox { get; set; } = new();

    [JsonPropertyName("specials")]
    public List<SpecialItem> Specials { get; set; } = new();

    [JsonPropertyName("now")] 
    public string Now => DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss");

    [JsonPropertyName("first_login")]
    public bool FirstLogin { get; set; }

    [JsonPropertyName("travel_pamphlets")]
    public TravelPamphlet[] TravelPamphlets { get; set; } = new TravelPamphlet[0];

    [JsonPropertyName("stamp_card_rewards")]
    public StampCardReward[] StampCardRewards { get; set; } = new StampCardReward[0];

    [JsonPropertyName("active_information")]
    public ActiveInformation[] ActiveInformation { get; set; } = new ActiveInformation[0];

    [JsonPropertyName("event_rewards")]
    public EventReward[] EventRewards { get; set; } = new EventReward[0];

    [JsonPropertyName("members")]
    public List<MemberData> Members { get; set; } = new();
}