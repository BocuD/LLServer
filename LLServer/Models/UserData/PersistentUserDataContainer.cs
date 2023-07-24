using LLServer.Database.Models;
using LLServer.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Models.UserData;

public class PersistentUserDataContainer
{
    private DbContext Context { get; }
    private User User { get; }
    
    public PersistentUserDataContainer(DbContext context, User user)
    {
        Context = context;
        User = user;
    }
    
    public UserData UserData => User.UserData;
    public UserDataAqours UserDataAqours => User.UserDataAqours;
    public UserDataSaintSnow UserDataSaintSnow => User.UserDataSaintSnow;
    public List<MemberData> Members => User.Members;
    public List<MemberCardData> MemberCards => User.MemberCards;
    public List<MusicData> Musics => new();

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
        get => User.Flags;
        set => User.Flags = value;
    }
    
    //Travel data
    public List<TravelData> Travels => User.TravelData;
    public List<TravelPamphlet> TravelPamphlets => User.TravelPamphlets;
    
    //Game history
    public static List<GameHistoryBase> GameHistoryStub = new();
    public static List<GameHistoryBase> GameHistoryAqoursStub = new();
    public static List<GameHistoryBase> GameHistorySaintSnowStub = new();

    public List<GameHistoryBase> GameHistory => GameHistoryStub;
    public List<GameHistoryBase> GameHistoryAqours => GameHistoryAqoursStub;
    public List<GameHistoryBase> GameHistorySaintSnow => GameHistorySaintSnowStub;

    //Travel history
    public List<TravelHistory> TravelHistory => User.TravelHistory;
    public List<TravelHistoryAqours> TravelHistoryAqours => User.TravelHistoryAqours;
    public List<TravelHistorySaintSnow> TravelHistorySaintSnow => User.TravelHistorySaintSnow;
    
    //Achievements
    public List<YellAchievement> YellAchievements => User.YellAchievements;
    public List<AchievementRecordBook> AchievementRecordBooks => User.AchievementRecordBooks;
    
    //Items
    public List<Item> Items => User.Items;
    public List<SpecialItem> SpecialItems => User.SpecialItems;
    
    //Unlockables
    public List<NamePlate> NamePlates => User.NamePlates;
    public List<Badge> Badges => User.Badges;
    
    
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
            New = true,
        });
        
        //add nameplate and badge from userdata
        NamePlates.Add(new NamePlate
        {
            Id = UserData.Nameplate,
            New = false,
        });
        
        Badges.Add(new Badge
        {
            Id = UserData.Badge,
            New = false,
        });
        
        //initialize other data
        UserData.Level = 1;
    }

    public void SetUserData(SetUserData input)
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
                    Members.Add(new MemberData());
                    member = Members.Last();
                }

                member.Camera = equipSkill.Camera;
                member.CharacterId = equipSkill.CharacterId;
                member.CardMemberId = equipSkill.CardMemberId;
                member.CardMemorialId = equipSkill.CardMemorialId;
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
                        CharacterId = memberYell.CharacterId,
                    });
                    member = Members.FirstOrDefault(m => m.CharacterId == memberYell.CharacterId);
                }

                if (member != null)
                {
                    member.YellPoint = memberYell.YellPoint;
                }
            }
        }
    }
}