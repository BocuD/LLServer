using System.Text;
using System.Text.Json;
using LLServer.Handlers;
using LLServer.Models;
using LLServer.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LLServer.Controllers.Game;

[ApiController]
[Route("game")]
public class GameController : BaseController<GameController>
{
    [HttpPost]
    public async Task<ActionResult<ResponseContainer>> BaseHandler()
    {
        var body = await Request.BodyReader.ReadAsync();
        var bodyString = Encoding.Default.GetString(body.Buffer).Replace("\0", string.Empty);
        Logger.LogInformation("Body {Body}", bodyString);

        // Parse body as json
        var request = JsonSerializer.Deserialize<RequestBase>(bodyString);
        if (request is null)
        {
            Logger.LogWarning("Request deserialize failed");
            return BadRequest();
        }

        ResponseContainer response;
        switch (request.Protocol)
        {
            case "unlock":
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
                break;
            case "gameconfig":
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
                break;
            case "information":
                response = InformationHandler.Handle();
                break;
            case "auth":
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new AuthResponse
                    {
                        AbnormalEnd = 0,
                        BlockSequence = 1,
                        Name = "",
                        SessionKey = "12345678901234567890123456789012",
                        Status = 0,
                        UserId = "1"
                    }
                };
                break;
            default:
                Logger.LogWarning("Unhandled protocol: {Protocol}", request.Protocol);
                response = new ResponseContainer
                {
                    Result = 200,
                    Response = new ResponseBase()
                };
                break;
        }

        return Ok(response);
    }
}