using System.Data;
using LLServer.Gacha.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Gacha.WebUI;

[Route("Gacha/GroupEditor")]
public class GachaGroupEditor : Controller
{
    private readonly GachaDbContext gachaDbContext;
    
    public GachaGroupEditor(GachaDbContext gachaDbContext)
    {
        this.gachaDbContext = gachaDbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> EditGroup(int id)
    {
        if (id == -1)
        {
            GachaCardGroup newGroup = new()
            {
                name = "New card group"
            };
            
            gachaDbContext.GachaCardGroups.Add(newGroup);
            await gachaDbContext.SaveChangesAsync();
            
            //get the id of the new group
            id = newGroup.id;
            
            return RedirectToAction("EditGroup", new { id = id });
        }

        GachaCardGroup? group = await gachaDbContext.GachaCardGroups.FindAsync(id);

        if (group == null)
        {
            return NotFound();
        }

        var cardIdNamePairs = await gachaDbContext.GachaCards
            .Select(c => new { c.id, c.name })
            .ToListAsync();
        ViewBag.cardIdNamePairs = new SelectList(cardIdNamePairs, "id", "name");
        
        Dictionary<string, string> cardIdToImage = new();
        
        foreach (GachaCard card in gachaDbContext.GachaCards)
        {
            cardIdToImage.Add(card.id, card.GetCardImage());
        }
        
        ViewBag.cardIdToImage = cardIdToImage;

        return View("EditCardGroup", group);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditGroup(GachaCardGroup group)
    {
        try
        {
            GachaCardGroup? entry;
            entry = await gachaDbContext.GachaCardGroups.FindAsync(group.id);

            if (entry == null)
            {
                return NotFound();
            }
            
            //overwrite the properties
            entry.name = group.name;
            entry.cardIds = group.cardIds;

            //save changes
            await gachaDbContext.SaveChangesAsync();
        }
        catch (DBConcurrencyException)
        {
            if (!gachaDbContext.GachaCardGroups.Any(i => i.id == group.id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction("Cards", "GachaOverview");
    }
    
    [HttpGet("DeleteGroup")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        GachaCardGroup? group = gachaDbContext.GachaCardGroups.Find(id);
        if(group == null)
        {
            return NotFound();
        }
        
        //check if the group id is used in any table
        foreach (GachaTable table in gachaDbContext.GachaTables)
        {
            if (table.cardGroupIds.Contains(group.id))
            {
                return BadRequest("Group is still in use in table " + table.id + "!");
            }
        }

        gachaDbContext.GachaCardGroups.Remove(group);
        await gachaDbContext.SaveChangesAsync();
        
        return RedirectToAction("Cards", "GachaOverview");
    }
}