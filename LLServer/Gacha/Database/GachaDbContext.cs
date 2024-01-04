using Microsoft.EntityFrameworkCore;

namespace LLServer.Gacha.Database;

public class GachaDbContext : DbContext
{
    public GachaDbContext(DbContextOptions<GachaDbContext> options) : base(options)
    {
        
    }

    public DbSet<GachaCard> GachaCards { get; set; }
    public DbSet<GachaTable> GachaTables { get; set; }
    public DbSet<GachaCardGroup> GachaCardGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}