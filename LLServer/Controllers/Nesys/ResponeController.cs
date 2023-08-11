using LLServer.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MainServer.Controllers.Game;

[ApiController]
[Route("service/respone")]
public class ResponeController : BaseController<ResponeController>
{
    [HttpPost("respone.php")]
    public IActionResult Respone()
    {
        Logger.LogInformation("Respone data from {RemoteIpAddress} {data}", Request.HttpContext.Connection.RemoteIpAddress, Request.Body);
        
        return Ok("1");
    }
}