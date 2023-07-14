namespace LLServer.Models.UserDataModel;

[Serializable]
public class UserDataContainer
{
    public UserData UserData { get; set; } = new();
    public UserDataAqours UserDataAqours { get; set; } = new();
    public UserDataSaintSnow? UserDataSaintSnow { get; set; }

    public MemberData[] Members { get; set; } = new MemberData[0];
    public MemberCardData[] MemberCards { get; set; } = new MemberCardData[0];
    public SkillCardData[] SkillCards { get; set; } = new SkillCardData[0];

    public MemorialCardData[] MemorialCards { get; set; } = new MemorialCardData[0];
    public Item[] Items { get; set; } = new Item[0];
    public MusicData[] Musics { get; set; } = new MusicData[0];
    public LiveData[] Lives { get; set; } = new LiveData[0];

    public StageData[] Stages { get; set; } = new StageData[0];

    //game history
    //game history aqours
    //game history saint snow
    //travel history
    //travel history aqours
    //travel history saint snow
    public MailBoxItem[] MailBox { get; set; } = new MailBoxItem[0];
    public SpecialData[] Specials { get; set; } = new SpecialData[0];

    public string Flags { get; set; } = string.Empty;
    //achievements
    //yell achievements
    //limited achievements

    //max amount seems to be 256
    public Mission[] Missions { get; set; } = new Mission[0];
    public MissionPoint MissionPoint { get; set; } = new();
    public int[] DailyRecords { get; set; } = new int[0];
    public HonorData[] Honors { get; set; } = new HonorData[0];

    public ScfesProfile ScfesProfile { get; set; } = new();

    //sif prints
    //travel
    public TravelPamphlet[] TravelPamphlets { get; set; } = new TravelPamphlet[0];

    //travel talks
    public GachaStatus[] GachaStatus { get; set; } = new GachaStatus[0];

    //card frames
    //snap frames
    //snap stamps
    public NamePlate[] NamePlates { get; set; } = new NamePlate[0];

    public Badge[] Badges { get; set; } = new Badge[0];

    //event status
    public EventReward[] EventRewards { get; set; } = new EventReward[0];

    //event result
    public string Now { get; set; } = "";
    public string FirstLogin { get; set; } = "";

    //dice bonus
    public StampCard[] StampCards { get; set; } = new StampCard[0];
    public StampCardReward[] StampCardRewards { get; set; } = new StampCardReward[0];
    public ActiveInformation[] ActiveInformation { get; set; } = new ActiveInformation[0];

