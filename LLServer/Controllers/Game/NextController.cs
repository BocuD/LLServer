using System.Text;
using LLServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Game;

// TODO: In theory this would be where next request is sent
[ApiController]
[Route("game/next")]
public class NextController : BaseController<NextController>
{
    [HttpPost]
    public async Task<ActionResult<ResponseContainer>> Respond()
    {
        var body = await Request.BodyReader.ReadAsync();
        Logger.LogInformation("Body {Body}", Encoding.Default.GetString(body.Buffer));
        
        var result = new ResponseContainer
        {
            Response = new ResponseBase(),
            Result = 200
        };

        return result;
    }
}