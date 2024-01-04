using System.Data;
using LLServer.Gacha.Database;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Gacha.WebUI;

[Route("Gacha/CardEditor")]
public class GachaCardEditor : Controller
{
    private readonly GachaDbContext gachaDbContext;

    public GachaCardEditor(GachaDbContext gachaDbContext)
    {
        this.gachaDbContext = gachaDbContext;
    }
    
    [HttpGet("EditCard")]
    public async Task<IActionResult> EditCard(string id)
    {
        GachaCard? entry = await gachaDbContext.GachaCards.FindAsync(id);
        
        if (entry == null)
        {
            return NotFound();
        }

        return View("EditGachaCard", entry);
    }

    [HttpPost("EditCard")]
    public async Task<IActionResult> EditCard(GachaCard gachaCard)
    {
        try
        {
            GachaCard? entry;
            entry = await gachaDbContext.GachaCards.FindAsync(gachaCard.id);

            if (entry == null)
            {
                return NotFound();
            }

            //overwrite the properties
            entry.name = gachaCard.name;

            await gachaDbContext.SaveChangesAsync();
        }
        catch (DBConcurrencyException)
        {
            if (!gachaDbContext.GachaCards.Any(i => i.id == gachaCard.id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction("Cards", "GachaOverview");
    }

    [HttpGet("DeleteCard")]
    public IActionResult DeleteCard(string id)
    {
        GachaCard? card = gachaDbContext.GachaCards.Find(id);
        if(card == null)
        {
            return NotFound();
        }

        gachaDbContext.GachaCards.Remove(card);
        gachaDbContext.SaveChanges();
        
        return RedirectToAction("Cards", "GachaOverview");
    }
}