using System.Text.Json.Serialization;
using LLServer.Models.UserData;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LLServer.Models.Responses;

public class UserDataResponse : ResponseBase
{
    [JsonPropertyName("userdata")]
    public UserData.UserData UserData { get; set; } = new();

    [JsonPropertyName("userdata_aqours")]
    public UserDataAqours UserDataAqours { get; set; } = new();

    [JsonPropertyName("userdata_saintsnow")]
    public UserDataSaintSnow? UserDataSaintSnow { get; set; }

    [JsonPropertyName("members")]
    public MemberData[] Members { get; set; } = Array.Empty<MemberData>();

    [JsonPropertyName("membercard")]
    public MemberCardData[] MemberCards { get; set; } = Array.Empty<MemberCardData>();

    [JsonPropertyName("skillcard")]
    public SkillCardData[] SkillCards { get; set; } = Array.Empty<SkillCardData>();

    [JsonPropertyName("memorialcard")]
    public MemorialCardData[] MemorialCards { get; set; } = Array.Empty<MemorialCardData>();

    [JsonPropertyName("item")]
    public Item[] Items { get; set; } = Array.Empty<Item>();

    [JsonPropertyName("musics")]
    public MusicData[] Musics { get; set; } = Array.Empty<MusicData>();

    [JsonPropertyName("lives")]
    public LiveData[] Lives { get; set; } = Array.Empty<LiveData>();

    [JsonPropertyName("stages")]
    public StageData[] Stages { get; set; } = Array.Empty<StageData>();

    //game history
    //game history aqours
    //game history saint snow
    [JsonPropertyName("travel_history")]
    public TravelHistory[] TravelHistory { get; set; } = Array.Empty<TravelHistory>();

    [JsonPropertyName("travel_history_aqours")]
    public TravelHistory[] TravelHistoryAqours { get; set; } = Array.Empty<TravelHistory>();

    [JsonPropertyName("travel_history_saintsnow")]
    public TravelHistory[] TravelHistorySaintSnow { get; set; } = Array.Empty<TravelHistory>();

    [JsonPropertyName("mailbox")]
    public MailBoxItem[] MailBox { get; set; } = Array.Empty<MailBoxItem>();

    [JsonPropertyName("specials")]
    public SpecialData[] Specials { get; set; } = Array.Empty<SpecialData>();

    [JsonPropertyName("flags")]
    public string Flags { get; set; } = "";

    [JsonPropertyName("achievements")]
    public Achievement[] Achievements { get; set; } = Array.Empty<Achievement>();

    [JsonPropertyName("record_books")]
    public AchievementRecordBook[] RecordBooks { get; set; } = Array.Empty<AchievementRecordBook>();

    [JsonPropertyName("yell_achievements")]
    public YellAchievement[] YellAchievements { get; set; } = Array.Empty<YellAchievement>();

    [JsonPropertyName("limited_achievements")]
    public LimitedAchievement[] LimitedAchievements { get; set; } = Array.Empty<LimitedAchievement>();

    [JsonPropertyName("missions")]
    public Mission[] Missions { get; set; } = Array.Empty<Mission>();

    [JsonPropertyName("mission_point")]
    public MissionPoint MissionPoint { get; set; } = new();

    [JsonPropertyName("daily_records")]
    public int[] DailyRecords { get; set; } = Array.Empty<int>();

    [JsonPropertyName("honors")]
    public HonorData[] Honors { get; set; } = Array.Empty<HonorData>();

    [JsonPropertyName("scfes_profile")]
    public ScfesProfile ScfesProfile { get; set; } = new();

    //sif prints
    [JsonPropertyName("travels")]
    public TravelData[] Travels { get; set; } = Array.Empty<TravelData>();

    [JsonPropertyName("travel_pamphlets")]
    public TravelPamphlet[] TravelPamphlets { get; set; } = Array.Empty<TravelPamphlet>();

    //travel talks
    [JsonPropertyName("gacha_status")]
    public GachaStatus[] GachaStatus { get; set; } = Array.Empty<GachaStatus>();

    //card frames
    //snap frames
    //snap stamps
    [JsonPropertyName("nameplates")]
    public NamePlate[] NamePlates { get; set; } = Array.Empty<NamePlate>();

    [JsonPropertyName("badges")]
    public Badge[] Badges { get; set; } = Array.Empty<Badge>();

    [JsonPropertyName("event_status")]
    public EventStatus[] EventStatus { get; set; } = Array.Empty<EventStatus>();

    [JsonPropertyName("event_rewards")]
    public EventReward[] EventRewards { get; set; } = Array.Empty<EventReward>();

    [JsonPropertyName("event_result")]
    public EventResult EventResult { get; set; } = new EventResult();

    [JsonPropertyName("now")]
    public string Now { get; set; } = "";

    [JsonPropertyName("first_login")]
    public bool FirstLogin { get; set; }

    [JsonPropertyName("dice_bonus")]
    public bool DiceBonus { get; set; }

    [JsonPropertyName("stamp_cards")]
    public StampCard[] StampCards { get; set; } = Array.Empty<StampCard>();

    [JsonPropertyName("stamp_card_rewards")]
    public StampCardReward[] StampCardRewards { get; set; } = Array.Empty<StampCardReward>();

    [JsonPropertyName("active_information")]
    public ActiveInformation[] ActiveInformation { get; set; } = Array.Empty<ActiveInformation>();
}