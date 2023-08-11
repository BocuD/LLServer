using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Nesys;

[ApiController]
[Route("server")]
public class ServerController : BaseController<ServerController>
{
    [HttpGet("cursel.php")]
    public ActionResult<string> GetCursel()
    {
        Logger.LogInformation("Cursel request from {RemoteIpAddress}", Request.HttpContext.Connection.RemoteIpAddress);
        
        return Ok("1\n");
    }

    [HttpGet("gameinfo.php")]
    public ActionResult<string> GetGameInfo()
    {
        Logger.LogInformation("Game info request from {RemoteIpAddress}", Request.HttpContext.Connection.RemoteIpAddress);
        
        return Ok("0\n"            +
                  "3\n"            +
                  "301000,test1\n" +
                  "302000,test2\n" +
                  "303000,test3");
    }

    [HttpGet("certify.php")]
    public ActionResult<string> Certify(string? gid,    string?                          mac,
        [FromQuery(Name = "r")] string?         random, [FromQuery(Name = "md")] string? md5)
    {
        Logger.LogInformation("Certify request from {RemoteIpAddress}", Request.HttpContext.Connection.RemoteIpAddress);
        
        var serverIpAddress = Request.HttpContext.Connection.LocalIpAddress;
        var ticket = string.Join(string.Empty,
            MD5.HashData(Encoding.UTF8.GetBytes(gid ?? "303807")).Select(b => b.ToString("x2")));
        var response = $"host=http://{serverIpAddress}\n"                             +
                       "no=1337\n"                                                    +
                       "name=LLServer\n"                                              +
                       "pref=nesys\n"                                                 +
                       "addr=Local\n"                                                 +
                       "x-next-time=15\n"                                             +
                       $"x-img=http://{Request.Host.Value}/news.png\n"                +
                       $"x-ranking=http://{Request.Host.Value}/ranking/ranking.php\n" +
                       $"ticket={ticket}";
        return Ok(response);
    }

    [HttpGet("data.php")]
    public ActionResult<string> GetData()
    {
        Logger.LogInformation("Data request from {RemoteIpAddress}", Request.HttpContext.Connection.RemoteIpAddress);
        
        const string response = "count=0\n" +
                                "nexttime=0\n";
        return Ok(response);
    }

    [HttpGet("FireAlert.php")]
    public ActionResult<string> FireAlert()
    {
        Logger.LogInformation("Fire alert from {RemoteIpAddress}", Request.HttpContext.Connection.RemoteIpAddress);
        return Ok("Success");
    }
}