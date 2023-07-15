using System.Text.Json.Serialization;

namespace LLServer.Models.UserDataModel;

public class UserDataResponse : ResponseBase
{
    [JsonPropertyName("userdata")] public UserData UserData { get; set; } = new();
    [JsonPropertyName("userdata_aqours")] public UserDataAqours UserDataAqours { get; set; } = new();
    [JsonPropertyName("userdata_saintsnow")] public UserDataSaintSnow? UserDataSaintSnow { get; set; }
    
    [JsonPropertyName("members")] public MemberData[] Members { get; set; } = new MemberData[0];
    [JsonPropertyName("membercard")] public MemberCardData[] MemberCards { get; set; } = new MemberCardData[0];
    
    [JsonPropertyName("skillcard")] public SkillCardData[] SkillCards { get; set; } = new SkillCardData[0];
    [JsonPropertyName("memorialcard")] public MemorialCardData[] MemorialCards { get; set; } = new MemorialCardData[0];
    [JsonPropertyName("item")] public Item[] Items { get; set; } = new Item[0];
    [JsonPropertyName("musics")] public MusicData[] Musics { get; set; } = new MusicData[0];
    [JsonPropertyName("lives")] public LiveData[] Lives { get; set; } = new LiveData[0];
    [JsonPropertyName("stages")] public StageData[] Stages { get; set; } = new StageData[0];
    //game history
    //game history aqours
    //game history saint snow
    [JsonPropertyName("travel_history")] public TravelHistory[] TravelHistory { get; set; } = new TravelHistory[0];
    [JsonPropertyName("travel_history_aqours")] public TravelHistory[] TravelHistoryAqours { get; set; } = new TravelHistory[0];
    [JsonPropertyName("travel_history_saintsnow")] public TravelHistory[] TravelHistorySaintSnow { get; set; } = new TravelHistory[0];
    [JsonPropertyName("mailbox")] public MailBoxItem[] MailBox { get; set; } = new MailBoxItem[0];
    [JsonPropertyName("specials")] public SpecialData[] Specials { get; set; } = new SpecialData[0];
    [JsonPropertyName("flags")] public string Flags { get; set; } = "";
    [JsonPropertyName("achievements")] public Achievement[] Achievements { get; set; } = new Achievement[0];
    [JsonPropertyName("record_books")] public AchievementRecordBook[] RecordBooks { get; set; } = new AchievementRecordBook[0];
    [JsonPropertyName("yell_achievements")] public YellAchievement[] YellAchievements { get; set; } = new YellAchievement[0];
    [JsonPropertyName("limited_achievements")] public LimitedAchievement[] LimitedAchievements { get; set; } = new LimitedAchievement[0];
    [JsonPropertyName("missions")] public Mission[] Missions { get; set; } = new Mission[0];
    [JsonPropertyName("mission_point")] public MissionPoint MissionPoint { get; set; } = new();
    [JsonPropertyName("daily_records")] public int[] DailyRecords { get; set; } = new int[0];
    [JsonPropertyName("honors")] public HonorData[] Honors { get; set; } = new HonorData[0];
    [JsonPropertyName("scfes_profile")] public ScfesProfile ScfesProfile { get; set; } = new();
    //sif prints
    [JsonPropertyName("travels")] public TravelData[] Travels { get; set; } = new TravelData[0];
    [JsonPropertyName("travel_pamphlets")] public TravelPamphlet[] TravelPamphlets { get; set; } = new TravelPamphlet[0];
    //travel talks
    [JsonPropertyName("gacha_status")] public GachaStatus[] GachaStatus { get; set; } = new GachaStatus[0];
    //card frames
    //snap frames
    //snap stamps
    [JsonPropertyName("nameplates")] public NamePlate[] NamePlates { get; set; } = new NamePlate[0];
    [JsonPropertyName("badges")] public Badge[] Badges { get; set; } = new Badge[0];
    [JsonPropertyName("event_status")] public EventStatus[] EventStatus { get; set; } = new EventStatus[0];
    [JsonPropertyName("event_rewards")] public EventReward[] EventRewards { get; set; } = new EventReward[0];
    [JsonPropertyName("event_result")] public EventResult EventResult { get; set; } = new EventResult();
    [JsonPropertyName("now")] public string Now { get; set; } = "";
    [JsonPropertyName("first_login")] public string FirstLogin { get; set; } = "";
    [JsonPropertyName("dice_bonus")] public bool DiceBonus { get; set; } = false;
    [JsonPropertyName("stamp_cards")] public StampCard[] StampCards { get; set; } = new StampCard[0];
    [JsonPropertyName("stamp_card_rewards")] public StampCardReward[] StampCardRewards { get; set; } = new StampCardReward[0];
    [JsonPropertyName("active_information")] public ActiveInformation[] ActiveInformation { get; set; } = new ActiveInformation[0];
}