    private static UserDataContainer mockStorage = new()
    {
        UserData = new UserData()
        {
            CharacterId = 0,
            IdolKind = 0,
            Name = "Test123456",
            NoteSpeedLevel = 0,
            SubMonitorType = 0,
            VolumeBgm = 0,
            VolumeSe = 0,
            VolumeVoice = 0,
            TenpoName = "1337",
            PlayDate = DateTime.Now.ToString("yyyy-MM-dd"),
            PlaySatellite = 1,
            PlayCenter = 0,
            Level = 69,
            TotalExp = 1000,
            Honor = 5,
            Badge = 1,
            Nameplate = 0,
            ProfileCardId1 = "",
            ProfileCardId2 = "",
            CreditCountSatellite = 0,
            CreditCountCenter = 0,
            PlayLs4 = 0
        },
        UserDataAqours = new UserDataAqours()
        {
            CharacterId = 0,
            Honor = 0,
            Badge = 0,
            Nameplate = 0,
            ProfileCardId1 = "",
            ProfileCardId2 = "",
        },
        UserDataSaintSnow = new UserDataSaintSnow()
        {
            CharacterId = 0,
            Honor = 0,
            Badge = 0,
            Nameplate = 0,
            ProfileCardId1 = "",
            ProfileCardId2 = "",
        },
        Members = new MemberData[1]
        {
            new MemberData()
            {
                CharacterId = 1,
                CardMemberId = 0,
                YellPoint = 0,
                CardMemorialId = 2000,
                AchieveRank = 1,
                Main = 0,
                Camera = 0,
                Stage = 0,
                SelectCount = 0,
                New = true
            }
        },
        MemberCards = new MemberCardData[1]
        {
            new MemberCardData()
            {
                CardMemberId = 0,
                Count = 2,
                New = false,
                PrintRest = 0
            }
        },
        SkillCards = new SkillCardData[1]
        {
            new SkillCardData()
            {
                CardSkillId = 0,
                SkillLevel = 0,
                New = false,
                PrintRest = 0
            }
        },
        MemorialCards = new MemorialCardData[1]
        {
            new MemorialCardData()
            {
                CardMemorialId = 0,
                Count = 1,
                GoalCount = 0,
                New = false,
                PrintRest = 0,
                SelectCount = 0,
                TalkCount = 0
            }
        },
        Items = new Item[1]
        {
            new Item()
            {
                ItemId = 0,
                Count = 1,
            }
        },
        Musics = new MusicData[1]
        {
            new MusicData()
            {
                MusicId = 0,
                Unlocked = true,
                New = false
            }
        },
        Lives = new LiveData[1]
        {
            new LiveData()
            {
                LiveId = 10,
                SelectCount = 0,
                Unlocked = true,
                New = false,
                FullCombo = false,
                TotalHiScore = 69420,
                TechnicalHiScore = 420,
                TechnicalHiRate = 1,
                CoopTotalHiScore2 = 0,
                CoopTotalHiScore3 = 0,
                PlayerCount1 = 0,
                PlayerCount2 = 0,
                PlayerCount3 = 0,
                RankCount0 = 0,
                RankCount1 = 0,
                RankCount2 = 0,
                RankCount3 = 0,
                RankCount4 = 0,
                RankCount5 = 0,
                RankCount6 = 0,
                TrophyCountGold = 0,
                TrophyCountSilver = 0,
                TrophyCountBronze = 0,
                FinaleCount = 0,
                TechnicalRank = 0
            }
        },
        Stages = StageData.stages.Select(x => new StageData() { StageId = x, New = false, SelectCount = 0, Unlocked = true })
                .ToArray()
        ,

        MailBox = new MailBoxItem[1]
        {
            new MailBoxItem()
            {
                Id = 0,
                ItemId = 0,
                Attrib = 0,
                Category = 0,
                Count = 1
            }
        },
        Specials = new SpecialData[1]
        {
            new SpecialData()
            {
                IdolKind = 0,
                SpecialId = 0,
            }
        },
        Flags = "0",
        Missions = new Mission[1]
        {
            new Mission()
            {
                MissionId = 0,
                Achieved = false,
                Value = 0
            }
        },
        MissionPoint = new MissionPoint()
        {
            Point = 0,
            AchievedPoint = 0
        },
        DailyRecords = new int[1]
        {
            0
        },
        Honors = new HonorData[1]
        {
            new HonorData()
            {
                HonorId = 0,
                Unlocked = false,
                New = false
            }
        },
        ScfesProfile = new ScfesProfile()
        {
            Enable = false
        },
        TravelPamphlets = new TravelPamphlet[1]
        {
            new TravelPamphlet()
            {
                TravelPamphletId = 0,
                Round = 0,
                TotalTalkCount = 0,
                TotalDiceCount = 0,
                IsNew = false,
                TravelExRewards = new int[1] {0}
            }
        },
        GachaStatus = new GachaStatus[1]
        {
            new GachaStatus()
            {
                GachaId = 0,
                IdolKind = 0,
                InUse = false,
                UsageCount = 0
            }
        },
        NamePlates = new NamePlate[1]
        {
            new NamePlate()
            {
                NamePlateId = 0,
                New = false
            }
        },
        Badges = new Badge[1]
        {
            new Badge()
            {
                BadgeId = 901001, //μ’s大好き badge
                New = false
            }
        },
        EventRewards = new EventReward[1]
        {
            new EventReward()
            {
                EventId = 0,
                RewardCategory = 0,
                RewardId = 0,
                EventPoint = 0,
                Rank = 0,
                RewardNum = 0
            }
        },
        //timestamp
        Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        FirstLogin = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        StampCards = new StampCard[1]
        {
            //todo: actual stamp card implementation is untested
            new StampCard()
            {
                StampCardId = 0,
                StampCount = 10,
                Achieved = false,
                StampCharacters = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8}
            }
        },
        StampCardRewards = new StampCardReward[1]
        {
            //todo: actual stamp card implementation is untested
            new StampCardReward()
            {
                StampCardId = 0,
                CardMemberId = 0,
                TradeCoin = false
            }
        },
        ActiveInformation = new ActiveInformation[0]
    };

    public static UserDataContainer GetDummyUserDataContainer()
    {
        return mockStorage;
    }

    public UserDataResponse GetUserData()
    {
        return new UserDataResponse()
        {
            // TODO: Use a proper mapper for this, like https://mapperly.riok.app/docs/intro
            //copy all properties
            UserData = UserData,
            UserDataAqours = UserDataAqours,
            UserDataSaintSnow = UserDataSaintSnow,
            Members = Members,
            MemberCards = MemberCards,
            SkillCards = SkillCards,
            MemorialCards = MemorialCards,
            Items = Items,
            Musics = Musics,
            Lives = Lives,
            Stages = Stages,
            MailBox = MailBox,
            Specials = Specials,
            Flags = Flags,
            Missions = Missions,
            MissionPoint = MissionPoint,
            DailyRecords = DailyRecords,
            Honors = Honors,
            ScfesProfile = ScfesProfile,
            TravelPamphlets = TravelPamphlets,
            GachaStatus = GachaStatus,
            NamePlates = NamePlates,
            Badges = Badges,
            EventRewards = EventRewards,
            Now = Now,
            FirstLogin = FirstLogin,
            StampCards = StampCards,
            StampCardRewards = StampCardRewards,
            ActiveInformation = ActiveInformation
        }; 
    }

    public GameEntryResponse GetGameEntry()
    {
        return new GameEntryResponse()
        {
            // TODO: Use a proper mapper for this, like https://mapperly.riok.app/docs/intro
            //copy all properties
            UserData = UserData,
            UserDataAqours = UserDataAqours,
            UserDataSaintSnow = UserDataSaintSnow,
            MemberCards = MemberCards,
            SkillCards = SkillCards,
            MemorialCards = MemorialCards,
            Items = Items,
            MailBox = MailBox,
            Specials = Specials,
            FirstLogin = FirstLogin,
            TravelPamphlets = TravelPamphlets,
            StampCardRewards = StampCardRewards,
            ActiveInformation = ActiveInformation,
            EventRewards = EventRewards,
            Members = Members
        };
    }

    public void InitializeUserData(InitializeUserData input)
    {
        // TODO: Use a proper mapper for this, like https://mapperly.riok.app/docs/intro
        //copy all properties
        if(input.UserData != null) UserData = input.UserData;
        if(input.UserDataAqours != null) UserDataAqours = input.UserDataAqours;
    }
    
    public void SetUserData(SetUserData input)
    {
        // TODO: Use a proper mapper for this, like https://mapperly.riok.app/docs/intro
        
        if(input.UserData != null) UserData = input.UserData;
        if(input.UserDataAqours != null) UserDataAqours = input.UserDataAqours;
        if(input.UserDataSaintSnow != null) UserDataSaintSnow = input.UserDataSaintSnow;
        
        if (input.EquipSkills != null)
        {
            foreach (EquipSkill equipSkill in input.EquipSkills)
            {
                //find a matching character id in members, if it doesn't exist add a new member entry
                MemberData? member = Members.FirstOrDefault(m => m.CharacterId == equipSkill.CharacterId);
                if (member == null)
                {
                    member = new MemberData();
                    Members = Members.Append(member).ToArray();
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
                    member = new MemberData();
                    Members = Members.Append(member).ToArray();
                }

                member.YellPoint = memberYell.YellPoint;
                member.AchieveRank = memberYell.YellRank;
            }
        }
    }
}