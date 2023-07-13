namespace LLServer.Models.UserDataModel;

[Serializable]
public class UserDataContainer
{
    public UserData UserData { get; set; } = new();
    public UserDataAqours UserDataAqours { get; set; } = new();
    public UserDataSaintSnow? UserDataSaintSnow { get; set; }
    
    public MemberData[] Members { get; set; } = new MemberData[0];
    public MemberCardData[] MemberCards { get; set; } = new MemberCardData[0];
    
    //skill card
    //memorial card
    //item
    public MusicData[] Musics { get; set; } = new MusicData[0];
    public LiveData[] Lives { get; set; } = new LiveData[0];
    //stages data
    //game history
    //game history aqours
    //game history saint snow
    //travel history
    //travel history aqours
    //travel history saint snow
    public MailBoxItem[] MailBox { get; set; } = new MailBoxItem[0];
    //specials
    public string Flags { get; set; } = string.Empty;
    //achievements
    //yell achievements
    //limited achievements
    //mission
    //mission point
    public int[] DailyRecords { get; set; } = new int[0];
    //honors
    public ScfesProfile ScfesProfile { get; set; } = new();
    //sif prints
    //travel
    //travel pamphlets
    //travel talks
    public GachaStatus[] GachaStatus { get; set; } = new GachaStatus[0];
    //card frames
    //snap frames
    //snap stamps
    public NamePlate[] NamePlates { get; set; } = new NamePlate[0];
    //badges
    //event status
    //event rewards
    //event result
    public bool FirstLogin { get; set; } = true;
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
                ProfileCardId1 = 0,
                ProfileCardId2 = 0,
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
                ProfileCardId1 = 0,
                ProfileCardId2 = 0,
            },
            UserDataSaintSnow = new UserDataSaintSnow()
            {
                CharacterId = 0,
                Honor = 0,
                Badge = 0,
                Nameplate = 0,
                ProfileCardId1 = 0,
                ProfileCardId2 = 0,
            },
            Members = new MemberData[1]
            {
                new MemberData()
                {
                    CharacterId = 0,
                    CardMemberId = 0,
                    YellPoint = 0,
                    CardMemorialId = 0,
                    AchieveRank = 69,
                    Main = 0,
                    Camera = 0,
                    Stage = 0,
                    SelectCount = 0,
                    New = false
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
                    LiveId = 0,
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
            Flags = "0",
            DailyRecords = new int[1]
            {
                0
            },
            ScfesProfile = new ScfesProfile()
            {
                Enable = false
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
            FirstLogin = true,
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
            Musics = Musics,
            Lives = Lives,
            MailBox = MailBox,
            Flags = Flags,
            DailyRecords = DailyRecords,
            ScfesProfile = ScfesProfile,
            GachaStatus = GachaStatus,
            NamePlates = NamePlates,
            FirstLogin = FirstLogin,
            StampCards = StampCards,
            StampCardRewards = StampCardRewards,
            ActiveInformation = ActiveInformation
        }; 
    }

    public void InitializeUserData(InitializeUserData input)
    {
        // TODO: Use a proper mapper for this, like https://mapperly.riok.app/docs/intro
        
        if(input.UserData != null) UserData = input.UserData;
        if(input.UserDataAqours != null) UserDataAqours = input.UserDataAqours;

        FirstLogin = false;
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