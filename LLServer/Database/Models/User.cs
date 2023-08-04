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
    [Required] public List<Achievement> Achievements { get; set; } = new();
    [Required] public List<YellAchievement> YellAchievements { get; set; } = new();
    [Required] public List<AchievementRecordBook> AchievementRecordBooks { get; set; } = new();
    
    //items
    [Required] public List<Item> Items { get; set; } = new();
    [Required] public List<SpecialItem> SpecialItems { get; set; } = new();
    
    //Unlockables
    [Required] public List<NamePlate> NamePlates { get; set; } = new();
    [Required] public List<Badge> Badges { get; set; } = new();

    //other data
    public string Flags { get; set; } = "";

    public static User GuestUser { get; } = InitializeGuestUser();

    private static User InitializeGuestUser()
    {
        User u = new()
        {
            UserId = 0,
            CardId = "7020392000000000",
            Initialized = false,
            UserData = new UserData()
            {
                IdolKind = 0,
                CharacterId = 0,
                PlayLs4 = 1
            },
            UserDataAqours = new UserDataAqours(),
            UserDataSaintSnow = new UserDataSaintSnow(),
            //add member data for all members
            Members = MemberData.MemberIds.Select(x => new MemberData()
            {
                CharacterId = x,
                CardMemberId = MemberCardData.InitialMemberCards[x],
                CardMemorialId = MemberCardData.InitialMemorialCards[x],
                AchieveRank = 1,
                New = false
            }).ToList(),
            //add default cards for all members
            MemberCards = MemberCardData.InitialMemberCards.Where(x => x != 0).Select(x => new MemberCardData()
            {
                CardMemberId = x,
                Count = 1,
                New = true
            }).ToList(),
            LiveDatas = new List<PersistentLiveData>(),
            TravelData = new List<TravelData>(),
            TravelPamphlets = new List<TravelPamphlet>(),
            TravelHistory = new List<TravelHistory>(),
            TravelHistoryAqours = new List<TravelHistoryAqours>(),
            TravelHistorySaintSnow = new List<TravelHistorySaintSnow>(),
            Achievements = new List<Achievement>(),
            YellAchievements = new List<YellAchievement>(),
            AchievementRecordBooks = new List<AchievementRecordBook>(),
            Items = new List<Item>(),
            SpecialItems = new List<SpecialItem>(),
            NamePlates = new List<NamePlate>(),
            Badges = new List<Badge>(),
        };

        return u;
    }
}