using System.ComponentModel.DataAnnotations;
using LLServer.Common;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Travel;
using LLServer.Models.UserData;

namespace LLServer.Database.Models;

public class User
{
    [Key]
    public ulong UserId { get; set; }
    
    public string CardId { get; set; } = "7020392000000000";

    public bool Initialized { get; set; }
    
    public GameSession? Session { get; set; }

    //user data
    [Required] public UserData UserData { get; set; } = new();
    [Required] public UserDataAqours UserDataAqours { get; set; } = new();
    [Required] public UserDataSaintSnow UserDataSaintSnow { get; set; } = new();
    
    //member data
    [Required] public List<MemberData> Members { get; set; } = new();
    [Required] public List<MemberCardData> MemberCards { get; set; } = new();
    [Required] public List<SkillCardData> SkillCards { get; set; } = new();
    [Required] public List<MemorialCardData> MemorialCards { get; set; } = new();
    
    //score and unlock data
    [Required] public List<PersistentLiveData> LiveDatas { get; set; } = new();
    
    //travel data
    [Required] public List<TravelData> TravelData { get; set; } = new();
    [Required] public List<TravelPamphlet> TravelPamphlets { get; set; } = new();
    [Required] public List<TravelTalk> TravelTalks { get; set; } = new();
    
    //history
    [Required] public List<GameHistory> GameHistory { get; set; } = new();
    
    [Required] public List<TravelHistory> TravelHistory { get; set; } = new();
    
    //achievements
    [Required] public List<Achievement> Achievements { get; set; } = new();
    [Required] public List<YellAchievement> YellAchievements { get; set; } = new();
    [Required] public List<AchievementRecordBook> AchievementRecordBooks { get; set; } = new();
    [Required] public List<LimitedAchievement> LimitedAchievements { get; set; } = new();
    
    //items
    [Required] public List<Item> Items { get; set; } = new();
    [Required] public List<SpecialItem> SpecialItems { get; set; } = new();
    
    //Unlockables
    [Required] public List<CardFrame> CardFrames { get; set; } = new();
    [Required] public List<NamePlate> NamePlates { get; set; } = new();
    [Required] public List<Badge> Badges { get; set; } = new();
    [Required] public List<HonorData> Honors { get; set; } = new();

    //Music
    [Required] public List<MusicData> Musics { get; set; } = new();
    
    //Mailbox
    [Required] public List<MailBoxItem> MailBox { get; set; } = new();

    
    //other data
    //this is actually pain.
    //"documentation"
    /*
    
    flag 0:       controls tutorial
    flag 2:       profile card tutorial (on final result screen)
    flag 3:       profile card tutorial (on live preparation screen)
    flag 7:       difficulty explanation tutorial
    flag 9:       about score tutorial
    flag 10:      group selection tutorial
    flag 11:      odekake school idol explanation tutorial (chika dice, board, rewards)
    flag 12:      odekake school idol explanation tutorial (items!)
    flag 13:      anniversary snap tutorial
    flag 16:      odekake school idol explanation tutorial (making odekake memories)
    
    flag 181:     has set username
    flag 182:     extreme mode is now available! tutorial
    
    
    to document:

    >> after the first half
    - information popups viewed state
    
    >> literally everything related to center mode lol

    
    //a version from a saved profile:
00100000010110001000000000000000000000000000000000000100000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000
     */
    
    public string Flags { get; set; } = "";

    //todo: make sure guest scores are saved between song 1 and song 2 (this will require making temporary profiles probably and deleting them after the guest session)
    //todo: initialize the skill card got achievements on guest profiles so the spam can be skipped
    public static User GuestUser { get; } = InitializeGuestUser();

    private static User InitializeGuestUser()
    {
        User u = new()
        {
            UserId = 0,
            CardId = "7020392000000000",
            Initialized = false,
            UserData = new UserData
            {
                Name = "ゲストプレイヤー",
                IdolKind = 0,
                CharacterId = 0,
                PlayLs4 = 1
            },
            UserDataAqours = new UserDataAqours(),
            UserDataSaintSnow = new UserDataSaintSnow(),
            //add member data for all members
            Members = MemberData.MemberIds.Select(x => new MemberData
            {
                CharacterId = x,
                CardMemberId = MemberCardData.InitialMemberCards[x],
                CardMemorialId = MemberCardData.InitialMemorialCards[x],
                AchieveRank = 1,
                New = false
            }).ToList(),
            //add default cards for all members
            MemberCards = MemberCardData.InitialMemberCards.Where(x => x != 0).Select(x => new MemberCardData
            {
                CardMemberId = x,
                Count = 1,
                New = true
            }).ToList(),
            //add default memorial cards for all members
            MemorialCards = MemberCardData.InitialMemorialCards.Where(x => x != 0).Select(x => new MemorialCardData
            {
                CardMemorialId = x,
                Count = 1,
                New = true
            }).ToList(),
            //add default skill cards for all members
            SkillCards = SkillCardData.InitialSkillCards.Select(x => new SkillCardData
            {
                CardSkillId = x,
                SkillLevel = 1,
                New = true
            }).ToList(),
            //in guest mode:
            /*
            controls
            group
            anniversary snap
            diff selection (before song 1)
            about score (after song 1)
            NO diff selection before song 2
            no tutorials after song 2, straight to see you next time
             */
            //construct a flags string that makes sense; here we skip the final screen profile card tutorial and the odekake memories tutorial. Username is set (of course).
            Flags = //new string('0', 200).SetFlag(2).SetFlag(16).SetFlag(181) //no idea what flag 59 does but it doesnt break anything so in the hardcoded version its there :P
            "00100000000000001000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000000000000000000"
        };

        return u;
    }
}