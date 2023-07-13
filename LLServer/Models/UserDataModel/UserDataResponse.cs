using System.Text.Json.Serialization;

namespace LLServer.Models.UserDataModel;

public class UserDataResponse : ResponseBase
{
    [JsonPropertyName("userdata")] public UserData UserData { get; set; } = new();
    [JsonPropertyName("userdata_aqours")] public UserDataAqours UserDataAqours { get; set; } = new();
    [JsonPropertyName("userdata_saintsnow")] public UserDataSaintSnow? UserDataSaintSnow { get; set; }
    
    [JsonPropertyName("members")] public MemberData[] Members { get; set; } = new MemberData[0];
    [JsonPropertyName("membercard")] public MemberCardData[] MemberCards { get; set; } = new MemberCardData[0];
    
    //skill card
    //memorial card
    //item
    [JsonPropertyName("musics")] public MusicData[] Musics { get; set; } = new MusicData[0];
    [JsonPropertyName("lives")] public LiveData[] Lives { get; set; } = new LiveData[0];
    [JsonPropertyName("stages")] public StageData[] Stages { get; set; } = new StageData[0];
    //game history
    //game history aqours
    //game history saint snow
    //travel history
    //travel history aqours
    //travel history saint snow
    [JsonPropertyName("mailbox")] public MailBoxItem[] MailBox { get; set; } = new MailBoxItem[0];
    //specials
    [JsonPropertyName("flags")] public string Flags { get; set; } = string.Empty;
    //achievements
    //yell achievements
    //limited achievements
    //mission
    //mission point
    [JsonPropertyName("daily_records")] public int[] DailyRecords { get; set; } = new int[0];
    [JsonPropertyName("honors")] public HonorData[] Honors { get; set; } = new HonorData[0];
    [JsonPropertyName("scfes_profile")] public ScfesProfile ScfesProfile { get; set; } = new();
    //sif prints
    //travel
    //travel pamphlets
    //travel talks
    [JsonPropertyName("gacha_status")] public GachaStatus[] GachaStatus { get; set; } = new GachaStatus[0];
    //card frames
    //snap frames
    //snap stamps
    [JsonPropertyName("nameplates")] public NamePlate[] NamePlates { get; set; } = new NamePlate[0];
    //badges
    //event status
    //event rewards
    //event result
    [JsonPropertyName("first_login")] public bool FirstLogin { get; set; } = true;
    //dice bonus
    [JsonPropertyName("stamp_cards")] public StampCard[] StampCards { get; set; } = new StampCard[0];
    [JsonPropertyName("stamp_card_rewards")] public StampCardReward[] StampCardRewards { get; set; } = new StampCardReward[0];
    [JsonPropertyName("active_information")] public ActiveInformation[] ActiveInformation { get; set; } = new ActiveInformation[0];
}