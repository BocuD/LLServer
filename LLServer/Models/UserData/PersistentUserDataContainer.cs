using LLServer.Common;
using LLServer.Controllers.Debugging;
using LLServer.Database.Models;
using LLServer.Mappers;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Travel;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LLServer.Models.UserData;

public class PersistentUserDataContainer
{
    private DbContext Context { get; }
    private User User { get; }
    private readonly bool isGuestUser;
    
    public PersistentUserDataContainer(DbContext context, GameSession session)
    {
        Context = context;

        isGuestUser = session.IsGuest;

        if (session.IsGuest)
        {
            User = User.GuestUser;
        }
        else
        {
            User = session.User;
        }
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        if (!isGuestUser)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
    
    public UserData UserData => User.UserData!;
    public UserDataAqours UserDataAqours => User.UserDataAqours!;
    public UserDataSaintSnow UserDataSaintSnow => User.UserDataSaintSnow!;
    public List<MemberData> Members => User.Members;
    public List<MemberCardData> MemberCards => User.MemberCards;
    public List<SkillCardData> SkillCards => User.SkillCards;
    public List<MemorialCardData> MemorialCards => User.MemorialCards;
    
    public List<MusicData> Musics => User.Musics;

    //wrapper to get livedata from database
    public List<LiveData> Lives
    {
        get
        {
            //get persistent live data from user
            List<LiveData> liveDatas = new();
            LiveDataMapper mapper = new();
            
            liveDatas.AddRange(User.LiveDatas.Select(x => mapper.FromPersistentLiveData(x)));
            return liveDatas;
        }
    }
    
    public List<PersistentLiveData> PersistentLives => User.LiveDatas;

    public List<StageData> Stages => new();
    public string Flags
    {
        get
        {
            if (FlagTester.testMode)
            {
                Log.Information("FlagTester is enabled, returning flags from FlagTester: {flags}", FlagTester.flags);
                return FlagTester.flags;
            }
            return User.Flags;
        }
        set => User.Flags = value;
    }

    //Travel data
    public List<TravelData> Travels => User.TravelData;
    public List<TravelPamphlet> TravelPamphlets => User.TravelPamphlets;
    public List<TravelTalk> TravelTalks => User.TravelTalks;

    //Game history
    public List<GameHistory> GameHistory => User.GameHistory.Where(g => g.IdolKind == 0).ToList();
    public List<GameHistory> GameHistoryAqours => User.GameHistory.Where(g => g.IdolKind == 1).ToList();
    public List<GameHistory> GameHistorySaintSnow => User.GameHistory.Where(g => g.IdolKind == 2).ToList();

    //Travel history
    public List<TravelHistory> TravelHistory => User.TravelHistory.Where(t => t.IdolKind == 0).ToList();
    public List<TravelHistory> TravelHistoryAqours => User.TravelHistory.Where(t => t.IdolKind == 1).ToList();
    public List<TravelHistory> TravelHistorySaintSnow => User.TravelHistory.Where(t => t.IdolKind == 2).ToList();
    
    //Achievements
    public List<Achievement> Achievements => User.Achievements;
    public List<YellAchievement> YellAchievements => User.YellAchievements;
    public List<AchievementRecordBook> AchievementRecordBooks => User.AchievementRecordBooks;
    public List<LimitedAchievement> LimitedAchievements => User.LimitedAchievements;
    
    //Items
    public List<Item> Items => User.Items;
    public List<SpecialItem> SpecialItems => User.SpecialItems;
    
    //Unlockables
    public List<CardFrame> CardFrames => User.CardFrames;
    public List<NamePlate> NamePlates => User.NamePlates;
    public List<Badge> Badges => User.Badges;
    public List<HonorData> Honors => User.Honors;
    
    //MailBox
    public List<MailBoxItem> MailBox => User.MailBox;
    
    
    //Active information
    public List<ActiveInformation> ActiveInformation => new();
    
    //Other
    public bool FirstLogin { get; set; } = false;
    
    public void Initialize(InitializeUserData initializeCommand)
    {
        if (initializeCommand.UserData == null)
        {
            return;
        }
        
        //copy all properties
        ReflectionMapper.Map(initializeCommand.UserData, UserData);
        
        if (initializeCommand.UserDataAqours != null)
            ReflectionMapper.Map(initializeCommand.UserDataAqours, UserDataAqours);
        
        if (initializeCommand.UserDataSaintSnow != null)
            ReflectionMapper.Map(initializeCommand.UserDataSaintSnow, UserDataSaintSnow);

        //check which idol kind is selected
        switch (initializeCommand.UserData.IdolKind)
        {
            case 0: //µ's
                //add default cards for all members
                MemberCards.AddRange(MemberCardData.InitialMemberCards
                    .Where(x => x < 90012)
                    .Where(x => x != 0 && MemberCards.All(y => y.CardMemberId != x)).Select(x =>
                        new MemberCardData
                        {
                            CardMemberId = x,
                            Count = 1,
                            New = false
                        }));
                
                //add default skill cards for all members
                SkillCards.AddRange(SkillCardData.InitialSkillCards.Select(x => new SkillCardData
                {
                    CardSkillId = x,
                    SkillLevel = 1,
                    New = false
                }));
                
                //add default memorial cards for all members
                MemorialCards.AddRange(MemberCardData.InitialMemorialCards
                    .Where(x => x < 10000)
                    .Where(x => x != 0 && MemorialCards.All(y => y.CardMemorialId != x)).Select(x =>
                        new MemorialCardData
                        {
                            CardMemorialId = x,
                            Count = 1,
                            New = false
                        }));
                break;
            
            case 1: //Aqours
                //add default cards for all members
                MemberCards.AddRange(MemberCardData.InitialMemberCards
                    .Where(x => x > 90011 && x < 190902)
                    .Where(x => x != 0 && MemberCards.All(y => y.CardMemberId != x)).Select(x =>
                        new MemberCardData
                        {
                            CardMemberId = x,
                            Count = 1,
                            New = false
                        }));
                
                //add default skill cards for all members
                SkillCards.AddRange(SkillCardData.InitialSkillCardsAqours.Select(x => new SkillCardData
                {
                    CardSkillId = x,
                    SkillLevel = 1,
                    New = false
                }));
                
                //add default memorial cards for all members
                MemorialCards.AddRange(MemberCardData.InitialMemorialCards
                    .Where(x => x > 10000 && x < 20000)
                    .Where(x => x != 0 && MemorialCards.All(y => y.CardMemorialId != x)).Select(x =>
                        new MemorialCardData
                        {
                            CardMemorialId = x,
                            Count = 1,
                            New = false
                        }));
                break;
            
            case 2: //Saint Snow
                //add default cards for all members
                MemberCards.AddRange(MemberCardData.InitialMemberCards
                    .Where(x => x > 190902)
                    .Where(x => x != 0 && MemberCards.All(y => y.CardMemberId != x)).Select(x =>
                        new MemberCardData
                        {
                            CardMemberId = x,
                            Count = 1,
                            New = false
                        }));
                
                //add default skill cards for all members
                SkillCards.AddRange(SkillCardData.InitialSkillCardsSaintSnow.Select(x => new SkillCardData
                {
                    CardSkillId = x,
                    SkillLevel = 1,
                    New = false
                }));
                
                //add default memorial cards for all members
                MemorialCards.AddRange(MemberCardData.InitialMemorialCards
                    .Where(x => x > 20000)
                    .Where(x => x != 0 && MemorialCards.All(y => y.CardMemorialId != x)).Select(x =>
                        new MemorialCardData
                        {
                            CardMemorialId = x,
                            Count = 1,
                            New = false
                        }));
                break;
        }
        
        
        //todo: when the hack above this gets removed this needs to be readded
        
        /*
        //assign first member cards
        int newMemberCharacterId = initializeCommand.UserData.IdolKind switch
        {
            0 => initializeCommand.UserData.CharacterId!.Value,
            1 => initializeCommand.UserDataAqours!.CharacterId!.Value,
            2 => initializeCommand.UserDataSaintSnow!.CharacterId!.Value,
            _ => 1
        };

        //add first member card
        MemberCards.Add(new MemberCardData
        {
            CardMemberId = MemberCardData.InitialMemberCards[newMemberCharacterId],
            Count = 1,
            New = false
        });
        */
        
        //add nameplate, badge, honor from userdata
        NamePlates.Add(new NamePlate
        {
            NamePlateId = UserData.Nameplate,
            New = false
        });
        
        Badges.Add(new Badge
        {
            BadgeId = UserData.Badge,
            New = false
        });
        
        Honors.Add(new HonorData
        {
            HonorId = UserData.Honor,
            Unlocked = true,
            New = false
        });
        
        //initialize other data
        UserData.Level = 1;
        
        //initialize flags
        Flags = new string('0', 200);

        Flags = Flags.SetFlag(0);       //controls tutorial
        Flags = Flags.SetFlag(10);      //group selection tutorial
        
        Flags = Flags.SetFlag(13);      //anniversary snap tutorial

        Flags = Flags.SetFlag(53);      //???
        
        Flags = Flags.SetFlag(181);     //has set username
    }

    public void SetUserData(SetUserDataParam input)
    {
        if (input.UserData != null) ReflectionMapper.Map(input.UserData, UserData);
        if (input.UserDataAqours != null) ReflectionMapper.Map(input.UserDataAqours, UserDataAqours);
        if (input.UserDataSaintSnow != null) ReflectionMapper.Map(input.UserDataSaintSnow, UserDataSaintSnow);

        if (input.EquipSkills != null)
        {
            foreach (EquipSkill equipSkill in input.EquipSkills)
            {
                //find a matching character id in members, if it doesn't exist add a new member entry
                MemberData? member = Members.FirstOrDefault(m => m.CharacterId == equipSkill.CharacterId);
                if (member == null)
                {
                    Members.Add(new MemberData
                    {
                        CharacterId = equipSkill.CharacterId,
                        New = false
                    });
                    member = Members.FirstOrDefault(m => m.CharacterId == equipSkill.CharacterId);
                }

                member.CharacterId = equipSkill.CharacterId;
                member.CardMemberId = equipSkill.CardMemberId;
                member.CardMemorialId = equipSkill.CardMemorialId;
                
                member.Camera = equipSkill.Camera;
                member.Main = equipSkill.Main;
                member.Stage = equipSkill.Stage;
            }
        }

        if (input.MemberYells != null)
        {
            foreach (MemberYell memberYell in input.MemberYells)
            {
                //find a matching character id in members, if it doesn't exist add a new member entry
                MemberData? member = Members.FirstOrDefault(m => m.CharacterId == memberYell.CharacterId);
                if (member == null)
                {
                    Members.Add(new MemberData
                    {
                        CharacterId = memberYell.CharacterId
                    });
                    member = Members.FirstOrDefault(m => m.CharacterId == memberYell.CharacterId);
                }

                if (member != null)
                {
                    member.YellPoint = memberYell.YellPoint;
                }
            }
        }
        
        HandleAttributeUserData(input.UserData);
        HandleAttributeUserData(input.UserDataAqours);
        HandleAttributeUserData(input.UserDataSaintSnow);
        
        //UserData.Set gets called after selecting a song so we can set the flag for the difficulty explanation tutorial here
        Flags = Flags.SetFlag(7);     //difficulty explanation tutorial
    }

    public void HandleAttributeUserData(NullableUserDataBase? input)
    {
        if (input == null) return;
        
        if (input.Nameplate != null)
        {
            //find a matching nameplate in nameplates, if it doesn't exist add a new nameplate entry
            NamePlate? namePlate = NamePlates.FirstOrDefault(n => n.NamePlateId == input.Nameplate);
            if (namePlate == null)
            {
                NamePlates.Add(new NamePlate
                {
                    NamePlateId = input.Nameplate.Value,
                    New = true
                });
            }
        }
        
        if (input.Badge != null)
        {
            //find a matching badge in badges, if it doesn't exist add a new badge entry
            Badge? badge = Badges.FirstOrDefault(b => b.BadgeId == input.Badge);
            if (badge == null)
            {
                Badges.Add(new Badge
                {
                    BadgeId = input.Badge.Value,
                    New = true
                });
            }
        }

        if (input.Honor != null)
        {
            //find a matching honor in honors, if it doesn't exist add a new honor entry
            HonorData? honor = Honors.FirstOrDefault(h => h.HonorId == input.Honor);
            if (honor == null)
            {
                Honors.Add(new HonorData
                {
                    HonorId = input.Honor.Value,
                    Unlocked = true,
                    New = true
                });
            }
        }
    }
}