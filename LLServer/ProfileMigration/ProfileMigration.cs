using System.Text;
using System.Text.Json;
using LLServer.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LLServer.ProfileMigration;

public class OverviewModel
{
    public class ProfileData
    {
        public string name;
        public string userId;
        public string lastLogin;
        public string tenpo;
        public int playcountSatellite;
        public int playcountCenter;
        public string nesicaId;
    }

    public List<ProfileData> Profiles { get; set; }
}

public class ProfileMigration : Controller
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<ProfileMigration> logger;
    
    public ProfileMigration(ApplicationDbContext dbContext, ILogger<ProfileMigration> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }
    
    [HttpGet("ProfileMigration")]
    public IActionResult Index()
    {
        var users = dbContext.Users.Include(u => u.UserData).ToArray();
        
        var profiles = users.Select(user => new OverviewModel.ProfileData
        {
            name = user.UserData.Name,
            userId = user.UserId.ToString(),
            lastLogin = user.UserData.PlayDate,
            tenpo = user.UserData.TenpoName,
            playcountSatellite = user.UserData.PlayCenter,
            playcountCenter = user.UserData.PlaySatellite,
            nesicaId = user.CardId
        }).ToList();

        OverviewModel model = new()
        {
            Profiles = profiles
        };
        return View(model);
    }
    
    [HttpGet("DownloadJson")]
    public async Task<IActionResult> DownloadJson(ulong userId)
    {
        //load user profile
        var user = await dbContext.LoadFullProfile(userId);
        
        //export to json
        var json = JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });
        return File(Encoding.UTF8.GetBytes(json), "application/json", $"{user.CardId}.json");
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

            await dbContext.ImportProfileFromJson(json);
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet("DeleteProfile")]
    public async Task<IActionResult> DeleteProfile(ulong userId)
    {
        await dbContext.DeleteProfile(userId);
        
        return RedirectToAction("Index");
    }
}