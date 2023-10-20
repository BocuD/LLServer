namespace LLServer.Event;

interface IEventDataProvider
{
    
}

public class EventDataProvider : IEventDataProvider
{
    static EventDataProvider()
    {
        //load all events
        //load all event data
        //load all event images
        
        //log a test message
        Console.WriteLine("Static constructor called");
    }
    
    private readonly ILogger<EventDataProvider> logger;

    public EventDataProvider(ILogger<EventDataProvider> logger)
    {
        this.logger = logger;
        
        logger.LogInformation("EventDataProvider constructor called");
    }
}