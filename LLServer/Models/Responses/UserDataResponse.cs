using System.Text.Json.Serialization;
using LLServer.Models.Travel;
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
    public List<MemberData> Members { get; set; } = new();

    [JsonPropertyName("membercard")]
    public List<MemberCardData> MemberCards { get; set; } = new();

    [JsonPropertyName("skillcard")]
    public List<SkillCardData> SkillCards { get; set; } = new();

    [JsonPropertyName("memorialcard")] 
    public List<MemorialCardData> MemorialCards { get; set; } = new();

    [JsonPropertyName("item")]
    public List<Item> Items { get; set; } = new();

    [JsonPropertyName("musics")]
    public List<MusicData> Musics { get; set; } = new();

    [JsonPropertyName("lives")]
    public List<LiveData> Lives { get; set; } = new();

    [JsonPropertyName("stages")] 
    public List<StageData> Stages { get; set; } = new();

    [JsonPropertyName("game_history")]
    public List<GameHistory> GameHistory { get; set; } = new();
    
    [JsonPropertyName("game_history_aqours")]
    public List<GameHistory> GameHistoryAqours { get; set; } = new();
    
    [JsonPropertyName("game_history_saintsnow")]
    public List<GameHistory> GameHistorySaintSnow { get; set; } = new();
    
    [JsonPropertyName("travel_history")]
    public TravelHistory[] TravelHistory { get; set; } = Array.Empty<TravelHistory>();

    [JsonPropertyName("travel_history_aqours")]
    public TravelHistory[] TravelHistoryAqours { get; set; } = Array.Empty<TravelHistory>();

    [JsonPropertyName("travel_history_saintsnow")]
    public TravelHistory[] TravelHistorySaintSnow { get; set; } = Array.Empty<TravelHistory>();

    [JsonPropertyName("mailbox")]
    public List<MailBoxItem> MailBox { get; set; } = new();

    [JsonPropertyName("specials")]
    public List<SpecialItem> SpecialItems { get; set; } = new();

    //example value:
    //00100000010110001000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000
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
    
    
    //todo: hardcoded mess
    [JsonPropertyName("gacha_status")]
    public GachaStatus[] GachaStatus { get; set; } = 
    {
        new()
        {
            GachaId = 1,
            IdolKind = 0,
            InUse = false,
            UsageCount = 1
        },
        new()
        {
            GachaId = 2,
            IdolKind = 0,
            InUse = false,
            UsageCount = 1
        }
    };

    [JsonPropertyName("card_frames")]
    public CardFrame[] CardFrames { get; set; } = Array.Empty<CardFrame>();
    
    //snap frames
    [JsonPropertyName("snap_stamps")]
    public int[] SnapStamps { get; set; } = Array.Empty<int>();
    
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
    public string Now => DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss");

    [JsonPropertyName("first_login")]
    public bool FirstLogin { get; set; }

    [JsonPropertyName("dice_bonus")] 
    public bool DiceBonus { get; set; } = true;

    [JsonPropertyName("stamp_cards")]
    public StampCard[] StampCards { get; set; } = Array.Empty<StampCard>();

    [JsonPropertyName("stamp_card_rewards")]
    public StampCardReward[] StampCardRewards { get; set; } = Array.Empty<StampCardReward>();

    [JsonPropertyName("active_information")]
    public ActiveInformation[] ActiveInformation { get; set; } = Array.Empty<ActiveInformation>();
}