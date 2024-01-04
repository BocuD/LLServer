using System.Data;
using LLServer.Gacha.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LLServer.Gacha.WebUI;

[Route("Gacha/TableEditor")]
public class GachaTableEditor : Controller
{
    private readonly GachaDbContext gachaDbContext;

    public GachaTableEditor(GachaDbContext gachaDbContext)
    {
        this.gachaDbContext = gachaDbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> EditTable(string id)
    {
        if (id == "new")
        {
            int randomId = new Random().Next();
            
            id = $"a_new_gacha_id_{randomId}";

            GachaTable newTable = new()
            {
                id = $"a_new_gacha_id_{randomId}",
                isValid = false,
                cardIds = Array.Empty<string>(),
                characterIdBools = new bool[23],
                metaData = new GachaTableMetaData()
            };
            
            gachaDbContext.GachaTables.Add(newTable);
            await gachaDbContext.SaveChangesAsync();
            
            return RedirectToAction("EditTable", new { id = id });
        }

        GachaTable? table = await gachaDbContext.GachaTables.FindAsync(id);

        if (table == null)
        {
            return NotFound();
        }

        var cardIdNamePairs = await gachaDbContext.GachaCards
            .Select(c => new { c.id, c.name })
            .ToListAsync();
        ViewBag.cardIdNamePairs = new SelectList(cardIdNamePairs, "id", "name");

        table.newName = table.id;

        return View("EditGachaTable", table);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditTable(GachaTable gachaTable)
    {
        try
        {
            GachaTable? entry;
            entry = await gachaDbContext.GachaTables.FindAsync(gachaTable.id);

            if (entry == null)
            {
                return NotFound();
            }
            
            //overwrite the properties
            entry.isValid = gachaTable.isValid;
            entry.characterIdBools = gachaTable.characterIdBools;
            entry.maxRarity = gachaTable.maxRarity;
            entry.cardIds = gachaTable.cardIds;
            
            if (gachaTable.newName != gachaTable.id)
            {
                //check if new name is already taken
                if (gachaDbContext.GachaTables.Any(i => i.id == gachaTable.newName))
                {
                    return BadRequest();
                }
                
                //create new entry with new id
                GachaTable newTable = new()
                {
                    isValid = entry.isValid,
                    characterIdBools = entry.characterIdBools,
                    maxRarity = entry.maxRarity,
                    cardIds = entry.cardIds,

                    metaData = entry.metaData,
                    id = gachaTable.newName
                };
            
                //remove old entry
                gachaDbContext.GachaTables.Remove(entry);
            
                //add new entry
                gachaDbContext.GachaTables.Add(newTable);
            }

            //save changes
            await gachaDbContext.SaveChangesAsync();
        }
        catch (DBConcurrencyException)
        {
            if (!gachaDbContext.GachaTables.Any(i => i.id == gachaTable.id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction("Index", "GachaOverview");
    }
    
    
    [HttpGet("DeleteTable")]
    public IActionResult DeleteTable(string id)
    {
        GachaTable? table = gachaDbContext.GachaTables.Find(id);
        if(table == null)
        {
            return NotFound();
        }

        gachaDbContext.GachaTables.Remove(table);
        gachaDbContext.SaveChanges();
        
        return RedirectToAction("Index", "GachaOverview");
    }
}