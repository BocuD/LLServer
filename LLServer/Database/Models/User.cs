using System.ComponentModel.DataAnnotations;
using LLServer.Models.UserData;

namespace LLServer.Database.Models;

public class User
{
    [Key]
    public ulong UserId { get; set; }
    
    public string CardId { get; set; } = "7020392000000000";

    public bool Initialized { get; set; }
    
    public Session? Session { get; set; }

    //user data
    [Required]
    public UserData? UserData { get; set; }
    [Required]
    public UserDataAqours? UserDataAqours { get; set; }
    [Required]
    public UserDataSaintSnow? UserDataSaintSnow { get; set; }
    
    //member data
    [Required] public List<MemberData> Members { get; set; } = new();
    [Required] public List<MemberCardData> MemberCards { get; set; } = new();
    
    //score and unlock data
    [Required] public List<PersistentLiveData> LiveDatas { get; set; } = new();
    
    //travel data
    [Required] public List<TravelData> TravelData { get; set; } = new();
    [Required] public List<TravelPamphlet> TravelPamphlets { get; set; } = new();
    [Required] public List<TravelHistory> TravelHistory { get; set; } = new();
    [Required] public List<TravelHistoryAqours> TravelHistoryAqours { get; set; } = new();
    [Required] public List<TravelHistorySaintSnow> TravelHistorySaintSnow { get; set; } = new();
    
    //achievements
    [Required] public List<AchievementRecordBook> AchievementRecordBooks { get; set; } = new();
    
    //other data
    public string Flags { get; set; } = "";
}