using LLServer.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MainServer.Controllers.Game;

[ApiController]
[Route("service/respone")]
public class ResponeController : BaseController<ResponeController>
{
    [HttpPost("respone.php")]
    public async Task<IActionResult> Respone()
    {
        //get body from request
        string body = await new StreamReader(Request.Body).ReadToEndAsync();
        Logger.LogInformation("Respone data from {RemoteIpAddress} {data}", Request.HttpContext.Connection.RemoteIpAddress, body);
        
        return Ok("1");
    }
}