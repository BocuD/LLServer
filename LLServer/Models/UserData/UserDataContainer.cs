namespace LLServer.Models.UserData;

[Obsolete("Old class that was used for testing, remains purely for the dummy data that is in here"), Serializable]
public class UserDataContainer
{
    public UserData               UserData          { get; set; } = new();
    public UserDataAqours         UserDataAqours    { get; set; } = new();
    public UserDataSaintSnow      UserDataSaintSnow { get; set; } = new();
    public List<MemberData>       Members           { get; set; } = new();
    public List<MemberCardData>   MemberCards       { get; set; } = new();
    public List<SkillCardData>    SkillCards        { get; set; } = new();
    public List<MemorialCardData> MemorialCards     { get; set; } = new();
    public List<Item>             Items             { get; set; } = new();
    public List<MusicData>        Musics            { get; set; } = new();
    public List<LiveData>         Lives             { get; set; } = new();
    public List<StageData>        Stages            { get; set; } = new();

    //game history
    //game history aqours
    //game history saint snow
    public TravelHistoryBase[]     TravelHistory          { get; set; } = new TravelHistoryBase[0];
    public TravelHistoryBase[]     TravelHistoryAqours    { get; set; } = new TravelHistoryBase[0];
    public TravelHistoryBase[]     TravelHistorySaintSnow { get; set; } = new TravelHistoryBase[0];
    public List<MailBoxItem>       MailBox                { get; set; } = new();
    public List<SpecialItem>       Specials               { get; set; } = new();
    public string                  Flags                  { get; set; } = "";
    public Achievement[]           Achievements           { get; set; } = new Achievement[0];
    public AchievementRecordBook[] RecordBooks            { get; set; } = new AchievementRecordBook[0];
    public YellAchievement[]       YellAchievements       { get; set; } = new YellAchievement[0];
    public LimitedAchievement[]    LimitedAchievements    { get; set; } = new LimitedAchievement[0];

    //max amount seems to be 256
    public Mission[]    Missions     { get; set; } = new Mission[0];
    public MissionPoint MissionPoint { get; set; } = new();
    public int[]        DailyRecords { get; set; } = new int[0];
    public HonorData[]  Honors       { get; set; } = new HonorData[0];
    public ScfesProfile ScfesProfile { get; set; } = new();

    //sif prints
    public TravelData[]     Travels         { get; set; } = new TravelData[0];
    public TravelPamphlet[] TravelPamphlets { get; set; } = new TravelPamphlet[0];

    //travel talks
    public GachaStatus[] GachaStatus { get; set; } = new GachaStatus[0];

    //card frames
    //snap frames
    //snap stamps
    public NamePlate[]         NamePlates        { get; set; } = new NamePlate[0];
    public Badge[]             Badges            { get; set; } = new Badge[0];
    public EventStatus[]       EventStatus       { get; set; } = new EventStatus[0];
    public EventReward[]       EventRewards      { get; set; } = new EventReward[0];
    public EventResult         EventResult       { get; set; } = new();
    public bool                FirstLogin        { get; set; }
    public bool                DiceBonus         { get; set; }
    public StampCard[]         StampCards        { get; set; } = new StampCard[0];
    public StampCardReward[]   StampCardRewards  { get; set; } = new StampCardReward[0];
    public ActiveInformation[] ActiveInformation { get; set; } = new ActiveInformation[0];

