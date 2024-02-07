using System.Text.Json;
using LLServer.Database.Models;
using LLServer.Mappers;
using LLServer.Models.Travel;
using LLServer.Models.UserData;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Database;

using ProfileCard = Models.ProfileCard;

public class ApplicationDbContext : DbContext
{
    private readonly ILogger<ApplicationDbContext> logger;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
        : base(options)
    {
        this.logger = logger;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<GameSession> Sessions { get; set; }
    
    public DbSet<ProfileCard> ProfileCards { get; set; }
    
    public DbSet<UserData> UserData { get; set; }
    public DbSet<UserDataAqours> UserDataAqours { get; set; }
    public DbSet<UserDataSaintSnow> UserDataSaintSnow { get; set; }
    public DbSet<MemberData> MemberData { get; set; }
    public DbSet<MemberCardData> MemberCardData { get; set; }
    public DbSet<SkillCardData> SkillCardData { get; set; }
    public DbSet<MemorialCardData> MemorialCardData { get; set; }
    
    public DbSet<PersistentLiveData> LiveDatas { get; set; }
    
    public DbSet<TravelData> TravelData { get; set; }
    public DbSet<TravelPamphlet> TravelPamphlets { get; set; }
    public DbSet<TravelTalk> TravelTalks { get; set; }
    
    public DbSet<GameHistory> GameHistory { get; set; }
    public DbSet<TravelHistory> TravelHistory { get; set; }
    
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<YellAchievement> YellAchievements { get; set; }
    public DbSet<AchievementRecordBook> AchievementRecordBooks { get; set; }
    public DbSet<LimitedAchievement> LimitedAchievements { get; set; }
    
    public DbSet<Item> Items { get; set; }
    public DbSet<SpecialItem> SpecialItems { get; set; }
    
    public DbSet<CardFrame> CardFrames { get; set; }
    public DbSet<NamePlate> NamePlates { get; set; }
    public DbSet<Badge> Badges { get; set; }
    public DbSet<HonorData> Honors { get; set; }
    
    public DbSet<MusicData> Musics { get; set; }
    
    public DbSet<MailBoxItem> MailBox { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(user => user.UserId);

        modelBuilder.Entity<User>()
            .HasAlternateKey(user => user.CardId);
        
        
        modelBuilder.Entity<GameSession>()
            .HasKey(s => s.SessionId);
        
        modelBuilder.Entity<GameSession>()
            .Property(s => s.SessionId)
            .HasMaxLength(32);

        modelBuilder.Entity<GameSession>()
            .Property(s => s.IsActive)
            .IsRequired();

        modelBuilder.Entity<GameSession>()
            .Property(s => s.CreateTime)
            .IsRequired();

        modelBuilder.Entity<GameSession>()
            .Property(s => s.ExpireTime)
            .IsRequired();
        

        //optional user on sessions
        modelBuilder.Entity<GameSession>()
            .HasOne(s => s.User)
            .WithOne(u => u.Session)
            .HasForeignKey<GameSession>(s => s.UserId)
            .IsRequired(false);
    }
    
    public async Task ImportProfileFromJson(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var importedUser = JsonSerializer.Deserialize<User>(json, options);
        
        if (importedUser == null)
        {
            logger.LogError("Failed to deserialize user from json, aborting profile import!");
            return;
        }
        
        //make sure there is no existing user with this id
        User? existingUser = await Users
            .Where(u => u.CardId == importedUser.CardId)
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            await DeleteProfile(existingUser.UserId);
        }
        
        var newUser = await RegisterNewUser(importedUser.CardId);
        
        if (newUser == null)
        {
            logger.LogError("Failed to create new user for nesicaId {id}, aborting profile import!", importedUser.CardId);
            return;
        }

        ReflectionMapper.Map(importedUser.UserData, newUser.UserData);
        ReflectionMapper.Map(importedUser.UserDataAqours, newUser.UserDataAqours);
        ReflectionMapper.Map(importedUser.UserDataSaintSnow, newUser.UserDataSaintSnow);
        
        //some funny code because references need to be set correctly
        foreach (var member in importedUser.Members)
        {
            var newMember = new MemberData();
            ReflectionMapper.Map(member, newMember);
            newMember.UserID = newUser.UserId;
            newMember.User = newUser;
            newUser.Members.Add(newMember);
        }
        
        foreach (var memberCard in importedUser.MemberCards)
        {
            var newMemberCard = new MemberCardData();
            ReflectionMapper.Map(memberCard, newMemberCard);
            newMemberCard.UserID = newUser.UserId;
            newMemberCard.User = newUser;
            newUser.MemberCards.Add(newMemberCard);
        }
        
        foreach (var skillCard in importedUser.SkillCards)
        {
            var newSkillCard = new SkillCardData();
            ReflectionMapper.Map(skillCard, newSkillCard);
            newSkillCard.UserID = newUser.UserId;
            newSkillCard.User = newUser;
            newUser.SkillCards.Add(newSkillCard);
        }
        
        foreach (var memorialCard in importedUser.MemorialCards)
        {
            var newMemorialCard = new MemorialCardData();
            ReflectionMapper.Map(memorialCard, newMemorialCard);
            newMemorialCard.UserID = newUser.UserId;
            newMemorialCard.User = newUser;
            newUser.MemorialCards.Add(newMemorialCard);
        }
        
        foreach (var liveData in importedUser.LiveDatas)
        {
            var newLiveData = new PersistentLiveData();
            ReflectionMapper.Map(liveData, newLiveData);
            newLiveData.UserID = newUser.UserId;
            newLiveData.User = newUser;
            newUser.LiveDatas.Add(newLiveData);
        }
        
        foreach (var travelData in importedUser.TravelData)
        {
            var newTravelData = new TravelData();
            ReflectionMapper.Map(travelData, newTravelData);
            newTravelData.UserID = newUser.UserId;
            newTravelData.User = newUser;
            newUser.TravelData.Add(newTravelData);
        }
        
        foreach (var travelPamphlet in importedUser.TravelPamphlets)
        {
            var newTravelPamphlet = new TravelPamphlet();
            ReflectionMapper.Map(travelPamphlet, newTravelPamphlet);
            newTravelPamphlet.UserID = newUser.UserId;
            newTravelPamphlet.User = newUser;
            newUser.TravelPamphlets.Add(newTravelPamphlet);
        }
        
        foreach (var travelTalk in importedUser.TravelTalks)
        {
            var newTravelTalk = new TravelTalk();
            ReflectionMapper.Map(travelTalk, newTravelTalk);
            newTravelTalk.UserID = newUser.UserId;
            newTravelTalk.User = newUser;
            newUser.TravelTalks.Add(newTravelTalk);
        }
        
        foreach (var gameHistory in importedUser.GameHistory)
        {
            var newGameHistory = new GameHistory();
            ReflectionMapper.Map(gameHistory, newGameHistory);
            newGameHistory.UserID = newUser.UserId;
            newGameHistory.User = newUser;
            newUser.GameHistory.Add(newGameHistory);
        }
        
        foreach (var travelHistory in importedUser.TravelHistory)
        {
            var newTravelHistory = new TravelHistory();
            ReflectionMapper.Map(travelHistory, newTravelHistory);
            newTravelHistory.UserID = newUser.UserId;
            newTravelHistory.User = newUser;
            newUser.TravelHistory.Add(newTravelHistory);
        }
        
        foreach (var achievement in importedUser.Achievements)
        {
            var newAchievement = new Achievement();
            ReflectionMapper.Map(achievement, newAchievement);
            newAchievement.UserID = newUser.UserId;
            newAchievement.User = newUser;
            newUser.Achievements.Add(newAchievement);
        }
        
        foreach (var yellAchievement in importedUser.YellAchievements)
        {
            var newYellAchievement = new YellAchievement();
            ReflectionMapper.Map(yellAchievement, newYellAchievement);
            newYellAchievement.UserID = newUser.UserId;
            newYellAchievement.User = newUser;
            newUser.YellAchievements.Add(newYellAchievement);
        }
        
        foreach (var achievementRecordBook in importedUser.AchievementRecordBooks)
        {
            var newAchievementRecordBook = new AchievementRecordBook();
            ReflectionMapper.Map(achievementRecordBook, newAchievementRecordBook);
            newAchievementRecordBook.UserID = newUser.UserId;
            newAchievementRecordBook.User = newUser;
            newUser.AchievementRecordBooks.Add(newAchievementRecordBook);
        }
        
        foreach (var limitedAchievement in importedUser.LimitedAchievements)
        {
            var newLimitedAchievement = new LimitedAchievement();
            ReflectionMapper.Map(limitedAchievement, newLimitedAchievement);
            newLimitedAchievement.UserID = newUser.UserId;
            newLimitedAchievement.User = newUser;
            newUser.LimitedAchievements.Add(newLimitedAchievement);
        }
        
        foreach (var item in importedUser.Items)
        {
            var newItem = new Item();
            ReflectionMapper.Map(item, newItem);
            newItem.UserID = newUser.UserId;
            newItem.User = newUser;
            newUser.Items.Add(newItem);
        }
        
        foreach (var specialItem in importedUser.SpecialItems)
        {
            var newSpecialItem = new SpecialItem();
            ReflectionMapper.Map(specialItem, newSpecialItem);
            newSpecialItem.UserID = newUser.UserId;
            newSpecialItem.User = newUser;
            newUser.SpecialItems.Add(newSpecialItem);
        }
        
        foreach (var cardFrame in importedUser.CardFrames)
        {
            var newCardFrame = new CardFrame();
            ReflectionMapper.Map(cardFrame, newCardFrame);
            newCardFrame.UserID = newUser.UserId;
            newCardFrame.User = newUser;
            newUser.CardFrames.Add(newCardFrame);
        }
        
        foreach (var namePlate in importedUser.NamePlates)
        {
            var newNamePlate = new NamePlate();
            ReflectionMapper.Map(namePlate, newNamePlate);
            newNamePlate.UserID = newUser.UserId;
            newNamePlate.User = newUser;
            newUser.NamePlates.Add(newNamePlate);
        }
        
        foreach (var badge in importedUser.Badges)
        {
            var newBadge = new Badge();
            ReflectionMapper.Map(badge, newBadge);
            newBadge.UserID = newUser.UserId;
            newBadge.User = newUser;
            newUser.Badges.Add(newBadge);
        }
        
        foreach (var honor in importedUser.Honors)
        {
            var newHonor = new HonorData();
            ReflectionMapper.Map(honor, newHonor);
            newHonor.UserID = newUser.UserId;
            newHonor.User = newUser;
            newUser.Honors.Add(newHonor);
        }
        
        foreach (var music in importedUser.Musics)
        {
            var newMusic = new MusicData();
            ReflectionMapper.Map(music, newMusic);
            newMusic.UserID = newUser.UserId;
            newMusic.User = newUser;
            newUser.Musics.Add(newMusic);
        }
        
        foreach (var mailBoxItem in importedUser.MailBox)
        {
            var newMailBoxItem = new MailBoxItem();
            ReflectionMapper.Map(mailBoxItem, newMailBoxItem);
            newMailBoxItem.UserID = newUser.UserId;
            newMailBoxItem.User = newUser;
            newUser.MailBox.Add(newMailBoxItem);
        }
        
        newUser.Flags = importedUser.Flags;
        newUser.Initialized = true;
        
        await SaveChangesAsync();
    }

