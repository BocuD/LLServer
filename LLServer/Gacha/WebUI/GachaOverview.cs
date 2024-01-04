using LLServer.Gacha.Database;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Gacha.WebUI;

public class GachaOverviewModel
{
    public GachaTable[] gachaTables { get; set; }
}

public class GachaCardsModel
{
    public GachaCardGroup[] gachaCardGroups { get; set; }
    public GachaCard[] memberCards { get; set; }
    public GachaCard[] skillCards { get; set; }
    public GachaCard[] memorialCards { get; set; }
}

[Route("Gacha/Overview")]
public class GachaOverviewController : Controller
{
    private readonly GachaDbContext gachaDbContext;
    private readonly GachaDataProvider gachaDataProvider;
    private readonly ILogger<GachaOverviewController> logger;

    public GachaOverviewController(GachaDbContext gachaDbContext, GachaDataProvider gachaDataProvider, ILogger<GachaOverviewController> logger)
    {
        this.gachaDbContext = gachaDbContext;
        this.gachaDataProvider = gachaDataProvider;
        this.logger = logger;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        GachaOverviewModel model = new()
        {
            gachaTables = gachaDbContext.GachaTables.ToArray(),
        };

        return View(model);
    }
    
    [HttpGet("Cards")]
    public IActionResult Cards()
    {
        GachaCardsModel model = new()
        {
            gachaCardGroups = gachaDbContext.GachaCardGroups.ToArray(),
            memberCards = gachaDbContext.GachaCards.Where(c => c.cardType == CardType.Member).ToArray(),
            skillCards = gachaDbContext.GachaCards.Where(c => c.cardType == CardType.Skill).ToArray(),
            memorialCards = gachaDbContext.GachaCards.Where(c => c.cardType == CardType.Memorial).ToArray()
        };

        return View(model);
    }

    [HttpGet("ScanCards")]
    public IActionResult ScanCards()
    {
        gachaDataProvider.ScanCards();
        
        return RedirectToAction("Index");
    }
    
    [HttpGet("RescanLogs")]
    public async Task<IActionResult> RescanLogs()
    {
        await gachaDataProvider.RescanLogs();
        
        return RedirectToAction("Index");
    }

    [HttpGet("UploadLog")]
    public IActionResult UploadLog()
    {
        return View();
    }
    
    [HttpPost("UploadLog")]
    public async Task<IActionResult> UploadLog(IFormFile logFile)
    {
        if (logFile == null)
        {
            return BadRequest();
        }

        logger.LogInformation("Received log file {FileName}", logFile.FileName);
        
        //ensure the folder exists
        string fullPath = Path.Combine("uploadedlogs", logFile.FileName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? throw new InvalidOperationException());
        
        //check if the filename already exists
        if (System.IO.File.Exists(fullPath))
        {
            //append a number to the filename
            int i = 1;
            while (System.IO.File.Exists(fullPath))
            {
                fullPath = Path.Combine("uploadedlogs", $"{Path.GetFileNameWithoutExtension(logFile.FileName)}_{i}{Path.GetExtension(logFile.FileName)}");
                i++;
            }
        }
        
        //save the file to disk at /uploadedlogs
        await using FileStream fileStream = new(fullPath, FileMode.Create);
        await logFile.CopyToAsync(fileStream);
        fileStream.Close();
        
        using StreamReader reader = new(logFile.OpenReadStream());
        string log = await reader.ReadToEndAsync();

        await gachaDataProvider.ParseLogFile(log);

        return RedirectToAction("Index");
    }
    
    [HttpPost("UploadFolder")]
    public async Task<IActionResult> UploadFolder()
    {
        var files = Request.Form.Files;
        foreach (var file in files)
        {
            await UploadLog(file);
        }

        return RedirectToAction("Index");
    }
}