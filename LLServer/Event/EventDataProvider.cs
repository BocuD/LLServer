using LLServer.Event.Database;
using LLServer.Models.Information;

namespace LLServer.Event;

public class EventDataProvider
{
    private readonly ILogger<EventDataProvider> logger;
    private readonly EventDbContext eventDbContext;

    public EventDataProvider(ILogger<EventDataProvider> logger, EventDbContext eventDbContext)
    {
        this.logger = logger;
        this.eventDbContext = eventDbContext;
    }
    
    public void CacheEvents()
    {
        //todo: actually cache stuff heh
        logger.LogInformation("Event count: {eventcount}", eventDbContext.Events.Count());
        logger.LogInformation("Information count: {eventcount}", eventDbContext.Information.Count());
    }

    public List<ResourceInformation> GetResourceInformation()
    {
        List<ResourceInformation> resourceInformation = new();
        int resourceId = 0;

        foreach (ResourceEntry resource in eventDbContext.Resources)
        {
            resourceInformation.Add(new ResourceInformation
            {
                Id = resourceId++,
                Category = 0,
                Enable = true,
                ResourceId = resource.id.ToString(),
                Image = Path.GetFileName(resource.path),
                Title = resource.name,
                Hash = resource.hash
            });
        }
        
        return resourceInformation;
    }

    public List<Information> GetInformation(bool isTerminal = false)
    {
        List<Information> information = new();
        int informationId = 0;
        
        foreach (InformationEntry informationEntry in eventDbContext.Information)
        {
            if (informationEntry.start > DateTime.Now || informationEntry.end < DateTime.Now)
            {
                continue;
            }
            
            if (isTerminal && !informationEntry.DisplayCenter || !isTerminal && !informationEntry.DisplaySatellite)
            {
                continue;
            }
            
            information.Add(new Information
            {
                Id = informationId++,
                Category = 0,
                DisplayCenter = informationEntry.DisplayCenter,
                DisplaySatellite = informationEntry.DisplaySatellite,
                Enable = true,
                StartDatetime = informationEntry.start.ToString("yyyy-MM-ddhh:mm:ss"),
                EndDatetime = informationEntry.end.ToString("yyyy-MM-ddhh:mm:ss"),
                Image = Path.GetFileName(informationEntry.resource.path),
                Order = 0,
                Resource = informationEntry.resource.id.ToString(),
                Title = informationEntry.name
            });
        }
        
        return information;
    }
}