using LLServer.Database.Models;
using LLServer.Models.Requests.Travel;
using LLServer.Models.Travel;
using LLServer.Models.UserData;
using Microsoft.EntityFrameworkCore;
using ProfileCard = LLServer.Database.Models.ProfileCard;

namespace LLServer.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
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
}