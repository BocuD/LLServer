using LLServer.Event.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Event.WebUI;

[Route("Event/Information")]
public class InformationEditor : Controller
{
    private readonly EventDbContext eventDbContext;

    public InformationEditor(EventDbContext eventDbContext)
    {
        this.eventDbContext = eventDbContext;
    }
    
    public async Task<IActionResult> CreateInformation()
    {
        return RedirectToAction("EditInformation", new {id = -1});
    }

    [HttpGet]
    public async Task<IActionResult> EditInformation(int id)
    {
        //get all the resources
        var resources = await eventDbContext.Resources.ToListAsync();
        ViewBag.Resources = new SelectList(resources, "id", "name");
        
        if (id == -1)
        {
            return View("EditInformation", new InformationEntry
            {
                name = "New information entry",
                start = DateTime.Now,
                end = DateTime.Now
            });
        }
        
        InformationEntry? entry = await eventDbContext.Information.FindAsync(id);
        
        if (entry == null)
        {
            return NotFound();
        }

        return View("EditInformation", entry);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditInformation(InformationEntry informationEntry)
    {
        try
        {
            InformationEntry? entry;
            if (informationEntry.id == -1)
            {
                entry = new InformationEntry();
            }
            else
            {
                entry = eventDbContext.Information.Find(informationEntry.id);
            }
            
            if (entry == null)
            {
                return NotFound();
            }
            
            //overwrite the properties
            entry.name = informationEntry.name;
            entry.startString = informationEntry.startString;
            entry.endString = informationEntry.endString;
            entry.resourceID = informationEntry.resourceID;
            entry.DisplayCenter = informationEntry.DisplayCenter;
            entry.DisplaySatellite = informationEntry.DisplaySatellite;
            
            if (informationEntry.id == -1)
            {
                eventDbContext.Information.Add(entry);
            }
            
            await eventDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!eventDbContext.Information.Any(i => i.id == informationEntry.id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction("Index", "Overview");
    }

    [HttpGet("DeleteInformation")]
    public IActionResult DeleteInformation(int id)
    {
        InformationEntry? entry = eventDbContext.Information.Find(id);
        if(entry == null)
        {
            return NotFound();
        }

        eventDbContext.Information.Remove(entry);
        eventDbContext.SaveChanges();
        
        return RedirectToAction("Index", "Overview");
    }
}