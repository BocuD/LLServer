using LLServer.Event.Database;

namespace LLServer.Event;

interface IEventDataProvider
{
    
}

public class EventDataProvider : IEventDataProvider
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
        logger.LogInformation("Event count: {eventcount}", eventDbContext.Events.Count());
        logger.LogInformation("Information count: {eventcount}", eventDbContext.Information.Count());
    }
}