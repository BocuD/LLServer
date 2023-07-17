using LLServer.Database.Models;
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