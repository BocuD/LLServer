using System.Security.Cryptography;
using LLServer.Event.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Event.WebUI;

[Route("Event/Resource")]
public class ResourceEditor : Controller
{
    private readonly EventDbContext eventDbContext;

    public ResourceEditor(EventDbContext eventDbContext)
    {
        this.eventDbContext = eventDbContext;
    }
    
    [HttpGet("CreateResource")]
    public ActionResult CreateResource()
    {
        //get all the files inside wwwroot/info
        string[] paths = Directory.GetFiles("wwwroot/info", "*", SearchOption.AllDirectories);

        ViewBag.Paths = paths;

        return View("CreateResource");
    }

    [HttpPost("CreateResource")]
    public async Task<ActionResult> CreateResource(ResourceEntry resourceEntry)
    {
        if (eventDbContext.Resources.Any(e => e.path == resourceEntry.path))
        {
            ModelState.AddModelError("path", "This path already exists");
        }

        //get the file and compute the hash
        using MD5 md5 = MD5.Create();
        
        using FileStream stream = System.IO.File.OpenRead(resourceEntry.path);
        resourceEntry.hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
        resourceEntry.name = Path.GetFileName(resourceEntry.path);
        
        eventDbContext.Resources.Add(resourceEntry);
        await eventDbContext.SaveChangesAsync();
        
        //redirect to edit resource
        return RedirectToAction(nameof(EditResource), new {resourceEntry.id});
    }
    
    [HttpGet("EditResource")]
    public async Task<IActionResult> EditResource(int id)
    {
        ResourceEntry? entry = await eventDbContext.Resources.FindAsync(id);
        
        if (entry == null)
        {
            return NotFound();
        }
        
        //get references to resource
        var informationReferences = eventDbContext.Information.Where(e => e.resourceID == id).ToList();
        var eventReferences = eventDbContext.Events.Where(e => e.resourceID == id).ToList();

        string references = "\n";
        
        foreach(var information in informationReferences)
        {
            references += $"Information: {information.name}\n";
        }
        
        foreach(var @event in eventReferences)
        {
            references += $"Event: {@event.name}\n";
        }

        ViewBag.ResourceReferences = references;
        
        return View("EditResource", entry);
    }

    [HttpPost("EditResource")]
    public async Task<IActionResult> UpdateResource(ResourceEntry resourceEntry)
    {
        try
        {
            //overwrite the properties
            var entry = eventDbContext.Resources.Find(resourceEntry.id);
            entry.name = resourceEntry.name;
            await eventDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!eventDbContext.Resources.Any(e => e.id == resourceEntry.id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction("Index", "Overview");
    }

    [HttpGet("DeleteResource")]
    public IActionResult DeleteResource(int id)
    {
        ResourceEntry? entry = eventDbContext.Resources.Find(id);
        if(entry == null)
        {
            return NotFound();
        }

        eventDbContext.Resources.Remove(entry);
        eventDbContext.SaveChanges();
        
        return RedirectToAction("Index", "Overview");
    }
}