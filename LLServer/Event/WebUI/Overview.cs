using LLServer.Event.Database;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Event.WebUI;

//model for the event editor page
public class OverviewModel
{
    public ResourceEntry[] resources { get; set; }
    public InformationEntry[] information { get; set; }
    public EventEntry[] events { get; set; }
}

[Route("Event/Overview")]
public class OverviewController : Controller
{
    private readonly EventDbContext eventDbContext;

    public OverviewController(EventDbContext eventDbContext)
    {
        this.eventDbContext = eventDbContext;
    }
    
    [HttpGet]
    public ActionResult Index()
    {
        OverviewModel model = new()
        {
            resources = eventDbContext.Resources.ToArray(),
            information = eventDbContext.Information.ToArray(),
            events = eventDbContext.Events.ToArray()
        };

        return View(model);
    }

    // POST: Event/CreateElement
    [HttpPost]
    public ActionResult CreateElement(string submit)
    {
        switch (submit)
        {
            case "Create Event":
                //return RedirectToAction("CreateEvent");
                break;

            case "Create Information":
                return RedirectToAction("EditInformation", "InformationEditor", new {id = -1});

            case "Create Resource":
                return RedirectToAction("CreateResource", "ResourceEditor");

            default:
                BadRequest();
                break;
        }

        // Redirect or return appropriate view
        return RedirectToAction(nameof(Index));
    }
}