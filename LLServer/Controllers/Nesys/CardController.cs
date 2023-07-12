using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Nesys;

[ApiController]
[Route("service/card")]
public class CardController : ControllerBase
{
    [HttpPost("cardn.cgi")]
    public IActionResult Card([FromForm(Name="card_no")]string cardNo)
    {
        return Ok($"1\n1,1\n{cardNo}");
    }
}