    public async Task<User?> RegisterNewUser(string cardId)
    {
        User user = new()
        {
            CardId = cardId,
            Initialized = false,
            
            UserData = new UserData(),
            UserDataAqours = new UserDataAqours(),
            UserDataSaintSnow = new UserDataSaintSnow(),
            
            Members = new List<MemberData>(),
            MemberCards = new List<MemberCardData>(),
            SkillCards = new List<SkillCardData>(),
            MemorialCards = new List<MemorialCardData>(),
            
            LiveDatas = new List<PersistentLiveData>(),
            
            TravelData = new List<TravelData>(),
            TravelPamphlets = new List<TravelPamphlet>(),
            TravelTalks = new List<TravelTalk>(),
            
            GameHistory = new List<GameHistory>(),
            TravelHistory = new List<TravelHistory>(),
            
            Achievements = new List<Achievement>(),
            YellAchievements = new List<YellAchievement>(),
            AchievementRecordBooks = new List<AchievementRecordBook>(),
            LimitedAchievements = new List<LimitedAchievement>(),
            
            Items = new List<Item>(),
            SpecialItems = new List<SpecialItem>(),
            
            CardFrames = new List<CardFrame>(),
            NamePlates = new List<NamePlate>(),
            Badges = new List<Badge>(),
            Honors = new List<HonorData>(),
            
            Musics = new List<MusicData>(),
            
            MailBox = new List<MailBoxItem>()
        };
        
        Users.Add(user);
        
        UserData.Add(user.UserData);
        UserDataAqours.Add(user.UserDataAqours);
        UserDataSaintSnow.Add(user.UserDataSaintSnow);
        
        MemberData.AddRange(user.Members);
        MemberCardData.AddRange(user.MemberCards);
        SkillCardData.AddRange(user.SkillCards);
        MemorialCardData.AddRange(user.MemorialCards);
        
        LiveDatas.AddRange(user.LiveDatas);
        
        TravelData.AddRange(user.TravelData);
        TravelPamphlets.AddRange(user.TravelPamphlets);
        TravelTalks.AddRange(user.TravelTalks);
        
        GameHistory.AddRange(user.GameHistory);
        TravelHistory.AddRange(user.TravelHistory);
        
        Achievements.AddRange(user.Achievements);
        YellAchievements.AddRange(user.YellAchievements);
        AchievementRecordBooks.AddRange(user.AchievementRecordBooks);
        LimitedAchievements.AddRange(user.LimitedAchievements);
        
        Items.AddRange(user.Items);
        SpecialItems.AddRange(user.SpecialItems);
        
        CardFrames.AddRange(user.CardFrames);
        NamePlates.AddRange(user.NamePlates);
        Badges.AddRange(user.Badges);
        Honors.AddRange(user.Honors);
        
        Musics.AddRange(user.Musics);
        
        MailBox.AddRange(user.MailBox);

        await SaveChangesAsync();
        
        return user;
    }

