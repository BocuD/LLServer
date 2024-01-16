using System.Text.Json;
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

    public string ExportToJson()
    {
        var cards = GachaCards.ToList();
        var tables = GachaTables.ToList();
        var groups = GachaCardGroups.ToList();
        
        var json = JsonSerializer.Serialize(new
        {
            cards,
            tables,
            groups
        }, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        return json;
    }

    public void ImportFromJson(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, options);

        var cards = JsonSerializer.Deserialize<List<GachaCard>>(data["cards"].GetRawText(), options);
        var tables = JsonSerializer.Deserialize<List<GachaTable>>(data["tables"].GetRawText(), options);
        var groups = JsonSerializer.Deserialize<List<GachaCardGroup>>(data["groups"].GetRawText(), options);
        
        //clear old entries
        GachaCards.RemoveRange(GachaCards);
        GachaTables.RemoveRange(GachaTables);
        GachaCardGroups.RemoveRange(GachaCardGroups);

        SaveChanges();
        
        //add new entries
        GachaCards.AddRange(cards);
        GachaTables.AddRange(tables);
        GachaCardGroups.AddRange(groups);
        
        SaveChanges();
    }
}
