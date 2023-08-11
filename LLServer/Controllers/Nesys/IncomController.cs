using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Nesys;

[ApiController]
[Route("service/incom")]
public class IncomController : BaseController<IncomController>
{
    private const string INCOM_RESPONSE = "1+1";

    [HttpPost("incom.php")]
    public IActionResult Incom()
    {
        Logger.LogInformation("Incom data from {RemoteIpAddress} {data}", Request.HttpContext.Connection.RemoteIpAddress, Request.Body);
        
        return Ok(INCOM_RESPONSE);
    }

    [HttpPost("incomALL.php")]
    public IActionResult IncomAll()
    {
        Logger.LogInformation("incomAll data from {RemoteIpAddress} {data}", Request.HttpContext.Connection.RemoteIpAddress, Request.Body);

        return Ok(INCOM_RESPONSE);
    }
}