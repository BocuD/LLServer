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
    public MemberCardData[] MemberCards { get; set; } = new MemberCardData[0];

    [JsonPropertyName("skillcard")]
    public SkillCardData[] SkillCards { get; set; } = new SkillCardData[0];

    [JsonPropertyName("memorialcard")]
    public MemorialCardData[] MemorialCards { get; set; } = new MemorialCardData[0];

    [JsonPropertyName("item")]
    public Item[] Items { get; set; } = new Item[0];

    [JsonPropertyName("mailbox")]
    public MailBoxItem[] MailBox { get; set; } = new MailBoxItem[0];

    [JsonPropertyName("specials")]
    public SpecialData[] Specials { get; set; } = new SpecialData[0];

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
    public MemberData[] Members { get; set; } = new MemberData[0];
}