    public async Task<User?> LoadFullProfile(ulong userId)
    {
        User? user = await Users
            .Where(u => u.UserId == userId)
            .AsSplitQuery()
            .Include(u => u.UserData)
            .Include(u => u.UserDataAqours)
            .Include(u => u.UserDataSaintSnow)
                
            .Include(u => u.Members)
            .Include(u => u.MemberCards)
            .Include(u => u.SkillCards)
            .Include(u => u.MemorialCards)
                
            .Include(u => u.Musics)
            .Include(u => u.LiveDatas)
                
            .Include(u => u.TravelData)
            .Include(u => u.TravelPamphlets)
            .Include(u => u.TravelTalks)
                
            .Include(u => u.GameHistory)
            .Include(u => u.TravelHistory)
                
            .Include(u => u.Achievements)
            .Include(u => u.YellAchievements)
            .Include(u => u.AchievementRecordBooks)
            .Include(u => u.LimitedAchievements)
                
            .Include(u => u.Items)
            .Include(u => u.SpecialItems)
                
            .Include(u => u.CardFrames)
            .Include(u => u.NamePlates)
            .Include(u => u.Badges)
            .Include(u => u.Honors)
                
            .Include(u => u.MailBox)
            .FirstOrDefaultAsync();
        return user;
    }

    public async Task DeleteProfile(ulong userId)
    {
        GameSession[] sessions = await Sessions
            .Where(s => s.UserId == userId)
            .ToArrayAsync();
        
        //delete sessions
        Sessions.RemoveRange(sessions);
        
        User? user = await LoadFullProfile(userId); 

        if (user == null) return;
        
        Users.Remove(user);
        await SaveChangesAsync();
    }
}