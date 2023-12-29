﻿using System.ComponentModel.DataAnnotations;
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
    111111 //controls tutorial
    111111 //group selection tutorial
    0000000000000 //contains at least the aniversary snap tutorial (maybe others) 
     */
    //flag 0:       controls tutorial
    //flag 10:      group selection tutorial
    //flag 13:      anniversary snap tutorial
    //
    //somewhere in the second half: contains whether or not some information items should be shown
    //flag 181:     has set username
    //???
    
    public string Flags { get; set; } = "";

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
            SkillCards = SkillCardData.InitialSkillCards.Select(x => new SkillCardData
            {
                CardSkillId = x,
                SkillLevel = 1,
                New = true
            }).ToList(),
            Flags = "00100000010110001000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000100000000000000000"
        };

        return u;
    }
}