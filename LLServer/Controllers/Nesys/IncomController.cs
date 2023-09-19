using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Nesys;

[ApiController]
[Route("service/incom")]
public class IncomController : BaseController<IncomController>
{
    private const string INCOM_RESPONSE = "1+1";

    [HttpPost("incom.php")]
    public async Task<IActionResult> Incom()
    {
        string body = await new StreamReader(Request.Body).ReadToEndAsync();
        Logger.LogInformation("Incom data from {RemoteIpAddress} {data}", Request.HttpContext.Connection.RemoteIpAddress, body);
        
        return Ok(INCOM_RESPONSE);
    }

    [HttpPost("incomALL.php")]
    public async Task<IActionResult> IncomAll()
    {
        string body = await new StreamReader(Request.Body).ReadToEndAsync();
        Logger.LogInformation("incomAll data from {RemoteIpAddress} {data}", Request.HttpContext.Connection.RemoteIpAddress, body);

        return Ok(INCOM_RESPONSE);
    }
    
    [HttpPost("shop.php")]
    public async Task<IActionResult> Shop()
    {
        string body = await new StreamReader(Request.Body).ReadToEndAsync();
        Logger.LogInformation("Shop data from {RemoteIpAddress} {data}", Request.HttpContext.Connection.RemoteIpAddress, body);

        return Ok(INCOM_RESPONSE);
    }
}