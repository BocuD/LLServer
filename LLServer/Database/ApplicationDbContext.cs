using LLServer.Database.Models;
using LLServer.Models.UserData;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<UserData> UserData { get; set; }
    public DbSet<UserDataAqours> UserDataAqours { get; set; }
    public DbSet<UserDataSaintSnow> UserDataSaintSnow { get; set; }
    public DbSet<MemberData> MemberData { get; set; }
    public DbSet<MemberCardData> MemberCardData { get; set; }
    
    public DbSet<PersistentLiveData> LiveDatas { get; set; }
    
    public DbSet<TravelData> TravelData { get; set; }
    public DbSet<TravelPamphlet> TravelPamphlets { get; set; }
    public DbSet<TravelHistory> TravelHistory { get; set; }
    public DbSet<TravelHistoryAqours> TravelHistoryAqours { get; set; }
    public DbSet<TravelHistorySaintSnow> TravelHistorySaintSnow { get; set; }
    
    public DbSet<YellAchievement> YellAchievements { get; set; }
    public DbSet<AchievementRecordBook> AchievementRecordBooks { get; set; }
    
    public DbSet<Item> Items { get; set; }
    public DbSet<SpecialItem> SpecialItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(user => user.UserId);

        modelBuilder.Entity<User>()
            .HasAlternateKey(user => user.CardId);
        
        

        modelBuilder.Entity<Session>()
            .HasKey(s => s.SessionId);
        
        modelBuilder.Entity<Session>()
            .Property(s => s.SessionId)
            .HasMaxLength(32);

        modelBuilder.Entity<Session>()
            .Property(s => s.IsActive)
            .IsRequired();

        modelBuilder.Entity<Session>()
            .Property(s => s.CreateTime)
            .IsRequired();

        modelBuilder.Entity<Session>()
            .Property(s => s.ExpireTime)
            .IsRequired();

        modelBuilder.Entity<Session>()
            .HasOne<User>(s => s.User)
            .WithOne(u => u.Session)
            .HasForeignKey<Session>(s => s.UserId);
    }
}