    private static UserDataContainer mockStorage = new()
    {
        UserData = new UserData
        {
            CharacterId = 0,
            IdolKind = 0,
            Name = "Test123456",
            NoteSpeedLevel = 0,
            SubMonitorType = 0,
            VolumeBgm = 0,
            VolumeSe = 0,
            VolumeVoice = 0,
            ScreenFilterLevel = 0,
            TenpoName = "1337",
            PlayDate = DateTime.Now.ToString("yyyy-MM-dd"),
            PlaySatellite = 0,
            PlayCenter = 0,
            Level = 1,
            TotalExp = 0,
            Honor = 0,
            Badge = 901001, //μ’s大好き (default badge)
            Nameplate = 201, //アキバ (test nameplate)
            ProfileCard1 = new ProfileCard(0),
            ProfileCard2 = new ProfileCard(0),
            CreditCountSatellite = 0,
            CreditCountCenter = 0
        },
        UserDataAqours = new UserDataAqours
        {
            CharacterId = 0,
            Honor = 0,
            Badge = 902001, //Aqours大好き (default badge)
            Nameplate = 201, //アキバ (test nameplate)
            ProfileCard1 = new ProfileCard(0),
            ProfileCard2 = new ProfileCard(0),
        },
        UserDataSaintSnow = new UserDataSaintSnow
        {
            CharacterId = 0,
            Honor = 0,
            Badge = 0,
            Nameplate = 0,
            ProfileCard1 = new ProfileCard(0),
            ProfileCard2 = new ProfileCard(0),
        },
        Members = new List<MemberData>
        {
            new()
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
        MemberCards = new List<MemberCardData>
        {
            new()
            {
                CardMemberId = 0,
                Count = 2,
                New = false,
                PrintRest = 0
            }
        },
        SkillCards = new List<SkillCardData>
        {
            new()
            {
                CardSkillId = 0,
                SkillLevel = 0,
                New = false,
                PrintRest = 0
            }
        },
        MemorialCards = new List<MemorialCardData>
        {
            new()
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
        Items = new List<Item>
        {
            new()
            {
                ItemId = 0,
                Count = 1,
            }
        },
        Musics = new(),
        Lives = new(),
        Stages = new(),
        TravelHistory = new TravelHistoryBase[]
        {
            new()
            {
                Id = 0,
                CardMemberId = 0,
                SnapBackgroundId = 0,
                OtherCharacterId = 0,
                OtherPlayerName = "Player",
                OtherPlayerNameplate = 201, //アキバ (test nameplate)
                OtherPlayerBadge = 901001, //μ’s大好き (default badge)
                TravelPamphletId = 0,
                CreateType = 0,
                TenpoName = "1337",
                //SnapStampList = new SnapStamp[0],
                //CoopInfo = new CoopInfo[0],
                Created = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss"),
                PrintRest = false
            }
        },
        TravelHistoryAqours = new TravelHistoryBase[0],
        TravelHistorySaintSnow = new TravelHistoryBase[0],
        MailBox = new List<MailBoxItem>
        {
            new()
            {
                Id = 0,
                ItemId = 0,
                Attrib = 0,
                Category = 0,
                Count = 1
            }
        },
        Specials = new List<SpecialItem>
        {
            new()
            {
                IdolKind = 0,
                SpecialId = 0,
            }
        },
        Flags = "0",
        Achievements = new Achievement[]
        {
            new()
            {
                AchievementId = 100,
                Unlocked = false,
                New = false
            }
        },
        RecordBooks = new AchievementRecordBook[]
        {
            new()
            {
                Type = "countFinale",
                Values = new int[0]
            },
            new()
            {
                Type = "countSkillInvoke",
                Values = new int[0]
            },
            new()
            {
                Type = "countSelectMember",
                Values = new int[0]
            },
            new()
            {
                Type = "countSelectUnit",
                Values = new int[0]
            },
            new()
            {
                Type = "countGetMemberCard",
                Values = new int[0]
            },
            new()
            {
                Type = "countGetSkillCard",
                Values = new int[0]
            },
            new()
            {
                Type = "countPhotograph",
                Values = new int[0]
            },
            new()
            {
                Type = "countVisualScoreIconSuccess",
                Values = new int[0]
            }
        },
        YellAchievements = new YellAchievement[]
        {
            new()
            {
                YellAchievementId = 0,
                Unlocked = false,
                New = false
            }
        },
        LimitedAchievements = new LimitedAchievement[]
        {
            new()
            {
                LimitedAchievementId = 0,
                Unlocked = false,
                New = false
            }
        },
        Missions = new Mission[]
        {
            new()
            {
                MissionId = 0,
                Achieved = false,
                Value = 0
            }
        },
        MissionPoint = new MissionPoint
        {
            Point = 0,
            AchievedPoint = 0
        },
        DailyRecords = new []
        {
            0
        },
        Honors = new HonorData[]
        {
            new()
            {
                HonorId = 0,
                Unlocked = false,
                New = false
            }
        },
        ScfesProfile = new ScfesProfile
        {
            Enable = false
        },
        Travels = new TravelData[]
        {
            new()
            {
                Slot = 0,
                TravelPamphletId = 0,
                CharacterId = 0,
                CardMemorialId = 0,
                Positions = new[] { 0, 1, 2 },
                LastLandmark = 0,
                Modified = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss")
            }
        },
        TravelPamphlets = new TravelPamphlet[]
        {
            new()
            {
                TravelPamphletId = 0,
                Round = 0,
                TotalTalkCount = 0,
                TotalDiceCount = 0,
                IsNew = false,
                TravelExRewards = new [] { 0 }
            }
        },
        GachaStatus = new GachaStatus[]
        {
            new()
            {
                GachaId = 0,
                IdolKind = 0,
                InUse = false,
                UsageCount = 0
            }
        },
        NamePlates = new NamePlate[]
        {
            new()
            {
                NamePlateId = 201, //アキバ (test nameplate)
                New = false
            }
        },
        Badges = new Badge[]
        {
            new()
            {
                BadgeId = 901001, //μ’s大好き badge
                New = false
            },
            new()
            {
                BadgeId = 901002, //Aqours大好き badge
                New = false
            }
        },
        EventStatus = new EventStatus[]
        {
            new()
            {
                EventId = 0,
                EventPoint = 0,
                Level = 1,
                Rank = 0,
                NextReward = 0,
                FirstPlay = 0
            }
        },
        EventRewards = new EventReward[]
        {
            new()
            {
                EventId = 0,
                RewardCategory = 0,
                RewardId = 0,
                EventPoint = 0,
                Rank = 0,
                RewardNum = 0
            }
        },
        EventResult = new EventResult
        {
            EventId = 0,
            AddPoint = 0,
            EventPoint = 0,
            UpdateScore = 0,
            Rewards = new EventResultReward[]
            {
                new()
                {
                    Reward = new EventResultReward.EventResultRewardData
                    {
                        RequirePoint = 0,
                        RewardArg = 0,
                        RewardType = 0
                    },
                    NextReward = new EventResultReward.EventResultRewardData
                    {
                        RequirePoint = 0,
                        RewardArg = 0,
                        RewardType = 0
                    }
                }
            }
        },
        //timestamp
        FirstLogin = false,
        DiceBonus = false,
        StampCards = new StampCard[]
        {
            //todo: actual stamp card implementation is untested
            new()
            {
                StampCardId = 0,
                StampCount = 10,
                Achieved = false,
                StampCharacters = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }
            }
        },
        StampCardRewards = new StampCardReward[]
        {
            //todo: actual stamp card implementation is untested
            new()
            {
                StampCardId = 0,
                CardMemberId = 0,
                TradeCoin = false
            }
        },
        ActiveInformation = new ActiveInformation[0]
    };
}