using System.Text.Json;
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
    
    public string ExportToJson()
    {
        var resources = Resources.ToList();
        var information = Information.ToList();
        var events = Events.ToList();
        
        var json = JsonSerializer.Serialize(new
        {
            resources,
            information,
            events
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

        var resources = JsonSerializer.Deserialize<List<ResourceEntry>>(data["resources"].GetRawText(), options);
        var information = JsonSerializer.Deserialize<List<InformationEntry>>(data["information"].GetRawText(), options);
        var events = JsonSerializer.Deserialize<List<EventEntry>>(data["events"].GetRawText(), options);
        
        //clear old entries
        Resources.RemoveRange(Resources);
        Information.RemoveRange(Information);
        Events.RemoveRange(Events);

        SaveChanges();
        
        //add new entries
        foreach (ResourceEntry resource in resources.Where(resource => Resources.All(r => r.id != resource.id)))
        {
            Resources.Add(resource);
        }
        
        foreach (InformationEntry info in information.Where(info => Information.All(i => i.id != info.id)))
        {
            Information.Add(info);
        }
        
        foreach (EventEntry @event in events.Where(@event => Events.All(e => e.id != @event.id)))
        {
            Events.Add(@event);
        }
        
        SaveChanges();
    }
}
