using System.Text.Json.Serialization;
using LLServer.Models.Requests.Travel;
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
    public List<MemberCardData>? MemberCards { get; set; } = null;

    [JsonPropertyName("skillcard")]
    public List<SkillCardData>? SkillCards { get; set; } = null;

    [JsonPropertyName("memorialcard")] 
    public List<MemorialCardData>? MemorialCards { get; set; } = null;

    [JsonPropertyName("item")]
    public List<Item>? Items { get; set; } = null;

    [JsonPropertyName("musics")]
    public List<MusicData>? Musics { get; set; } = null;

    [JsonPropertyName("lives")]
    public List<LiveData>? Lives { get; set; } = null;

    [JsonPropertyName("stages")] 
    public List<StageData>? Stages { get; set; } = null;

    [JsonPropertyName("game_history")]
    public List<GameHistory>? GameHistory { get; set; } = null;
    
    [JsonPropertyName("game_history_aqours")]
    public List<GameHistory>? GameHistoryAqours { get; set; } = null;
    
    [JsonPropertyName("game_history_saintsnow")]
    public List<GameHistory>? GameHistorySaintSnow { get; set; } = null;

    [JsonPropertyName("travel_history")] 
    public TravelHistory[]? TravelHistory { get; set; } = null;

    [JsonPropertyName("travel_history_aqours")]
    public TravelHistory[]? TravelHistoryAqours { get; set; } = null;

    [JsonPropertyName("travel_history_saintsnow")]
    public TravelHistory[]? TravelHistorySaintSnow { get; set; } = null;

    [JsonPropertyName("mailbox")]
    public List<MailBoxItem> MailBox { get; set; } = new();

    [JsonPropertyName("specials")]
    public List<SpecialItem> SpecialItems { get; set; } = new();

    //example value:
    //00100000010110001000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000
    [JsonPropertyName("flags")]
    public string Flags { get; set; } = "";

    [JsonPropertyName("achievements")]
    public Achievement[]? Achievements { get; set; } = null;

    [JsonPropertyName("record_books")]
    public AchievementRecordBook[]? AchievementRecordBooks { get; set; } = null;

    [JsonPropertyName("yell_achievements")]
    public YellAchievement[]? YellAchievements { get; set; } = null;

    [JsonPropertyName("limited_achievements")]
    public LimitedAchievement[]? LimitedAchievements { get; set; } = null;

    [JsonPropertyName("missions")]
    public Mission[]? Missions { get; set; } = null;

    [JsonPropertyName("mission_point")]
    public MissionPoint? MissionPoint { get; set; } = null;

    [JsonPropertyName("daily_records")]
    public int[]? DailyRecords { get; set; } = null;

    [JsonPropertyName("honors")]
    public HonorData[]? Honors { get; set; } = null;

    //todo: figure out if we want to do anything with this, it seems to be deprecated in the latest version of the game
    [JsonPropertyName("scfes_profile")]
    public ScfesProfileEnabled? ScfesProfile { get; set; } = new ScfesProfileEnabled()
    {
        Enable = true
    };

    [JsonPropertyName("sif_prints")]
    public SifPrint[]? SifPrints { get; set; } = null;
    
    [JsonPropertyName("travels")] 
    public TravelData[]? Travels { get; set; } = null;

    [JsonPropertyName("travel_pamphlets")] 
    public TravelPamphlet[]? TravelPamphlets { get; set; } = null;

    [JsonPropertyName("travel_talks")] 
    public TravelTalk[]? TravelTalks { get; set; } = null;
    
    
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
    public CardFrame[]? CardFrames { get; set; } = null;
    
    //snap frames
    [JsonPropertyName("snap_stamps")]
    public int[]? SnapStamps { get; set; } = null;
    
    [JsonPropertyName("nameplates")]
    public NamePlate[]? NamePlates { get; set; } = null;

    [JsonPropertyName("badges")]
    public Badge[]? Badges { get; set; } = null;

    [JsonPropertyName("event_status")]
    public EventStatus[]? EventStatus { get; set; } = null;

    [JsonPropertyName("event_rewards")]
    public EventReward[]? EventRewards { get; set; } = null;

    [JsonPropertyName("event_result")]
    public EventResult? EventResult { get; set; } = null;

    [JsonPropertyName("now")] 
    public string Now => DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss");

    [JsonPropertyName("first_login")]
    public bool FirstLogin { get; set; }

    [JsonPropertyName("dice_bonus")] 
    public bool DiceBonus { get; set; } = true;

    [JsonPropertyName("stamp_cards")]
    public StampCard[]? StampCards { get; set; } = null;

    [JsonPropertyName("stamp_card_rewards")]
    public StampCardReward[]? StampCardRewards { get; set; } = null;

    [JsonPropertyName("active_information")]
    public ActiveInformation[]? ActiveInformation { get; set; } = null;
}