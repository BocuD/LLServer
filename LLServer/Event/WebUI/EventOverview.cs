using System.Text;
using LLServer.Event.Database;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Event.WebUI;

//model for the event editor page
public class EventOverviewModel
{
    public ResourceEntry[] resources { get; set; }
    public InformationEntry[] information { get; set; }
    public EventEntry[] events { get; set; }
}

[Route("Event/Overview")]
public class EventOverviewController : Controller
{
    private readonly EventDbContext eventDbContext;

    public EventOverviewController(EventDbContext eventDbContext)
    {
        this.eventDbContext = eventDbContext;
    }
    
    [HttpGet]
    public ActionResult Index()
    {
        EventOverviewModel model = new()
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

    [HttpGet("UploadJson")]
    public IActionResult UploadJson()
    {
        return View();
    }
    
    [HttpPost("UploadJson")]
    public async Task<IActionResult> UploadJson(IFormFile jsonFile)
    {
        if (jsonFile == null)
        {
            return BadRequest();
        }
        
        using (var reader = new StreamReader(jsonFile.OpenReadStream()))
        {
            string json = await reader.ReadToEndAsync();
            
            eventDbContext.ImportFromJson(json);
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet("DownloadJson")]
    public IActionResult DownloadJson()
    {
        string json = eventDbContext.ExportToJson();
        
        return File(Encoding.UTF8.GetBytes(json), "application/json", "events.json");
    }
}
