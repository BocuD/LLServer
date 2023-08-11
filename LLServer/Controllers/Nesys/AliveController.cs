using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Nesys;

[ApiController]
[Route("alive")]
public class AliveController : BaseController<AliveController>
{
    [HttpGet("i.php")]
    public IActionResult AliveCheck()
    {
        var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
        var serverIpAddress = Request.HttpContext.Connection.LocalIpAddress;
        var response = $"REMOTE ADDRESS:{remoteIpAddress}\n" +
                       "SERVER NAME:GCLocalServer\n"         +
                       $"SERVER ADDR:{serverIpAddress}";
        
        Logger.LogInformation("Alive check from {RemoteIpAddress}", remoteIpAddress);

        return Ok(response);
    }

    [HttpGet("{id}/Alive.txt")]
    public IActionResult GetAliveFile()
    {
        Logger.LogInformation("Alive file request from {RemoteIpAddress}", Request.HttpContext.Connection.RemoteIpAddress);
        
        return Ok("");
    }
}