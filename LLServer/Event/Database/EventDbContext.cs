using Microsoft.EntityFrameworkCore;

namespace LLServer.Event.Database;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ResourceEntry> Resources { get; set; }
    public DbSet<InformationEntry> Information { get; set; }
    public DbSet<EventEntry> Events { get; set; }
}